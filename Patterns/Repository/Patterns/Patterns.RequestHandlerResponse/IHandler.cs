using System;

namespace Patterns.RequestHandlerResponse
{
    public interface IHandler : IDisposable
    {
    }

    public interface IHandlerFor<TRequest> : IHandler
    {
        
    }
}
