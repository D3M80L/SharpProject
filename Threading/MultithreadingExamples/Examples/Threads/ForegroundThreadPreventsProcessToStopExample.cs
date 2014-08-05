using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Threads
{
    public sealed class ForegroundThreadPreventsProcessToStopExample : ThreadExampleBase, IImportantExample
    {
        protected override void OnRun()
        {
            StartForegroundThread();
        }

        private void StartForegroundThread()
        {
            var thread = new Thread(start: RunInThread);
            Log.Info(StartingThread);
            thread.Start();
        }

        private void RunInThread()
        {
            Log.Info("RunInThread. IsBackground={0}", Thread.CurrentThread.IsBackground);
            Thread.Sleep(30000); // Simulate work
            Log.Info(ExitingFromThread);
        }
    }
}