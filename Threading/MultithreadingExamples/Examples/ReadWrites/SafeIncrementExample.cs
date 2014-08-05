using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.ReadWrites
{
    public sealed class SafeIncrementExample : ReadWritesBase, ISolutionFor<UnsafeIncrementExample>
    {
        public long _x = 0;
        public long _y = 0;

        protected override void OnRun()
        {
            for (int i = 0; i < HowManyThreadsToUse; ++i)
            {
                ThreadPool.QueueUserWorkItem(callBack: _ => Increment());
            }

            WaitForFinish();

            Log.Info("X={0}", _x);
            Log.Info("Y={0}", _y);
        }

        private void Increment()
        {
            Interlocked.Increment(ref _x);
            Interlocked.Increment(ref _y);

            Notify();
        }
    }
}