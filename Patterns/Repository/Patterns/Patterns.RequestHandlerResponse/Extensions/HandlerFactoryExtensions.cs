namespace Patterns.RequestHandlerResponse.Extensions
{
    public static class HandlerFactoryExtensions
    {
        public static IHandlerFactory SetHandlerFor<TRequest>(this IHandlerFactory handlerFactory, IHandler handler)
            where TRequest : class
        {
            handlerFactory.SetHandlerForType(typeof(TRequest), handler);
            return handlerFactory;
        }
    }
}