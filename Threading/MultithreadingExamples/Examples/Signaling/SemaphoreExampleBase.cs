using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Signaling
{
    public abstract class SemaphoreExampleBase : ExampleBase
    {
        public const string BeforeWaitOne = "BeforeWaitOne";
        public const string AfterWaitOne = "AfterWaitOne";
        public const string AfterRelease = "AfterRelease";
        public const int HowManyThreadsToUse = 100;

        protected override void OnRun()
        {
            for (int i = 0; i < HowManyThreadsToUse; ++i)
            {
                ThreadPool.QueueUserWorkItem(_ => RunInThread());
            }
        }

        private void RunInThread()
        {
            try
            {
                Log.Info(BeforeWaitOne);
                SemaphoreWaitOne();

                Log.Info(AfterWaitOne);
            }
            finally
            {
                SemaphoreRelease();
                Log.Info(AfterRelease);
            }
        }

        protected abstract void SemaphoreWaitOne();

        protected abstract void SemaphoreRelease();
    }
}