using System;

namespace Patterns.RequestHandlerResponse
{
    public interface IHandler : IDisposable
    {
        void Handle(HandlerContext context);
    }

    public interface IHandlerFor<TRequest> : IHandler
    {
        
    }
}
