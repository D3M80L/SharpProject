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

        public object Handle(object request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            using (var handler = _handlerFactory.BuildHandlerFor(request.GetType()))
            {
                var handlerContext = new HandlerContext();
                handlerContext.Request = request;

                handler.Handle(handlerContext);

                return handlerContext.Response;
            }
        }
    }
}