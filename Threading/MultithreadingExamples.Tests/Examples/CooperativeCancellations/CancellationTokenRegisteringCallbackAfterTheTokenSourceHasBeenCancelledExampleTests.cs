using MultithreadingExamples.Examples.CooperativeCancellations;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.CooperativeCancellations
{
    [TestFixture]
    public sealed class CancellationTokenRegisteringCallbackAfterTheTokenSourceHasBeenCancelledExampleTests : ExampleTestBase<CancellationTokenRegisteringCallbackAfterTheTokenSourceHasBeenCancelledExample>
    {
        [Test]
        public void RegisteringCallbacksOnTokenSource_CallbackMessageIsObserved()
        {
            // Arrange
            var callbackMessageObserver = new CatchStateObserver(x => x == CancellationTokenRegisteringCallbackAfterTheTokenSourceHasBeenCancelledExample.CallbackState);
            StateMachine.AddObserver(callbackMessageObserver);

            // Act
            RunExampleInThread();
            WaitForExitConfirmation(5000);

            // Assert
            Assert.AreEqual(1, callbackMessageObserver.Count);
        }
    }
}