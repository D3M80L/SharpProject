using System;

namespace Patterns.Log.Extensions
{
    public static class LogExtensions
    {
        public static bool TraceIsEnabled(this ILog log)
        {
            return InternalTraceIsEnabled(log, LogLevel.Trace);
        }

        public static void Trace(this ILog log, string message)
        {
            InternalLog(log, LogLevel.Trace, message);
        }

        public static void Trace(this ILog log, string message, Exception exception)
        {
            InternalLog(log, LogLevel.Trace, message, exception);
        }

        public static bool InfoIsEnabled(this ILog log)
        {
            return InternalTraceIsEnabled(log, LogLevel.Info);
        }

        public static void Info(this ILog log, string message)
        {
            InternalLog(log, LogLevel.Info, message);
        }

        public static void Info(this ILog log, string message, Exception exception)
        {
            InternalLog(log, LogLevel.Info, message, exception);
        }

        public static bool ErrorIsEnabled(this ILog log)
        {
            return InternalTraceIsEnabled(log, LogLevel.Error);
        }

        public static void Error(this ILog log, string message)
        {
            InternalLog(log, LogLevel.Error, message);
        }

        public static void Error(this ILog log, string message, Exception exception)
        {
            InternalLog(log, LogLevel.Error, message, exception);
        }

        private static void InternalLog(ILog log, LogLevel logLevel, string message, Exception exception = null)
        {
            if (log == null)
            {
                return;
            }
            log.Log(logLevel, message, exception);
        }

        private static bool InternalTraceIsEnabled(ILog log, LogLevel logLevel)
        {
            if (log == null)
            {
                return false;
            }

            return log.IsEnabled(logLevel);
        }
    }
}