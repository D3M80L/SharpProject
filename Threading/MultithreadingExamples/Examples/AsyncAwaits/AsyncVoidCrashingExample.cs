using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Exceptions;
using MultithreadingExamples.Infrastructure.Extensions;
using MultithreadingExamples.ThreadingModels;

namespace MultithreadingExamples.Examples.AsyncAwaits
{
    /// <summary>
    /// This is only for demo. Take a look at decompiled code (with show compiler-generated code option enabled).
    /// IAsyncStateMachine - http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.iasyncstatemachine(v=vs.110).aspx
    /// 
    /// For details about async/await:
    /// - http://msdn.microsoft.com/en-us/library/hh156513.aspx
    /// </summary>
    public sealed class AsyncVoidCrashingExample : AsynAwaitExampleBase, IImportantExample, IRelatedWith<LockingExample>
    {
        public const string VeryImportantException = "VeryImportantException";

        protected override void OnRun()
        {
            try
            {
                RunAsync();
            }
            catch (VeryImportantException)
            {
                Log.Info(VeryImportantException);
            }
        }

        /// <summary>
        /// Take a look, that this is an async 'void' method.
        /// Such methods may be used in event handlers, but should be omitted.
        /// </summary>
        /// <remarks>
        /// Q: Why should this be ommited?
        /// A: Call this method from UI thread. When the exception is thrown, then the exception will be pushed to UI.
        /// An unhandled excetpion then occurs 'somewhere' and 'anywhere'
        /// </remarks>
        private async void RunAsync()
        {
            Log.Info("EnteredRunAsync");

            await Task.Delay(1000);

            Log.Info("ThrowingException");

            throw new VeryImportantException();
        }
    }
}