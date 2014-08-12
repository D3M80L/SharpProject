using System;

namespace Patterns.DataAccess.Infrastructure
{
    public abstract class ContextItemBase : IDisposable
    {
        protected ContextItemBase(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
        }

        public string Name { get; private set; }

        public abstract object Context { get; }

        public void Save()
        {
            OnSave();
        }

        protected abstract void OnSave();

        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            try
            {
                OnDispose();
            }
            catch
            {
                
            }
        }

        protected abstract void OnDispose();
    }
}