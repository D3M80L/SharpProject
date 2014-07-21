using System;
using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure.Exceptions;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Tasks
{
    public sealed class CatchExceptionFromTask : TasksBase
    {
        protected override void OnRun()
        {
            var task = Task.Run(() => RunInTask());

            try
            {
                task.Wait();
            }
            catch (AggregateException)
            {
                Log.Info("AggregateException");
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