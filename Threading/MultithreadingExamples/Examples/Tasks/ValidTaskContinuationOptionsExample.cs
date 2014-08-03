using System.Threading;
using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure;

namespace MultithreadingExamples.Examples.Tasks
{
    public sealed class ValidTaskContinuationOptionsExample : TaskContinuationOptionsExampleBase, ISolutionFor<InvalidTaskContinuationOptionsExample>
    {
        protected override void BuildTask()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var task = Task
                .Run(() => RunInTask(cancellationTokenSource.Token), cancellationToken: cancellationTokenSource.Token);

            task.ContinueWith(t => OnCancelled(), continuationOptions: TaskContinuationOptions.OnlyOnCanceled);
            task.ContinueWith(t => OnFaulted(), continuationOptions: TaskContinuationOptions.OnlyOnFaulted); // SHOULD BE FIRED
            task.ContinueWith(t => OnFinished(), continuationOptions: TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}