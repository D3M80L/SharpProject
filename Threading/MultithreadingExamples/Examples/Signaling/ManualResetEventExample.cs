using System.Threading;

namespace MultithreadingExamples.Examples.Signaling
{
    public sealed class ManualResetEventExample : ResetEventExampleBase
    {
        private readonly ManualResetEvent _manualResetEvent = new ManualResetEvent(initialState: false);

        protected override void OnSignal()
        {
            _manualResetEvent.Set();
        }

        protected override void OnWait()
        {
            _manualResetEvent.WaitOne();
        }
    }
}