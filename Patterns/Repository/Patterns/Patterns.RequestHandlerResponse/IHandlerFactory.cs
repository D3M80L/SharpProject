using System;

namespace Patterns.RequestHandlerResponse
{
    public interface IHandlerFactory
    {
        IHandler BuildHandlerFor(Type requestType);
        void SetHandlerForType(Type type, IHandler handler);
    }
}