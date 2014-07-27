﻿using System.Threading;
using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Tasks
{
    public sealed class CreateTaskUsingFactory : TasksBase
    {
        protected override void OnRun()
        {
            Log.Info("ThreadId={0}", Thread.CurrentThread.ManagedThreadId);

            Task.Run(() => RunInTask());

            Log.Info(PressEnterToContinue);
            Interaction.ConfirmationRequest();
        }

        private void RunInTask()
        {
            Log.Info(InTask + "#ThreadId={0}", Thread.CurrentThread.ManagedThreadId);
        }
    }
}