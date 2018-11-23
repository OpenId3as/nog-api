using Microsoft.Extensions.Logging;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Interfaces;
using System;

namespace OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Logger
{
    public static class LoggerExtensions
    {
        public static ILoggerFactory AddContext(this ILoggerFactory factory,
            Func<string, LogLevel, bool> filter = null, string cnString = null)
        {
            factory.AddProvider(new LoggerProvider(filter, cnString));
            return factory;
        }

        public static ILoggerFactory AddContext(this ILoggerFactory factory, LogLevel minLevel, string cnString)
        {
            return AddContext(factory, (_, logLevel) => logLevel >= minLevel, cnString);
        }
    }
}
