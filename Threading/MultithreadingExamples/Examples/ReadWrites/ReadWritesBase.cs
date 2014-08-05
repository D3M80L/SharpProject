using System.Threading;
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
