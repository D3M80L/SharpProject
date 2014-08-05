using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Tasks
{
    public sealed class WaitingForANotStartedTaskBlocksExample : TasksExampleBase, IImportantExample
    {
        public const string BeforeWaitState = "BeforeWaitState";
        public const string AfterWaitState = "AfterWaitState";

        protected override void OnRun()
        {
            var task = new Task(RunInTask);

            Log.Info(BeforeWaitState);
            task.Wait(); // Blocked
            Log.Info(AfterWaitState);
        }

        private void RunInTask()
        {
            Log.Info(InTaskState);
        }
    }
}