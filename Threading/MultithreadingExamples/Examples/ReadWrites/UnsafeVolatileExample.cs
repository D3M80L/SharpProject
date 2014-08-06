using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.ReadWrites
{
    public sealed class UnsafeVolatileExample : ReadWritesBase, IImportantExample, IHasSolutionIn<SafeIncrementExample>
    {
        private long _x = 0;
        private long _y = 0;

        protected override void OnRun()
        {
            for (int i = 0; i < 200; i++)
            {
                ThreadPool.QueueUserWorkItem(_ => Increment());
            }

            WaitForFinish();

            Log.Info("X={0}", _x);
            Log.Info("Y={0}", _y);
        }

        private void Increment()
        {
            // note: Torn read - may happen too
            ++_x;
            ++_y;

            Notify();
        }
    }
}