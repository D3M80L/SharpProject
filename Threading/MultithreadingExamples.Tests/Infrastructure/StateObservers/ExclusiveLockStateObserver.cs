using System;

namespace MultithreadingExamples.Tests.Infrastructure.StateObservers
{
    public sealed class ExclusiveLockStateObserver : LockingStateObserver
    {
        private volatile bool _lockEveryone = false;

        public ExclusiveLockStateObserver(Predicate<string> statePredicate) : base(statePredicate)
        {
        }

        public override bool Satisfies(string state)
        {
            return _lockEveryone || base.Satisfies(state);
        }

        private readonly object _exclusivePadLock = new object();
        protected override void OnStateSatisfied(string state)
        {
            lock (_exclusivePadLock)
            {
                if (base.Satisfies(state))
                {
                    try
                    {
                        _lockEveryone = true;
                        base.OnStateSatisfied(state);
                    }
                    finally
                    {
                        _lockEveryone = false;
                    }
                }
            }
        }
    }
}