using MultithreadingExamples.Examples.OptimizationSensibles;
using MultithreadingExamples.Tests.Infrastructure;
using MultithreadingExamples.Tests.Infrastructure.StateObservers;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.OptimizationSensibles
{
    [TestFixture]
    public sealed class TimerGarbageCollectorSensibleExampleTests : ExampleTestBase<TimerGarbageCollectorSensibleExample>
    {
        [Test]
        public void NoTimerMessageAfterGcCollectInReleaseBuild()
        {
            // Arrange
            var gcCollectState = StateMachine.AddObserver(new ExclusiveLockStateObserver(state => state == TimerGarbageCollectorSensibleExample.GcCollect));

            // Act
            RunExampleInThread();

            var timerMessageState = new CatchStateObserver(state => state == TimerGarbageCollectorSensibleExample.TimerMessage);
            
            var gcCollected = gcCollectState
                .Wait(5000, () =>
                {
                    StateMachine.AddObserver(timerMessageState);

                    StateMachine.RemoveObserver(gcCollectState);
                });

            // Assert
            Assert.IsTrue(gcCollected, "GC Collect message not found.");
            Assert.IsTrue(timerMessageState.Wait(5000), "No timer message found.");
        }
    }
}
