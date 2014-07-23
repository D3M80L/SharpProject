using System;

namespace MultithreadingExamples.Tests.Infrastructure.StateObservers
{
    public abstract class StateObserverBase : IStateObserver
    {
        public static bool AnyState(string state)
        {
            return true;
        }

        protected readonly Predicate<string> StatePredicate;
        protected StateObserverBase(Predicate<string> statePredicate)
        {
            StatePredicate = statePredicate;
        }

        public virtual bool Satisfies(string state)
        {
            return StatePredicate(state);
        }

        public void State(string state)
        {
            if (Satisfies(state))
            {
                OnStateSatisfied(state);
            }
        }

        protected abstract void OnStateSatisfied(string state);
    }
}