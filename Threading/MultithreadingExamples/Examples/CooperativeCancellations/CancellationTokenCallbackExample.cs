using System.Threading;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.CooperativeCancellations
{
    public sealed class CancellationTokenCallbackExample : CooperativeCancellationExampleBase
    {
        public const string CancellationCallbackMessage = "CancellationCallbackMessage";

        protected override void OnRun()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            Process(cancellationTokenSource.Token);

            cancellationTokenSource.Cancel();
        }

        private void Process(CancellationToken cancellationToken)
        {
            cancellationToken.Register(() => TokenCallback(CancellationCallbackMessage));
            cancellationToken.Register(() => TokenCallback(CancellationCallbackMessage));
        }

        private void TokenCallback(string message)
        {
            Log.Info(message);
        }
    }
}