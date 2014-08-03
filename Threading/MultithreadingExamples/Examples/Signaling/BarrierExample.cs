using System;
using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Signaling
{
    public sealed class BarrierExample : ExampleBase
    {
        public const string BarrierFullMessage = "BarrierFullMessage";

        private readonly Barrier _barrier;

        public BarrierExample()
        {
            _barrier = new Barrier(4, BarrierFull);
        }

        protected override void OnRun()
        {
            for (int i = 0; i < 4; i++)
            {
                Log.Info("[T{0}]{1} ", Thread.CurrentThread.ManagedThreadId, i);
                _barrier.SignalAndWait();
            }
        }

        private void BarrierFull(Barrier barrier)
        {
            Log.Info(BarrierFullMessage);
        }
    }
}