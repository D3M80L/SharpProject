﻿using System;
using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.OptimizationSensibles
{
    public sealed class TimerGarbageCollectorSensibleExample : OptimizationSensibleBase, IImportantExample
    {
        public const string TimerMessage = "TimerMessage";
        public const string GcCollect = "GcCollect";

        protected override void OnRun()
        {
            // NOTE: Run this example in debug and later in release
            var timer = new Timer(TimerCallback, null, Timeout.Infinite, Timeout.Infinite);
            timer.Change(0, 1000);

            PerformGarbageCollection();
        }

        private void PerformGarbageCollection()
        {
            Log.Info("Going to make GC.Collect");
            Log.Info(PressEnterToContinue);
            Interaction.ConfirmationRequest();

            GC.Collect(); // here we are forcing the GC to make a garbage collect
            Log.Info(GcCollect);
        }

        private void TimerCallback(object state)
        {
            Log.Info(TimerMessage);
        }
    }

}
