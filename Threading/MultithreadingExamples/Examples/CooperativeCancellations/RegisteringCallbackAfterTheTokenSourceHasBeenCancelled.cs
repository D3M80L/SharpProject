﻿using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.CooperativeCancellations
{
    public sealed class RegisteringCallbackAfterTheTokenSourceHasBeenCancelled : CooperativeCancellationBase
    {
        protected override void OnRun()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            var token = cancellationTokenSource.Token;

            cancellationTokenSource.Cancel();

            Process(token);

        }

        private void Process(CancellationToken cancellationToken)
        {
            // note, that the cancellation token is already cancelled. Registering a callback will invoke the callback immediately
            cancellationToken.Register(() => Log.Info("CallbackA"));
        }
    }
}