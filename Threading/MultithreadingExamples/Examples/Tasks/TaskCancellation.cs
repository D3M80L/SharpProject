using System.Threading;
using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Tasks
{
    public sealed class TaskCancellation : TasksExampleBase
    {
        protected override void OnRun()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            Task
                .Run(() => RunInTask(cancellationTokenSource.Token), cancellationToken: cancellationTokenSource.Token)
                .ContinueWith(task => OnCancelled(), continuationOptions: TaskContinuationOptions.OnlyOnCanceled);

            CancelTask(cancellationTokenSource);

            Log.Info(PressEnterToContinue);
            Interaction.ConfirmationRequest();
        }

        private void CancelTask(CancellationTokenSource cancellationTokenSource)
        {
            Log.Info(PressEnterToContinue);
            Interaction.ConfirmationRequest();
            Log.Info("Cancelling");

            cancellationTokenSource.Cancel();
        }

        private void RunInTask(CancellationToken cancellationToken)
        {
            Log.Info("{0} ThreadId={1}", InTaskState, Thread.CurrentThread.ManagedThreadId);
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