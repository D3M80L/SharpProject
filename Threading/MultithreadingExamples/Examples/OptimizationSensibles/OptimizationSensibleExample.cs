using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.OptimizationSensibles
{
    /// <summary>
    /// NOTE: compilation for x86 and x64 may be different
    /// </summary>
    public sealed class OptimizationSensibleExample : OptimizationSensibleBase, IImportantExample, IHasSolutionIn<BlockOptimizationWithVolatileExample>
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

        private void StopCounting()
        {
            _stopCounting = true;
            Log.Info(FlagHasBeenSet);
        }

        private void ConfirmCountingStop()
        {
            Log.Info(ConfirmCountingStopMessage);
            ConsoleInput.ReadLine();
        }

        private void Count()
        {
            byte count = 0;
            while (!_stopCounting) // Optimization sensible
            {
                ++count;
            }

            Log.Log(CountingFinished);
        }
    }
}