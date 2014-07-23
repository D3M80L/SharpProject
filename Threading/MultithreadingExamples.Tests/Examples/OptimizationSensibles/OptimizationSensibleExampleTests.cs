using System;
using MultithreadingExamples.Examples.OptimizationSensibles;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Tests.Infrastructure;
using MultithreadingExamples.Tests.Infrastructure.StateObservers;
using NUnit.Framework;
using Rhino.Mocks;

namespace MultithreadingExamples.Tests.Examples.OptimizationSensibles
{
    [TestFixture]
    public sealed class OptimizationSensibleExampleTests : ExampleTestBase<OptimizationSensibleExample>
    {
        [Test]
        public void CountingNotFinishedInReleaseBuild()
        {
            // Arrange
            var countingFinished = StateMachine.AddObserver(new CatchStateObserver(state => state == OptimizationSensibleExample.CountingFinished));

            var confirmCountingStop = StateMachine.AddObserver(new ExclusiveLockStateObserver(state => state == OptimizationSensibleExample.ConfirmCountingStopMessage));

            // Act
            RunExampleInThread();
            confirmCountingStop.Wait(5000, action: () => { });

            // Assert
            Assert.IsTrue(countingFinished.Wait(5000));
        }
    }
}