using System;

namespace Patterns.RequestHandlerResponse
{
    public interface IHandlerFactory
    {
        IHandler GetHandlerFor(Type requestType);
        void SetHandlerForType(Type type, IHandler handler);
    }
}