using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MultithreadingExamples.Tests.Infrastructure
{
    public sealed class CatchAndRememberStateObserver : CatchStateObserverBase
    {
        private readonly ConcurrentBag<string> _states = new ConcurrentBag<string>();

        public CatchAndRememberStateObserver(Predicate<string> statePredicate) : base(statePredicate)
        {
        }

        protected override void OnStateSatisfied(string state)
        {
            _states.Add(state);
            base.OnStateSatisfied(state);
        }

        public IEnumerable<string> GetStates()
        {
            return _states.AsEnumerable();
        }
    }
}