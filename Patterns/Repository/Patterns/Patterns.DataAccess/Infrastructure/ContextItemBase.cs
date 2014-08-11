using System;

namespace Patterns.DataAccess.Infrastructure
{
    public abstract class ContextItemBase
    {
        protected ContextItemBase(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public abstract object Context { get; }

        public void Save()
        {
            OnSave();
        }

        protected abstract void OnSave();
    }

    public sealed class ContextItem<TContext> : ContextItemBase
        where TContext : class
    {
        private readonly Action<TContext> _saveAction = null;
        private readonly TContext _context = null;

        public ContextItem(TContext context, Action<TContext> saveAction = null) : this(context, name: typeof(TContext).FullName, saveAction: saveAction)
        {
            
        }

        public ContextItem(TContext context, string name, Action<TContext> saveAction = null) : base(name)
        {
            _saveAction = saveAction;
            _context = context;
        }

        public override object Context
        {
            get { return _context; }
        }

        protected override void OnSave()
        {
            if (_saveAction != null)
            {
                _saveAction(_context);
            }
        }
    }
}