using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Tasks
{
    public sealed class WaitingForANotStartedTaskBlocks : TasksBase, IImportantExample
    {
        protected override void OnRun()
        {
            var task = new Task(RunInTask);

            Log.Info("BeforeWait");
            task.Wait();
            Log.Info("AfterWait");
        }

        private void RunInTask()
        {
            Log.Info(InTask);
        }
    }
}