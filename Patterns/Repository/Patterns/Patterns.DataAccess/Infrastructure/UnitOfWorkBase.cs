using System;
using System.Collections.Generic;
using System.Linq;

namespace Patterns.DataAccess.Infrastructure
{
    public abstract class UnitOfWorkBase : IUnitOfWork, IDisposable
    {
        private readonly object _padLock = new object();
        protected readonly Dictionary<string, ContextItemBase> ContextContainer = new Dictionary<string, ContextItemBase>();

        public TContext Context<TContext>(string name)
            where TContext : class
        {
            ContextItemBase context = null;
            if (!ContextContainer.TryGetValue(BuildContextKey<TContext>(name), out context))
            {
                throw new InvalidOperationException("Specified context not found.");
            }

            if (context.Context is TContext)
            {
                return context.Context as TContext;
            }

            throw new InvalidOperationException("Registered context is of another type.");
        }

        public TContext Context<TContext>() where TContext : class
        {
            return Context<TContext>(name: null);
        }

        public void Save()
        {
            lock (_padLock)
            {
                ContextContainer.Values
                    .ToList()
                    .ForEach(contextItem =>
                    {
                        contextItem.Save();
                    });
            }
        }

        protected void RegisterContext(ContextItemBase contextItemBase)
        {
            ContextContainer.Add(contextItemBase.Name, contextItemBase);
        }

        private static string BuildContextKey<TContext>(string name) where TContext : class
        {
            return name ?? typeof(TContext).FullName;
        }

        public void Dispose()
        {
            ContextContainer.Clear();
            // TODO:
        }
    }
}