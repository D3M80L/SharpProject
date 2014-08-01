using System.Threading;
using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Signaling
{
    public abstract class ResetEventExampleBase : ExampleBase
    {
        public const string BeforeSignaling = "BeforeSignaling";
        public const string AfterSignaling = "AfterSignaling";

        public const string BeforeWaiting = "BeforeWaiting";
        public const string AfterWaiting = "AfterWaiting";

        protected override void OnRun()
        {
            Task.Run(() => Wait());

            Thread.Sleep(1000); // Simulate some work

            Task.Run(() => Signal());
        }

        private void Signal()
        {
            Log.Info(BeforeSignaling);
            OnSignal();
            Log.Info(AfterSignaling);
        }

        protected abstract void OnSignal();

        private void Wait()
        {
            Log.Info(BeforeWaiting);
            OnWait();
            Log.Info(AfterWaiting);
        }

        protected abstract void OnWait();
    }
}