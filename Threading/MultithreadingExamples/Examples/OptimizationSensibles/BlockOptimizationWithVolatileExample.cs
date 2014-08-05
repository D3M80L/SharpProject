using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.OptimizationSensibles
{
    /// <summary>
    /// 
    /// Volatile keyword is not CLS compilant and available in C# and not in VB, what is more
    /// having a volatile field like
    /// private volatile int sum = 5;
    /// there is no guarantee, that the total sum
    /// sum = sum + sum;
    /// will be 10. Volatile fields does not allow bit shifting and few other operations.
    /// The volatile keyword gives no value on Intel x86 and x64 processors, where a fence is 
    /// used on read and writes.
    /// 
    /// For some details about volatile (why not to use it) go to:
    /// - http://blogs.msdn.com/b/ericlippert/archive/2011/06/16/atomicity-volatility-and-immutability-are-different-part-three.aspx
    /// - http://joeduffyblog.com/2010/12/04/sayonara-volatile/
    /// </summary>
    public sealed class BlockOptimizationWithVolatileExample : OptimizationSensibleCounterExampleBase, ISolutionFor<OptimizationSensibleExample>
    {
        private volatile bool _stopCounting;

        protected override void StopCounting()
        {
            _stopCounting = true;
            Log.Info(FlagHasBeenSet);
        }

        protected override void Count()
        {
            byte count = 0;
            while (!_stopCounting)
            {
                ++count;
            }

            Log.Info(CountingFinished);
        }
    }
}