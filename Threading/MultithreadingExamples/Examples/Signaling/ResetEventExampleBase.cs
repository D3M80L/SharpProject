using System.Threading;
using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Signaling
{
    public abstract class ResetEventExampleBase : ExampleBase
    {
        public const string BeforeSignalingState = "BeforeSignalingState";
        public const string AfterSignalingState  = "AfterSignalingState";
        public const string BeforeWaitingState   = "BeforeWaitingState";
        public const string AfterWaitingState    = "AfterWaitingState";

        protected override void OnRun()
        {
            Task.Run(() => Wait());

            Thread.Sleep(1000); // Simulate some work

            Task.Run(() => Signal());
        }

        private void Signal()
        {
            Log.Info(BeforeSignalingState);
            OnSignal();
            Log.Info(AfterSignalingState);
        }

        protected abstract void OnSignal();

        private void Wait()
        {
            Log.Info(BeforeWaitingState);
            OnWait();
            Log.Info(AfterWaitingState);
        }

        protected abstract void OnWait();
    }
}