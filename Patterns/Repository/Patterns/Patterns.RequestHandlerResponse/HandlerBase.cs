using System;

namespace Patterns.RequestHandlerResponse
{
    public abstract class HandlerBase : IHandler
    {
        public void Dispose()
        {
            try
            {
                OnDispose();  
            } catch
            { }
        }

        public void Handle(HandlerContext context)
        {
            
        }

        protected virtual void OnDispose()
        {
            
        }
    }

    public abstract class HandlerBase<TRequest> : HandlerBase, IHandlerFor<TRequest>
    {
        
    }
}