using System.Threading;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.OptimizationSensibles
{
    public abstract class OptimizationSensibleCounterExampleBase : OptimizationSensibleBase
    {
        public const string ConfirmCountingStopMessage = "ConfirmCountingStop";
        public const string FlagHasBeenSet = "FlagHasBeenSet";
        public const string CountingFinished = "CountingFinished";

        protected sealed override void OnRun()
        {
            // NOTE: Run this example in debug and later in release
            ThreadPool.QueueUserWorkItem(_ => Count());

            ConfirmCountingStop();

            StopCounting();
        }

        protected void ConfirmCountingStop()
        {
            Log.Info(ConfirmCountingStopMessage);
            ConsoleInput.ReadLine();
        }

        protected abstract void StopCounting();

        protected abstract void Count();
    }
}