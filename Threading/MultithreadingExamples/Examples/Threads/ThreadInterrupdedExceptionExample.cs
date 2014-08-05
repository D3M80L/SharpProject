using System.Threading;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Threads
{
    public sealed class ThreadInterrupdedExceptionExample : ThreadExampleBase
    {
        public const string ThreadInterruptedExceptionState = "ThreadInterruptedExceptionState";
        public const string ExitingWorkerThreadState        = "ExitingWorkerThreadState";

        protected override void OnRun()
        {
            var thread = StartThread();

            Thread.Sleep(1000);

            Log.Info("Interrupting");
            thread.Interrupt();
        }

        private Thread StartThread()
        {
            var thread = new Thread(RunInThread);
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
                    Log.Info(ThreadInterruptedExceptionState);
                }
            }
            catch (ThreadInterruptedException)
            {
                Log.Info(ThreadInterruptedExceptionState);
            }

            Log.Info(ExitingWorkerThreadState);
        }
    }
}