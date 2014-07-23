﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MultithreadingExamples.Examples.OptimizationSensibles;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Tests.Infrastructure;
using MultithreadingExamples.Tests.Infrastructure.StateObservers;
using NUnit.Framework;
using Rhino.Mocks;

namespace MultithreadingExamples.Tests.Examples.OptimizationSensibles
{
    [TestFixture]
    public sealed class TimerGarbageCollectorSensibleTests : ExampleTestBase<TimerGarbageCollectorSensible>
    {
        [Test]
        public void NoTimerMessageAfterGcCollectInReleaseBuild()
        {
            // Arrange
            var gcCollectState = StateMachine.AddObserver(new ExclusiveLockStateObserver(state => state == TimerGarbageCollectorSensible.GcCollect));

            // Act
            RunExampleInThread();

            var timerMessageState = new CatchStateObserver(state => state == TimerGarbageCollectorSensible.TimerMessage);
            
            gcCollectState
                .Wait(5000, action: () =>
                {
                    StateMachine.AddObserver(new LockingStateObserver(x => x == ExampleBase.PressEnterToContinue));

                    StateMachine.AddObserver(timerMessageState);

                    StateMachine.RemoveObserver(gcCollectState);
                });

            // Assert
            Assert.IsTrue(timerMessageState.Wait(5000));
        }
    }
}
