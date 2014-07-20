using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.CooperativeCancellations
{
    public sealed class CancellationTokenCallback : CooperativeCancellationBase
    {
        protected override void OnRun()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            Process(cancellationTokenSource.Token);

            cancellationTokenSource.Cancel();
        }

        private void Process(CancellationToken cancellationToken)
        {
            cancellationToken.Register(() => TokenCallback("CallbackA"));
            cancellationToken.Register(() => TokenCallback("CallbackB"));
        }

        private void TokenCallback(string message)
        {
            Log.Info(message);
        }
    }
}