using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.OptimizationSensibles
{
    public sealed class BlockOptimizationWithMemoryBarierExample : OptimizationSensibleCounterExampleBase, ISolutionFor<OptimizationSensibleExample>
    {
        private bool _stopCounting = false;

        protected override void StopCounting()
        {
            Thread.MemoryBarrier();
            _stopCounting = true;
            Thread.MemoryBarrier();

            Log.Info(FlagHasBeenSet);
        }

        protected override void Count()
        {
            byte count = 0;
            Thread.MemoryBarrier();
            while (!_stopCounting)
            {
                Thread.MemoryBarrier();
                ++count;
            }

            Log.Info(CountingFinished);
        }

    }
}