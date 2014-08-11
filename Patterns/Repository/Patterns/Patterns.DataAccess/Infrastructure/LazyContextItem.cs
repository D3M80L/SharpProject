using System;

namespace Patterns.DataAccess.Infrastructure
{
    public sealed class LazyContextItem<TContext> : ContextItemBase
    {
        private readonly Action<TContext> _saveAction = null;
        private readonly Lazy<TContext> _lazyContext = null;

        public LazyContextItem(Func<TContext> contextFactory, string name = null, Action<TContext> saveAction = null) : base(name ?? typeof(TContext).FullName)
        {
            _lazyContext = new Lazy<TContext>(valueFactory: contextFactory, isThreadSafe: true);
            _saveAction = saveAction;
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

            if (_saveAction != null)
            {
                _saveAction(_lazyContext.Value);
            }
        }
    }
}