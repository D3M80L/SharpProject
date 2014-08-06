using System;
using System.Threading;
using MultithreadingExamples.Tests.Infrastructure.StateObservers;

namespace MultithreadingExamples.Tests.Infrastructure
{
    public abstract class CountdownStateObserverBase : StateObserverBase
    {
        protected readonly CountdownEvent _countdownEvent;

        protected CountdownStateObserverBase(Predicate<string> statePredicate, int countdown) : base(statePredicate)
        {
            _countdownEvent = new CountdownEvent(countdown);
        }

        protected override void OnStateSatisfied(string state)
        {
            if (!_countdownEvent.IsSet)
            {
                OnBeforeSignal(state);
                _countdownEvent.Signal();
            }
        }

        protected virtual void OnBeforeSignal(string message)
        {
            
        }

        public bool Wait(int timeout)
        {
            return _countdownEvent.Wait(timeout);
        }
    }
}