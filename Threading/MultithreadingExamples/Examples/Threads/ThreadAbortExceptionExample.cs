using System.Threading;
using MultithreadingExamples.Examples.CooperativeCancellations;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Threads
{
    public sealed class ThreadAbortExceptionExample : ThreadExampleBase, IImportantExample, IHasSolutionIn<CooperativeCancellationExampleBase>
    {
        public const string ThreadAbortExceptionMessage = "ThreadAbortExceptionMessage";

        protected override void OnRun()
        {
            var thread = StartThread();
            Thread.Sleep(1000);

            Log.Info("Aborting");
            thread.Abort();
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
                        Log.Info("."); // Simulate some work
                        Thread.Sleep(250);
                    }
                }
                catch (ThreadAbortException threadAbortException)
                {
                    Log.Info(ThreadAbortExceptionMessage);
                    Log.Info(threadAbortException.StackTrace);
                    // note, that this exception is rethrown in a fashion of 'throw threadAbortException;'
                }
            }
            catch (ThreadAbortException threadAbortException)
            {
                Log.Info(ThreadAbortExceptionMessage);
                Log.Info(threadAbortException.StackTrace); // note, that this stack trace contains only information from the first catch block
            }

            Log.Info("SomeOtherWork");
        }
    }
}