using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.OptimizationSensibles
{
    public sealed class BlockOptimizationWithVolatileReadWriteExample : OptimizationSensibleCounterExampleBase, ISolutionFor<OptimizationSensibleExample>
    {
        private bool _stopCounting;

        protected override void StopCounting()
        {
            Volatile.Write(ref _stopCounting, true);
            Log.Info(FlagHasBeenSet);
        }

        protected override void Count()
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