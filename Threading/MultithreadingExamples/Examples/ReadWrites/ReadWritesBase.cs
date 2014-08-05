using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure;

namespace MultithreadingExamples.Examples.ReadWrites
{
    public abstract class ReadWritesBase : ExampleBase
    {
        public const int HowManyThreadsToUse = 200;
        public const string XMessage = "X=";
        public const string YMessage = "Y=";

        private CountdownEvent _countdownEvent = new CountdownEvent(HowManyThreadsToUse);

        protected void WaitForFinish()
        {
            _countdownEvent.Wait();
        }

        protected void Notify()
        {
            _countdownEvent.Signal();
        }
    }
}
