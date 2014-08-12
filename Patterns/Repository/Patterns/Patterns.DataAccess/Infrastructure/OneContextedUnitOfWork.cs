using System;

namespace Patterns.DataAccess.Infrastructure
{
    public class OneContextedUnitOfWork<TUnitOfWorkContext> : UnitOfWorkBase
        where TUnitOfWorkContext : class
    {
        public OneContextedUnitOfWork(TUnitOfWorkContext unitOfWorkContext, Action<TUnitOfWorkContext> saveHandler, Action<TUnitOfWorkContext> disposeHandler = null)
        {
            RegisterContext(new ContextItem<TUnitOfWorkContext>(unitOfWorkContext, saveHandler: saveHandler, disposeHandler: disposeHandler));
        }

        public OneContextedUnitOfWork(Func<TUnitOfWorkContext> unitOfWorkContextFactory, Action<TUnitOfWorkContext> saveHandler, Action<TUnitOfWorkContext> disposeHandler = null)
        {
            RegisterContext(new LazyContextItem<TUnitOfWorkContext>(unitOfWorkContextFactory, saveHandler: saveHandler, disposeHandler: disposeHandler));
        }
    }
}