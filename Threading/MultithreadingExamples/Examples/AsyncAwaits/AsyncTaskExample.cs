using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Exceptions;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.AsyncAwaits
{
    public sealed class AsyncTaskExample : AsynAwaitExampleBase, ISolutionFor<AsyncVoidCrashingExample>
    {
        protected override void OnRun()
        {
            try
            {
                RunAsync();
            }
            catch (VeryImportantException)
            {
                Log.Info(ImportantExceptionState); // Is never caught
            }
        }

        private async Task RunAsync()
        {
            Log.Info("EnteredRunAsync");

            await Task.Delay(1000);

            Log.Info(ThrowingExceptionState);

            throw new VeryImportantException();
        }
    }
}