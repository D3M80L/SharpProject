using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.OptimizationSensibles
{
    /// <summary>
    /// NOTE: compilation for x86 and x64 may be different
    /// </summary>
    public sealed class OptimizationSensibleExample : OptimizationSensibleCounterExampleBase, IImportantExample, IHasSolutionIn<BlockOptimizationWithVolatileExample>
    {
        private bool _stopCounting = false;

        protected override void StopCounting()
        {
            _stopCounting = true;
            Log.Info(FlagHasBeenSet);
        }

        protected override void Count()
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