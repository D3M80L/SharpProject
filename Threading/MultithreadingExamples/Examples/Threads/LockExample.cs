using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Threads
{
    public sealed class LockExample : ExampleBase, ISolutionFor<InappropriateLockingExample>
    {
        public const string RunInThreadMessage = "RunInThreadMessage";
        public const string OnRunMessage = "OnRunMessage";
        public const string WaitingForTask = "WaitingForTask";
        public const string Adding = "Adding";

        private readonly object _padlock = new object();

        protected override void OnRun()
        {
            lock (this) // Monitor.Enter(this);
            {
                var task = new Task(RunInThread, TaskCreationOptions.LongRunning);
                task.Start();
                Log.Info(WaitingForTask);
                task.Wait(); // Deadlock
            } // Monitor.Exit(this); But not only!
        }

        private void RunInThread()
        {
            Add(RunInThreadMessage);
        }

        public void Add(string message)
        {
            Log.Info(Adding);

            lock (_padlock) // Monitor.Enter(_padlock);
            {
                Log.Info(message);
            } // Monitor.Exit(_padlock); But not only!
        }
    }
}