using System;

namespace Patterns.Log
{
    public interface ILogBuilder
    {
        ILog GetFor<T>();

        ILog GetFor(Type type);
    }
}