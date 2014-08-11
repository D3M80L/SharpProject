using System;

namespace Patterns.DataAccess.Infrastructure
{
    public class OneContextedUnitOfWork<TUnitOfWorkContext> : UnitOfWorkBase
        where TUnitOfWorkContext : class
    {
        public OneContextedUnitOfWork(TUnitOfWorkContext unitOfWorkContext, Action<TUnitOfWorkContext> saveAction)
        {
            RegisterContext(new ContextItem<TUnitOfWorkContext>(unitOfWorkContext, saveAction));
        }

        public OneContextedUnitOfWork(Func<TUnitOfWorkContext> unitOfWorkContextFactory, Action<TUnitOfWorkContext> saveAction)
        {
            RegisterContext(new LazyContextItem<TUnitOfWorkContext>(unitOfWorkContextFactory, saveAction: saveAction));
        }
    }
}