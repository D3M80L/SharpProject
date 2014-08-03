using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            var countPadLockExitState = new CatchStateObserver(x => x.StartsWith(PulseExample.ExitedPadLock));
            StateMachine.AddObserver(countPadLockExitState);

            // Act
            RunExampleInThread();
            WaitForExitConfirmation(3000);

            // Assert
            Assert.AreEqual(1, countPadLockExitState.Count);
        }

        [Test]
        public void PulseAll_AllWaitingThreadsShouldContinue()
        {
            // Arrange
            Example.UsePulseAllInsteadOfPulse = true;

            var countPadLockExitState = new CatchStateObserver(x => x.StartsWith(PulseExample.ExitedPadLock));
            StateMachine.AddObserver(countPadLockExitState);

            // Act
            RunExampleInThread();
            WaitForExitConfirmation(5000);

            // Assert
            Assert.AreEqual(4, countPadLockExitState.Count);
        }
    }
}
