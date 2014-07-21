using System.Threading;
using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Tasks
{
    public sealed class TaskCancellation : TasksBase
    {
        protected override void OnRun()
        {
            Log.Info("Main ThreadId={0}", Thread.CurrentThread.ManagedThreadId);
            var cancellationTokenSource = new CancellationTokenSource();

            Task
                .Run(() => RunInTask(cancellationTokenSource.Token), cancellationToken: cancellationTokenSource.Token)
                .ContinueWith(task => OnCancelled(), continuationOptions: TaskContinuationOptions.OnlyOnCanceled);

            CancelTask(cancellationTokenSource);

            Log.Info(PressEnterToContinue);
            ConsoleInput.ReadLine();
        }

        private void CancelTask(CancellationTokenSource cancellationTokenSource)
        {
            Log.Info(PressEnterToContinue);
            ConsoleInput.ReadLine();
            Log.Info("Cancelling");

            cancellationTokenSource.Cancel();
        }

        private void RunInTask(CancellationToken cancellationToken)
        {
            Log.Info("{0} ThreadId={1}", InTask, Thread.CurrentThread.ManagedThreadId);
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
        }

        private void OnCancelled()
        {
            Log.Info("{0} ThreadId={1}", "OnCancelled", Thread.CurrentThread.ManagedThreadId);
        }
    }
}