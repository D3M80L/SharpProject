using System;

namespace Patterns.Log
{
    public sealed class LogFactory
    {
        public static ILog GetFor<T>()
        {
            var logBuilder = LogContainer.GetBuilderFor<T>();
            if (logBuilder == null)
            {
                return null;
            }

            return logBuilder.GetFor<T>();
        }

        public static ILog GetFor(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            var logBuilder = LogContainer.GetBuilderFor(type);
            if (logBuilder == null)
            {
                return null;
            }

            return logBuilder.GetFor(type);
        }
    }
}