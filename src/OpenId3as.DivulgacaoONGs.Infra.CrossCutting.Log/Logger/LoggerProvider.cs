using Microsoft.Extensions.Logging;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Interfaces;
using System;

namespace OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Logger
{
    public class LoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> _filtro;
        private readonly string _cnString;

        public LoggerProvider(Func<string, LogLevel, bool> filtro, string cnString)
        {
            _filtro = filtro;
            _cnString = cnString;
        }

        public ILogger CreateLogger(string nomeCategoria)
        {
            return new Logger(nomeCategoria, _filtro, _cnString);
        }

        public void Dispose()
        {

        }
    }
}
