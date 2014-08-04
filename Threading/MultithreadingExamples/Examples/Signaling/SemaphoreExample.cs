using System.Threading;
using MultithreadingExamples.Infrastructure;

namespace MultithreadingExamples.Examples.Signaling
{
    public sealed class SemaphoreExample : SemaphoreExampleBase, IRelatedWith<SemaphoreSlimExample>
    {
        private readonly Semaphore _semaphore = new Semaphore(initialCount: 2, maximumCount: 4, name: "MySemaphore"); // Spans multiple processes

        protected override void SemaphoreWaitOne()
        {
            _semaphore.WaitOne();
        }

        protected override void SemaphoreRelease()
        {
            _semaphore.Release();
        }
    }
}