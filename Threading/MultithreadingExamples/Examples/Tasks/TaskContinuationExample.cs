using System.Threading;
using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Tasks
{
    public sealed class TaskContinuationExample : TasksExampleBase
    {
        protected override void OnRun()
        {
            Log.Info("Main ThreadId={0}", Thread.CurrentThread.ManagedThreadId);
            Task
                .Run(() => RunInTask("A"))
                .ContinueWith(_ => RunInTask("B"))
                .ContinueWith(_ => RunInTask("C"))
                .ContinueWith(_ => RunInTask("D"))
                .Wait();
        }

        private void RunInTask(string message)
        {
            Log.Info("{0}-{1} ThreadId={2}", InTask, message, Thread.CurrentThread.ManagedThreadId);
        }
    }
}