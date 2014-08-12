using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Patterns.DataAccess.Infrastructure
{
    public abstract class UnitOfWorkBase : IDisposableUnitOfWork
    {
        private readonly object _padLock = new object();
        protected readonly Dictionary<string, ContextItemBase> ContextContainer = new Dictionary<string, ContextItemBase>();

        public TContext Context<TContext>(string name = null)
            where TContext : class
        {
            ThrowIfDisposed();

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

        public void Save()
        {
            ThrowIfDisposed();

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
            if (contextItemBase == null)
            {
                throw new ArgumentNullException("contextItemBase");
            }

            ContextContainer.Add(contextItemBase.Name, contextItemBase);
        }

        private bool _isDisposed = false;

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }
            _isDisposed = true;

            ContextContainer.Values
                   .ToList()
                   .ForEach(contextItem =>
                    {
                        contextItem.Dispose();
                    });

            ContextContainer.Clear();
        }

        [DebuggerHidden]
        protected void ThrowIfDisposed()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        private static string BuildContextKey<TContext>(string name) where TContext : class
        {
            return name ?? typeof(TContext).FullName;
        }
    }
}