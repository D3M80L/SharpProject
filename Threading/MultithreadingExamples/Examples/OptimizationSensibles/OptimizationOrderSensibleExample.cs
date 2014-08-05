using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.OptimizationSensibles
{
    /// <summary>
    /// Note, this is a theoretical implementation.
    /// It is hard to simulate the behaviour.
    /// In the example I am creating two threads. Please note, that creating a thread
    /// takes few resources. During the creation - there may be enough time to
    /// refresh the data and 'synchronize' all values.
    /// </summary>
    public sealed class OptimizationOrderSensibleExample : OptimizationSensibleBase, IImportantExample
    {
        private bool someOtherFlag;
        private bool flag;
        private long result;
        private int anotherResult;

        protected override void OnRun()
        {
            for (int i = 0; i < 100; i++)
            {
                ThreadPool.QueueUserWorkItem(_ => Read());
            }

            Set();
        }

        public void Set()
        {
            result = 8;
            someOtherFlag = false;
            anotherResult = 1;
            flag = true; // may be optimized and reordered
        }

        public void Read()
        {
            if (flag)
            {
                long y = result + 1;
                Log.Info(y.ToString()); // NOTE: the value may be 1
            }
        }
    }
}