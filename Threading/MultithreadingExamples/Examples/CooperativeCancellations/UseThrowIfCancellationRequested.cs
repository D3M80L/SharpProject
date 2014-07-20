using System;
using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.CooperativeCancellations
{
    public sealed class UseThrowIfCancellationRequested : CooperativeCancellationBase
    {
        protected override void OnRun()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            ThreadPool.QueueUserWorkItem(x => Process(cancellationTokenSource.Token));

            Log.Info(WaitingForCancellation);
            ConsoleInput.ReadLine();

            cancellationTokenSource.Cancel();

            Log.Info(WaitingForConfirmation);
            ConsoleInput.ReadLine();
        }

        private void Process(CancellationToken cancellationToken)
        {
            try
            {
                while (true)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    SendMessage();
                }
            }
            catch (OperationCanceledException)
            {
                Log.Info(Cancelled);
            }
        }
    }
}