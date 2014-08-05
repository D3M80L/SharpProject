using System.Threading;
using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure;

namespace MultithreadingExamples.Examples.Tasks
{
    public sealed class InvalidTaskContinuationOptionsExample : TaskContinuationOptionsExampleBase, IImportantExample, IHasSolutionIn<ValidTaskContinuationOptionsExample>
    {
        protected override void BuildTask()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            // A common mistake, that different continuation options are combined together with fluent interface
            Task
                .Run(() => RunInTask(cancellationTokenSource.Token), cancellationTokenSource.Token)
                .ContinueWith(t => OnCancelled(), TaskContinuationOptions.OnlyOnCanceled)
                .ContinueWith(t => OnFaulted(), TaskContinuationOptions.OnlyOnFaulted) // SHOULD BE FIRED
                .ContinueWith(t => OnFinished(), TaskContinuationOptions.OnlyOnRanToCompletion);

        }
    }
}