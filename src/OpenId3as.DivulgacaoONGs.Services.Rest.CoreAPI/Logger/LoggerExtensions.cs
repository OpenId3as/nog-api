using Microsoft.Extensions.Logging;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Interfaces;
using System;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Logger
{
    public static class LoggerExtensions
    {
        public static ILoggerFactory AddContext(this ILoggerFactory factory, Func<string, LogLevel, bool> filter = null, ILogRepository logRepository = null)
        {
            factory.AddProvider(new LoggerProvider(filter, logRepository));
            return factory;
        }

        public static ILoggerFactory AddContext(this ILoggerFactory factory, LogLevel minLevel, ILogRepository logRepository)
        {
            return AddContext(factory, (_, logLevel) => logLevel >= minLevel, logRepository);
        }
    }
}
