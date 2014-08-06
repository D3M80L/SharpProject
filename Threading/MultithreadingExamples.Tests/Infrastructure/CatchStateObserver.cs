using System;

namespace MultithreadingExamples.Tests.Infrastructure
{
    public sealed class CatchStateObserver : CatchStateObserverBase
    {
        public CatchStateObserver(Predicate<string> state) : base(state)
        {
        }
    }
}