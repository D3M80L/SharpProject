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
                .Run(() => RunInTask(cancellationTokenSource.Token), cancellationTokenSource.Token);

            task.ContinueWith(t => OnCancelled(), TaskContinuationOptions.OnlyOnCanceled);
            task.ContinueWith(t => OnFaulted(), TaskContinuationOptions.OnlyOnFaulted); // SHOULD BE FIRED
            task.ContinueWith(t => OnFinished(), TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}