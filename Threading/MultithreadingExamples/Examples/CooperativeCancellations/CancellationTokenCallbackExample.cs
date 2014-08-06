using System.Threading;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.CooperativeCancellations
{
    public sealed class CancellationTokenCallbackExample : CooperativeCancellationExampleBase
    {
        public const string CancellationCallbackState = "CancellationCallbackState";

        protected override void OnRun()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            Process(cancellationTokenSource.Token);

            cancellationTokenSource.Cancel();
        }

        private void Process(CancellationToken cancellationToken)
        {
            cancellationToken.Register(() => TokenCallback(CancellationCallbackState));
            cancellationToken.Register(() => TokenCallback(CancellationCallbackState));
        }

        private void TokenCallback(string message)
        {
            Log.Info(message);
        }
    }
}