using System.Threading;
using MultithreadingExamples.Infrastructure.Exceptions;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Tasks
{
    public abstract class TaskContinuationOptionsExampleBase : TasksExampleBase
    {
        public const string OnFaultedMessage = "OnFaulted";
        public const string OnCancelledMessage = "OnCancelled";
        public const string OnFinishedMessage = "OnFinished";

        protected override void OnRun()
        {
            Log.Info("Main ThreadId={0}", Thread.CurrentThread.ManagedThreadId);

            BuildTask();
        }

        protected abstract void BuildTask();

        protected void RunInTask(CancellationToken cancellationToken)
        {
            try
            {
                Log.Info("{0} ThreadId={1}", InTaskState, Thread.CurrentThread.ManagedThreadId);
                throw new VeryImportantException();
            }
            catch (VeryImportantException)
            {
                Log.Info(ImportantExceptionState);
                throw;
            }
        }

        protected void OnCancelled()
        {
            Log.Info("{0} ThreadId={1}", OnCancelledMessage, Thread.CurrentThread.ManagedThreadId);
        }

        protected void OnFaulted()
        {
            Log.Info("{0} ThreadId={1}", OnFaultedMessage, Thread.CurrentThread.ManagedThreadId);
        }

        protected void OnFinished()
        {
            Log.Info("{0} ThreadId={1}", OnFinishedMessage, Thread.CurrentThread.ManagedThreadId);
        }

    }
}