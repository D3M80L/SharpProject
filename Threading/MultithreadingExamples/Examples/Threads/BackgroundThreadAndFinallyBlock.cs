using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Threads
{
    public sealed class BackgroundThreadAndFinallyBlock : ThreadExampleBase, IImportantExample
    {
        protected override void OnRun()
        {
            StartBackgroundThread();

            Thread.Sleep(1000);
        }

        private void StartBackgroundThread()
        {
            var thread = new Thread(start: RunInThread);
            thread.IsBackground = true;

            Log.Info(StartingThread);
            thread.Start();
        }

        private void RunInThread()
        {
            try
            {
                Log.Info("RunInThread. IsBackground={0}", Thread.CurrentThread.IsBackground);
                Thread.Sleep(5000);
            }
            finally
            {
                Log.Info(ExitingFromThread); // NOTE: never called when process stops
            }
        }
    }
}