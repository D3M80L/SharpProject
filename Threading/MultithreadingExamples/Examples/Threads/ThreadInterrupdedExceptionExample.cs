using System.Threading;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Threads
{
    public sealed class ThreadInterrupdedExceptionExample : ThreadExampleBase
    {
        public const string ThreadInterruptedExceptionMessage = "ThreadInterruptedExceptionMessage";
        public const string ExitingWorkerThread = "ExitingWorkerThread";

        protected override void OnRun()
        {
            var thread = StartThread();

            Thread.Sleep(1000);

            Log.Info("Interrupting");
            thread.Interrupt();
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
                    Log.Info(ThreadInterruptedExceptionMessage);
                }
            }
            catch (ThreadInterruptedException)
            {
                Log.Info(ThreadInterruptedExceptionMessage);
            }

            Log.Info(ExitingWorkerThread);
        }
    }
}