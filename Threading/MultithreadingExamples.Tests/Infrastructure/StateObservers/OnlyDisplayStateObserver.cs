using System;
using System.Threading;

namespace MultithreadingExamples.Tests.Infrastructure.StateObservers
{
    public sealed class OnlyDisplayStateObserver : StateObserverBase
    {
        public OnlyDisplayStateObserver() : base(statePredicate: AnyState)
        {
        }

        private readonly object _padLock = new object();

        protected override void OnStateSatisfied(string state)
        {
            lock (_padLock)
            {
                Console.WriteLine("State:{0} ThreadId={1}", state, Thread.CurrentThread.ManagedThreadId);
            }
        }
    }
}