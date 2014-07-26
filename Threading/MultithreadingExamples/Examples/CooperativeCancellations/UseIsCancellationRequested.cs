using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.CooperativeCancellations
{
    public sealed class UseIsCancellationRequested : CooperativeCancellationBase
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
