using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.CooperativeCancellations
{
    public abstract class CooperativeCancellationBase : ExampleBase
    {
        public const string Cancelled = "Cancelled";
        public const string SendingMessage = "SendingMessage";
        public const string WaitingForCancellation = "WaitingForCancellation";
        public const string WaitingForConfirmation = "WaitingForConfirmation";

        protected void SendMessage()
        {
            Log.Info(SendingMessage);
            Thread.Sleep(1000);
        }
    }
}