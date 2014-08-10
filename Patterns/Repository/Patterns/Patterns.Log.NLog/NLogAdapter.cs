using System;

namespace Patterns.Log.NLog
{
    public sealed class NLogAdapter : ILogBuilder
    {
        public ILog GetFor<T>()
        {
            throw new NotImplementedException();
        }

        public ILog GetFor(Type type)
        {
            throw new NotImplementedException();
        }
    }
}