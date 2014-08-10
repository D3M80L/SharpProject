using System;
using System.Collections.Generic;

namespace Patterns.RequestHandlerResponse
{
    public sealed class HandlerFactory : HandlerFactoryBase
    {
        private readonly HandlerPerRequestTypeContainer _handlerContainer = new HandlerPerRequestTypeContainer ();

        public override IHandler GetHandlerFor(Type requestType)
        {

            var handler = _handlerContainer.GetHandlerFor(requestType);

            if (handler != null)
            {
                return handler;
            }
            throw new InvalidOperationException("No handler registered for specified requestType");
        }

        public override void SetHandlerForType(Type type, IHandler handler)
        {

            _handlerContainer.SetHandlerForType(type, handler);
        }
    }

    public abstract class HandlerFactoryBase : IHandlerFactory
    {
        public abstract IHandler GetHandlerFor(Type requestType);
        public abstract void SetHandlerForType(Type type, IHandler handler);
    }
}
