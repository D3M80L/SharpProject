using System.Threading;

namespace MultithreadingExamples.Examples.Signaling
{
    /// <summary>
    /// See ManualResetEventSlim documentation http://msdn.microsoft.com/en-us/library/system.threading.manualreseteventslim(v=vs.110).aspx
    /// </summary>
    public sealed class ManualResetEventSlimExample : ResetEventExampleBase
    {
        private readonly ManualResetEventSlim _manualResetEventSlim = new ManualResetEventSlim(false);

        protected override void OnSignal()
        {
            _manualResetEventSlim.Set();
        }

        protected override void OnWait()
        {
            _manualResetEventSlim.Wait();
        }
    }
}
