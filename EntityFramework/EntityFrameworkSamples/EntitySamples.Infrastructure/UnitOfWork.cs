using System;

namespace EntitySamples.Infrastructure
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private SampleContext _sampleContext = new SampleContext();
        public TContext Context<TContext>()
            where TContext : class
        {
            if (typeof(TContext) == typeof(SampleContext))
            {
                return _sampleContext as TContext;
            }
            
            throw new ArgumentException("Unknown context");
        }

        public void Save()
        {
            _sampleContext.SaveChanges();
        }

        public void Dispose()
        {
            _sampleContext.Dispose();
            _sampleContext = null;
        }
    }
}