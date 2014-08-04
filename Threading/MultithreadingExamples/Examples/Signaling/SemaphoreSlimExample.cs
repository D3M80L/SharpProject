using System.Threading;
using MultithreadingExamples.Infrastructure;

namespace MultithreadingExamples.Examples.Signaling
{
    public sealed class SemaphoreSlimExample : SemaphoreExampleBase, IRelatedWith<SemaphoreExample>
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(initialCount: 2, maxCount: 4); // Used only in one process

        protected override void SemaphoreWaitOne()
        {
            _semaphore.Wait();
        }

        protected override void SemaphoreRelease()
        {
            _semaphore.Release();
        }
    }
}