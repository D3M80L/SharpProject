using System.Threading;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Threads
{
    public sealed class ThreadInterrupdedExceptionExample : ThreadExampleBase
    {
        protected override void OnRun()
        {
            var thread = StartThread();

            Thread.Sleep(1000);

            Log.Info("Interrupting");
            thread.Interrupt();
            Thread.Sleep(1000);
        }

        private Thread StartThread()
        {
            var thread = new Thread(start: RunInThread);
            thread.IsBackground = true;

            Log.Info(StartingThread);
            thread.Start();

            return thread;
        }

        private void RunInThread()
        {
            try
            {
                while (true)
                {
                    Log.Info(".");
                    Thread.Sleep(250);
                }
            }
            catch (ThreadInterruptedException)
            {
                Log.Info("ThreadInterruptedException");
            }

            Log.Info("SomeOtherWork");
        }
    }
}