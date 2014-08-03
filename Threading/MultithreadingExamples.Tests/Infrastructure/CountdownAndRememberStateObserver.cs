using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MultithreadingExamples.Tests.Infrastructure
{
    public sealed class CountdownAndRememberStateObserver : CountdownStateObserverBase
    {
        private readonly ConcurrentBag<string> _states = new ConcurrentBag<string>();

        public CountdownAndRememberStateObserver(Predicate<string> statePredicate, int countdown) : base(statePredicate, countdown)
        {
        }

        protected override void OnBeforeSignal(string message)
        {
            _states.Add(message);
        }

        public IEnumerable<string> GetStates()
        {
            return _states.AsEnumerable();
        }
    }
}