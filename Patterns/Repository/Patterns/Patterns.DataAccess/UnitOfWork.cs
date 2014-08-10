using System;

namespace Patterns.DataAccess
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        public TContext Context<TContext>() where TContext : class
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}