using System;
using System.Collections.Generic;

namespace Patterns.RequestHandlerResponse
{
    public sealed class HandlerPerRequestTypeContainer
    {
        private static readonly Dictionary<Type, IHandler> HandlerMap = new Dictionary<Type, IHandler>();

        public void SetHandlerForType(Type type, IHandler handler)
        {
            HandlerMap.Add(type, handler);
        }

        public IHandler GetHandlerFor(Type requestType)
        {
            IHandler handler = null;

            HandlerMap.TryGetValue(requestType, out handler);

            return handler;
        }
    }
}