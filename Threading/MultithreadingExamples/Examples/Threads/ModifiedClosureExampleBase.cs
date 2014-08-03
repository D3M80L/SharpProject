using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Threads
{
    public abstract class ModifiedClosureExampleBase : ThreadExampleBase
    {
        public const int HowManyThreadsToUse = 100;
        public const string RunInThreadMessage = "RunInThreadMessage";

        protected void RunInThread(int i)
        {
            Log.Info(RunInThreadMessage + i);
        }
    }
}