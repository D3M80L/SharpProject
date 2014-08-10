using System;

namespace Patterns.Log
{
    public sealed class LogContainer
    {
        public static void RegisterBuilder(ILogBuilder logBuilder)
        {
            
        }

        public static void UnregisterBuilder(ILogBuilder logBuilder)
        {
            
        }

        public static ILogBuilder GetBuilderFor<T>()
        {
            throw new NotImplementedException();
        }

        public static ILogBuilder GetBuilderFor(Type type)
        {
            throw new NotImplementedException();
        }
    }
}