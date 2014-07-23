using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.OptimizationSensibles
{
    public sealed class BlockOptimizationWithVolatileReadWriteExample : OptimizationSensibleBase, ISolutionFor<OptimizationSensibleExample>
    {
        public const string ConfirmCountingStopMessage = "ConfirmCountingStop";
        public const string FlagHasBeenSet = "FlagHasBeenSet";
        public const string CountingFinished = "CountingFinished";

        private bool _stopCounting = false;
        protected override void OnRun()
        {
            // NOTE: Run this example in debug and later in release
            ThreadPool.QueueUserWorkItem(_ => Count());

            ConfirmCountingStop();

            StopCounting();
        }

        private void ConfirmCountingStop()
        {
            Log.Info(ConfirmCountingStopMessage);
            ConsoleInput.ReadLine();
        }

        private void StopCounting()
        {
            Volatile.Write(ref _stopCounting, true);
            Log.Info(FlagHasBeenSet);
        }

        private void Count()
        {
            byte count = 0;
            while (!Volatile.Read(ref _stopCounting))
            {
                ++count;
            }

            Log.Info(CountingFinished);
        }
    }
}