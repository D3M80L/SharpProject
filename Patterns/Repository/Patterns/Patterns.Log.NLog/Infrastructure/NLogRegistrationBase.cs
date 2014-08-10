using System;

namespace Patterns.Log.NLog.Infrastructure
{
    public abstract class NLogRegistrationBase : IDisposable
    {
        private readonly NLogAdapter _nLogAdapter;

        protected NLogRegistrationBase(NLogAdapter nLogAdapter)
        {
            _nLogAdapter = nLogAdapter;
        }

        public void Register()
        {
            LogContainer.RegisterBuilder(_nLogAdapter);
        }

        public void Dispose()
        {
            LogContainer.UnregisterBuilder(_nLogAdapter);
        }
    }
}