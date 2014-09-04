using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Patterns.RequestHandlerResponse
{
    public sealed class HandlerPerRequestTypeContainer
    {
        private static readonly Dictionary<Type, Func<IHandler>> HandlerMap = new Dictionary<Type, Func<IHandler>>();


        public static IHandler CreateInstanceUsingActivator(Type handlerType)
        {
            var result = Activator.CreateInstance(handlerType);

            return result as IHandler;
        }

        public void SetHandlerForType(Type type, Func<IHandler> handler)
        {
            HandlerMap.Add(type, handler);
        }

        public IHandler BuildHandlerFor(Type requestType)
        {
            Func<IHandler> handlerBuilder = null;

            HandlerMap.TryGetValue(requestType, out handlerBuilder);

            if (handlerBuilder == null)
            {
                return null;
            }

            return handlerBuilder();
        }

        public void RegisterAllHandlersFromAssembly(Assembly assembly)
        {
            var requestHandler = typeof (IHandlerFor<>);


            // TODO: improve this code : do not call GetInterfaces twice or get rid off modified closure

            var handlerImplementations = assembly
                .GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract)
                .Where(x => x.GetInterfaces().Any(a => a.IsGenericType &&  a.GetGenericTypeDefinition() == requestHandler));


            foreach (var handlerImplementation in handlerImplementations)
            {
                var requestType = handlerImplementation
                    .GetInterfaces()
                    .First(x => x.IsGenericType && x.GetGenericTypeDefinition() == requestHandler)
                    .GetGenericArguments()[0];

                SetHandlerForType(requestType, () => CreateInstanceUsingActivator(handlerImplementation));
            }
        }
    }
}