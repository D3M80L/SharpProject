﻿using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure.Exceptions;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Tasks
{
    public sealed class ThrowExceptionInTaskExample : TasksExampleBase
    {
        protected override void OnRun()
        {
            Task.Run(() => RunInTask());

            Log.Info(PressEnterToContinue);
            Interaction.ConfirmationRequest();
        }

        private void RunInTask()
        {
            Log.Info(InTaskState);
            // If you run this method in pure Thread or ThreadPool, the application crashes
            throw new VeryImportantException();
        }
    }
}