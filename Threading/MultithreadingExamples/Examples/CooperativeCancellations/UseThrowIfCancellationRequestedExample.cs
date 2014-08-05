using System;
using System.Threading;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.CooperativeCancellations
{
    public sealed class UseThrowIfCancellationRequestedExample : CooperativeCancellationExampleBase
    {
        protected override void OnRun()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            ThreadPool.QueueUserWorkItem(x => Process(cancellationTokenSource.Token));

            Log.Info(WaitingForCancellation);
            Interaction.ConfirmationRequest();

            cancellationTokenSource.Cancel();

            Log.Info(WaitingForConfirmation);
            Interaction.ConfirmationRequest();
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