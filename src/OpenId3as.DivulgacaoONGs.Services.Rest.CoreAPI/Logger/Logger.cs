using Microsoft.Extensions.Logging;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Interfaces;
using System;
using Model = OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Entities;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Logger
{
    public class Logger : ILogger
    {
        private readonly string _category;
        private readonly Func<string, LogLevel, bool> _filter;
        private readonly ILogRepository _logRepository;
        private readonly int _messageMaxLength = 4000;

        public Logger(string category, Func<string, LogLevel, bool> filter, ILogRepository logRepository)
        {
            _category = category;
            _filter = filter;
            _logRepository = logRepository;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> format)
        {
            if (!IsEnabled(logLevel))
                return;

            if (format == null)
                throw new ArgumentNullException(nameof(format));

            var message = format(state, exception);
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            if (exception != null)
                message += $"\n{exception.ToString()}";

            message = message.Length > _messageMaxLength ? message.Substring(0, _messageMaxLength) : message;
            var log = new Model.Log()
            {
                Message = message,
                EventId = eventId.Id,
                LogLevel = logLevel.ToString(),
            };

            try
            {
                _logRepository.Add(log);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _filter == null || _filter(_category, logLevel);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}