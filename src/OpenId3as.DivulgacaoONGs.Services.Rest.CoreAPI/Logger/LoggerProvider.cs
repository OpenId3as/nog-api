using Microsoft.Extensions.Logging;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Interfaces;
using System;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Logger
{
    public class LoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> _filter;
        private readonly ILogRepository _logRepository;

        public LoggerProvider(Func<string, LogLevel, bool> filter, ILogRepository logRepository)
        {
            _filter = filter;
            _logRepository = logRepository;
        }

        public ILogger CreateLogger(string category)
        {
            return new Logger(category, _filter, _logRepository);
        }

        public void Dispose()
        {

        }
    }
}
