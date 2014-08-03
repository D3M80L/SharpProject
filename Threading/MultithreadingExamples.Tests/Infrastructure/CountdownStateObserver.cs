using System;

namespace MultithreadingExamples.Tests.Infrastructure
{
    public sealed class CountdownStateObserver : CountdownStateObserverBase
    {
        public CountdownStateObserver(Predicate<string> statePredicate, int countdown) : base(statePredicate, countdown)
        {
        }
    }
}