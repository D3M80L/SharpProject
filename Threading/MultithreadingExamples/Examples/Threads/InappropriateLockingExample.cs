using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Threads
{
    public sealed class InappropriateLockingExample : ExampleBase, IImportantExample, IHasSolutionIn<LockExample>
    {
        public const string RunInThreadMessage = "RunInThreadMessage";
        public const string OnRunMessage = "OnRunMessage";
        public const string WaitingForTask = "WaitingForTask";
        public const string Adding = "Adding";

        protected override void OnRun()
        {
            lock (this)
            {
                var task = new Task(RunInThread, TaskCreationOptions.LongRunning);
                task.Start();
                Log.Info(WaitingForTask);
                task.Wait(); // Deadlock
            }
        }

        private void RunInThread()
        {
            Add(RunInThreadMessage);
        }

        public void Add(string message)
        {
            Log.Info(Adding);

            lock (this) //
            {
                Log.Info(message);
            }
        }
    }
}
