using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Tasks
{
    public sealed class WaitingForANotStartedTaskBlocksExample : TasksExampleBase, IImportantExample
    {
        public const string BeforeWait = "BeforeWait";
        public const string AfterWait = "AfterWait";

        protected override void OnRun()
        {
            var task = new Task(RunInTask);

            Log.Info(BeforeWait);
            task.Wait(); // Blocked
            Log.Info(AfterWait);
        }

        private void RunInTask()
        {
            Log.Info(InTask);
        }
    }
}