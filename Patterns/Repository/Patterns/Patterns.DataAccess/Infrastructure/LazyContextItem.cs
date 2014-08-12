using System;

namespace Patterns.DataAccess.Infrastructure
{
    public sealed class LazyContextItem<TContext> : ContextItemBase
    {
        private readonly Action<TContext> _saveHandler = null;
        private readonly Action<TContext> _disposeHandler = null;
        private readonly Lazy<TContext> _lazyContext = null;

        public LazyContextItem(Func<TContext> contextFactory, string name = null, Action<TContext> saveHandler = null, Action<TContext> disposeHandler = null) : base(name ?? typeof(TContext).FullName)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException("contextFactory");
            }

            _lazyContext = new Lazy<TContext>(valueFactory: contextFactory, isThreadSafe: true);
            _saveHandler = saveHandler;
            _disposeHandler = disposeHandler;
        }

        public override object Context
        {
            get { return _lazyContext.Value; }
        }

        protected override void OnSave()
        {
            if (!_lazyContext.IsValueCreated)
            {
                return;
            }

            if (_saveHandler != null)
            {
                _saveHandler(_lazyContext.Value);
            }
        }

        protected override void OnDispose()
        {
            if (_disposeHandler == null || !_lazyContext.IsValueCreated)
            {
                return;
            }

            _disposeHandler(_lazyContext.Value);
        }
    }
}