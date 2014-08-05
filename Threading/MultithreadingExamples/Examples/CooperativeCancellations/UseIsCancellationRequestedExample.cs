using System.Threading;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.CooperativeCancellations
{
    public sealed class UseIsCancellationRequestedExample : CooperativeCancellationExampleBase
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
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Log.Info(Cancelled);
                    return;
                }

                SendMessage();
            }
        }
    }
}
