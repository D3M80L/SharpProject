using System;

namespace Patterns.DataAccess
{
    public interface IDisposableUnitOfWork : IUnitOfWork, IDisposable
    {
        
    }
}