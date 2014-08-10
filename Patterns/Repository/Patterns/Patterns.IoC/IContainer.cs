using System;

namespace Patterns.IoC
{
    public interface IContainer : IDisposable
    {
        T Resolve<T>();
    }
}