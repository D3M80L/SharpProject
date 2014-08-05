using System;
using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Exceptions;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.CooperativeCancellations
{
    public sealed class CancellationTokenCallbackThrowingExceptionExample : CooperativeCancellationExampleBase, IImportantExample
    {
        public const string CallbackThrowingExceptionMessage = "CallbackThrowingExceptionMessage";

        public bool ThrowOnFirstException { get; set; }

        protected override void OnRun()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            ThreadPool.QueueUserWorkItem(x => Process(cancellationTokenSource.Token));

            Log.Info("WaitingForCancellation");
            Thread.Sleep(250); // Simulate some work

            try
            {
                cancellationTokenSource.Cancel(throwOnFirstException: ThrowOnFirstException);
            }
            catch (VeryImportantException)
            {
                // Just to present the example - you need to call the Cancel with throwOnFirstException set to true 
                // Always prefer the AggregateException and call Cancel() 
                Log.Info(ImportantExceptionState);
            }
            catch (AggregateException)
            {
                Log.Info(AggregateExceptionMessage);
            }
        }

        private void Process(CancellationToken cancellationToken)
        {
            cancellationToken.Register(TokenCallback);
        }

        private void TokenCallback()
        {
            Log.Info(CallbackThrowingExceptionMessage);

            throw new VeryImportantException();
        }
    }
}