using System;
using System.Threading;
using MultithreadingExamples.Tests.Infrastructure.StateObservers;

namespace MultithreadingExamples.Tests.Infrastructure
{
    public sealed class CatchStateObserver : StateObserverBase
    {
        public CatchStateObserver(Predicate<string> state) : base(state)
        {
        }

        public bool WasObserved { get; private set; }

        private int _count;
        public int Count
        {
            get { return _count; }
        }

        private ManualResetEvent _resetEvent = new ManualResetEvent(false);
        protected override void OnStateSatisfied(string state)
        {
            Interlocked.Increment(ref _count);
            WasObserved = true;
            _resetEvent.Set();
        }

        public bool Wait(int timeout)
        {
            return _resetEvent.WaitOne(timeout);
        }
    }
}