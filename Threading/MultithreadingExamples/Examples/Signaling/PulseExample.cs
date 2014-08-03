using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Signaling
{
    /// <summary>
    /// Important:
    /// Calling Pulse or PulseAll before any thread is waiting for pulse causes, that the call is swallowed
    /// </summary>
    public sealed class PulseExample : ExampleBase
    {
        public const string WaitForPulseMessage = "WaitForPulseMessage";
        public const string EnteredPadLock = "EnteredPadLock";
        public const string ExitedPadLock = "ExitedPadLock";
        public const string BeforePulse = "BeforePulse";
        public const string AfterPulse = "AfterPulse";
        public const string BeforePulseAll = "BeforePulseAll";
        public const string AfterPulseAll = "AfterPulseAll";
        public const string Pulsing = "Pulsing";

        private readonly object _padLock = new object();
        private readonly CountdownEvent _countdownEvent = new CountdownEvent(4);

        public bool UsePulseAllInsteadOfPulse { get; set; }


        protected override void OnRun()
        {
            ThreadPool.QueueUserWorkItem(_ => WaitForPulse("A"));
            ThreadPool.QueueUserWorkItem(_ => WaitForPulse("B"));
            ThreadPool.QueueUserWorkItem(_ => WaitForPulse("C"));
            ThreadPool.QueueUserWorkItem(_ => WaitForPulse("D"));

            EnsureAllThreadsAreWaitingForPulse();
            
            CallPulseOrPulseAll();

            Thread.Sleep(2000);
        }

        private void CallPulseOrPulseAll()
        {
            Log.Info(Pulsing);

            if (UsePulseAllInsteadOfPulse)
            {
                PulseAll();
            }
            else
            {
                Pulse();
            }
        }

        private void Pulse()
        {
            lock (_padLock)
            {
                Log.Info(BeforePulse);

                Monitor.Pulse(_padLock);

                Log.Info(AfterPulse);
            }
        }

        private void PulseAll()
        {
            lock (_padLock)
            {
                Log.Info(BeforePulseAll);

                Monitor.PulseAll(_padLock);

                Log.Info(AfterPulseAll);
            }
        }

        private void WaitForPulse(string id)
        {
            Log.Info(WaitForPulseMessage + id);

            lock (_padLock)
            {
                Log.Info(EnteredPadLock + id);

                NotifyWaitingForPulse();
                Monitor.Wait(_padLock);

                Log.Info(ExitedPadLock + id);
            }
        }

        private void EnsureAllThreadsAreWaitingForPulse()
        {
            _countdownEvent.Wait();
        }

        private void NotifyWaitingForPulse()
        {
            _countdownEvent.Signal();
        }
    }
}
