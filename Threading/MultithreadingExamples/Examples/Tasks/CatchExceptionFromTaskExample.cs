using System;
using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure.Exceptions;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Tasks
{
    public sealed class CatchExceptionFromTaskExample : TasksExampleBase
    {
        protected override void OnRun()
        {
            var task = new Task(RunInTask, TaskCreationOptions.LongRunning);
            task.Start();

            try
            {
                task.Wait();
            }
            catch (VeryImportantException)
            {
                Log.Info(ImportantException); // Not catched
            }
            catch (AggregateException)
            {
                Log.Info(AggregateExceptionMessage);
            }
        }

        private void RunInTask()
        {
            Log.Info(InTask);
            // If you run this method in pure Thread or ThreadPool, the application crashes
            throw new VeryImportantException();
        }
    }
}