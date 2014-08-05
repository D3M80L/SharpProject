using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.OptimizationSensibles
{
    public sealed class CustomSpinningExample : OptimizationSensibleCounterExampleBase, IImportantExample
    {
        private bool _stopCounting = false;

        protected override void StopCounting()
        {
            _stopCounting = true;
            Log.Info(FlagHasBeenSet);
        }

        protected override void Count()
        {
            while (!_stopCounting) // Optimization sensible
            {
                Thread.Sleep(50);
            }

            Log.Log(CountingFinished);
        }
    }
}