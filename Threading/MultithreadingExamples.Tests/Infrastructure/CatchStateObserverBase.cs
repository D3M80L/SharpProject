using System;
using System.Threading;
using MultithreadingExamples.Tests.Infrastructure.StateObservers;

namespace MultithreadingExamples.Tests.Infrastructure
{
    /// <summary>
    /// Base implementation - counts how many states were observed
    /// </summary>
    public abstract class CatchStateObserverBase : StateObserverBase
    {
        protected CatchStateObserverBase(Predicate<string> statePredicate) : base(statePredicate)
        {
        }

        public bool WasObserved { get; private set; }

        private int _count;
        public int Count
        {
            get { return _count; }
        }

        private readonly ManualResetEvent _resetEvent = new ManualResetEvent(false);
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

        public bool Wait()
        {
            return _resetEvent.WaitOne();
        }

    }
}