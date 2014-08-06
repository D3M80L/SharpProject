using MultithreadingExamples.Examples.Signaling;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.Signaling
{
    [TestFixture]
    public sealed class PulseExampleTests : ExampleTestBase<PulseExample>
    {
        [Test]
        public void Pulse_OnlyOneWaitingThreadIsPulsed()
        {
            // Arrange
            Example.UsePulseAllInsteadOfPulse = false;

            var countPadLockExitState = new CountdownStateObserver(x => x.Contains(PulseExample.ExitedPadLock), 1);
            StateMachine.AddObserver(countPadLockExitState);

            var pulsedStateObserver = new CatchStateObserver(x => x == PulseExample.Pulsed);
            StateMachine.AddObserver(pulsedStateObserver);

            // Act
            RunExampleInThread();

            // Assert
            Assert.IsTrue(pulsedStateObserver.Wait(5000));
            Assert.IsTrue(countPadLockExitState.Wait(5000));
        }

        [Test]
        public void PulseAll_AllWaitingThreadsShouldContinue()
        {
            // Arrange
            Example.UsePulseAllInsteadOfPulse = true;

            var countPadLockExitState = new CountdownStateObserver(x => x.Contains(PulseExample.ExitedPadLock), 4);
            StateMachine.AddObserver(countPadLockExitState);

            var pulsedStateObserver = new CatchStateObserver(x => x == PulseExample.Pulsed);
            StateMachine.AddObserver(pulsedStateObserver);

            // Act
            RunExampleInThread();

            // Assert
            Assert.IsTrue(pulsedStateObserver.Wait(5000));
            Assert.IsTrue(countPadLockExitState.Wait(5000));
        }
    }
}
