using System;
using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Exceptions;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.CooperativeCancellations
{
    public sealed class CancellationTokenCallbackThrowingException : CooperativeCancellationBase, IImportantExample
    {
        protected override void OnRun()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            ThreadPool.QueueUserWorkItem(x => Process(cancellationTokenSource.Token));

            Log.Info("WaitingForCancellation");
            Interaction.ConfirmationRequest();

            try
            {
                cancellationTokenSource.Cancel(throwOnFirstException: false);
            }
            catch (VeryImportantException)
            {
                // Just to present the example - you need to call the Cancel with throwOnFirstException set to true 
                // Always prefer the AggregateException and call Cancel() 
                Log.Info("VeryImportantException");
            }
            catch (AggregateException)
            {
                Log.Info("AggregateException");
            }

            Log.Info("WaitingForConfirmation");
            Interaction.ConfirmationRequest();
        }

        private void Process(CancellationToken cancellationToken)
        {
            cancellationToken.Register(TokenCallback);
        }

        private void TokenCallback()
        {
            Log.Info("ThrowingException");

            throw new VeryImportantException();
        }
    }
}