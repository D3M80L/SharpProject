using System;
using System.Threading;

namespace MultithreadingExamples.Tests.Infrastructure.StateObservers
{
    public class LockingStateObserver : StateObserverBase
    {
        private AutoResetEvent _blockingStateHandled = new AutoResetEvent(false);
        private AutoResetEvent _predicateAccepted = new AutoResetEvent(false);

        public LockingStateObserver(Predicate<string> statePredicate) : base(statePredicate)
        {
        }

        private object _padLock = new object();
        protected override void OnStateSatisfied(string state)
        {
            lock (_padLock)
            {
                _predicateAccepted.Set();
                _blockingStateHandled.WaitOne();
            }
        }

        public bool Wait(int timeout, Action action)
        {
            if (!_predicateAccepted.WaitOne(timeout))
            {
                return false;
            }

            action();
            _blockingStateHandled.Set();

            return true;
        }
    }
}