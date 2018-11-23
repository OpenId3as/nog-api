using Microsoft.Extensions.Logging;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Context;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Interfaces;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Repositories;
using System;
using Model = OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Entities;

namespace OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Logger
{
    public class Logger : ILogger
    {
        private readonly string _nomeCategoria;
        private readonly Func<string, LogLevel, bool> _filtro;
        private readonly ILogRepository _logRepository;
        private readonly int _messageMaxLength = 4000;

        public Logger(string nomeCategoria, Func<string, LogLevel, bool> filtro, string cnString)
        {
            _nomeCategoria = nomeCategoria;
            _filtro = filtro;
            _logRepository = new LogRepository(new LogContext(cnString));
        }

        public void Log<TState>(LogLevel logLevel, EventId eventoId,
            TState state, Exception exception, Func<TState, Exception, string> formato)
        {
            if (!IsEnabled(logLevel))
                return;

            if (formato == null)
                throw new ArgumentNullException(nameof(formato));

            var mensagem = formato(state, exception);
            if (string.IsNullOrEmpty(mensagem))
            {
                return;
            }

            if (exception != null)
                mensagem += $"\n{exception.ToString()}";

            mensagem = mensagem.Length > _messageMaxLength ? mensagem.Substring(0, _messageMaxLength) : mensagem;
            var eventLog = new Model.Log()
            {
                Message = mensagem,
                EventId = eventoId.Id,
                LogLevel = logLevel.ToString(),
            };

            try
            {
                _logRepository.Add(eventLog);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return (_filtro == null || _filtro(_nomeCategoria, logLevel));
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}