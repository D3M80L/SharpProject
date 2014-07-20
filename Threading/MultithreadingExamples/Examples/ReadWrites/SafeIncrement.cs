using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.ReadWrites
{
    public sealed class SafeIncrement : ReadWritesBase, ISolutionFor<UnsafeIncrement>
    {
        public long _x = 0;
        public long _y = 0;

        protected override void OnRun()
        {
            for (int i = 0; i < 200; i++)
            {
                ThreadPool.QueueUserWorkItem(callBack: _ => Increment());
            }

            Thread.Sleep(2000);

            Log.Info("X={0}", _x);
            Log.Info("Y={0}", _y);
        }

        private void Increment()
        {
            // note: Torn read - may happen too
            Interlocked.Increment(ref _x);
            Interlocked.Increment(ref _y);
        }
    }
}