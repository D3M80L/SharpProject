using System;

namespace Patterns.DataAccess.Repositories
{
    public abstract class RepositoryBase : IRepository, IDisposable
    {
        protected IUnitOfWork UnitOfWork { get; private set; }

        protected RepositoryBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        private bool _isDisposed = false;

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }
            _isDisposed = true;

            try
            {
                OnDispose();
            }
            catch
            {
                
            }
        }

        protected virtual void OnDispose()
        {
            
        }
    }
}