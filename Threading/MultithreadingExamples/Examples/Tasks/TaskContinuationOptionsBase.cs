using System.Threading;
using MultithreadingExamples.Infrastructure.Exceptions;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Tasks
{
    public abstract class TaskContinuationOptionsBase : TasksBase
    {
        protected override void OnRun()
        {
            Log.Info("Main ThreadId={0}", Thread.CurrentThread.ManagedThreadId);

            BuildTask();

            Log.Info(PressEnterToContinue);
            Interaction.Confirmation();
        }

        protected abstract void BuildTask();

        protected void RunInTask(CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("{0} ThreadId={1}", InTask, Thread.CurrentThread.ManagedThreadId);
                throw new VeryImportantException();
            }
            catch (VeryImportantException)
            {
                Log.Info("VeryImportantException");
                throw;
            }
        }

        protected void OnCancelled()
        {
            Log.Info("{0} ThreadId={1}", "OnCancelled", Thread.CurrentThread.ManagedThreadId);
        }

        protected void OnFaulted()
        {
            Log.Info("{0} ThreadId={1}", "OnFaulted", Thread.CurrentThread.ManagedThreadId);
        }

        protected void OnFinished()
        {
            Log.Info("{0} ThreadId={1}", "OnFinished", Thread.CurrentThread.ManagedThreadId);
        }

    }
}