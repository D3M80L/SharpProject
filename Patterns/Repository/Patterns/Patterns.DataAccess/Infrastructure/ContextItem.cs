using System;

namespace Patterns.DataAccess.Infrastructure
{
    public sealed class ContextItem<TContext> : ContextItemBase
        where TContext : class
    {
        private readonly Action<TContext> _saveHandler = null;
        private readonly Action<TContext> _disposeHandler = null;
        private readonly TContext _context = null;

        public ContextItem(TContext context, string name = null, Action<TContext> saveHandler = null, Action<TContext> disposeHandler = null) : base(name ?? typeof(TContext).FullName)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;
            _saveHandler = saveHandler;
            _disposeHandler = disposeHandler;
        }

        public override object Context
        {
            get { return _context; }
        }

        protected override void OnSave()
        {
            if (_saveHandler != null)
            {
                _saveHandler(_context);
            }
        }

        protected override void OnDispose()
        {
            if (_disposeHandler == null)
            {
                return;
            }

            _disposeHandler(_context);
        }
    }
}