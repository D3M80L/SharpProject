using System;

namespace Patterns.RequestHandlerResponse
{
    public sealed class RequestHandler
    {
        private readonly IHandlerFactory _handlerFactory;

        public RequestHandler(IHandlerFactory handlerFactory)
        {
            _handlerFactory = handlerFactory;
        }

        public TResponse Handle<TRequest, TResponse>(TRequest request)
            where TRequest : class 
            where TResponse : class 
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            using (var handler = _handlerFactory.GetHandlerFor(request.GetType()))
            {
                // TODO: send request to handler

                return default (TResponse);
            }
        }
    }
}