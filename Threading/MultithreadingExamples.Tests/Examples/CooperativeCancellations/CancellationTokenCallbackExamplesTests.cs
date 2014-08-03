using MultithreadingExamples.Examples.CooperativeCancellations;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.CooperativeCancellations
{
    [TestFixture]
    public sealed class CancellationTokenCallbackExamplesTests : ExampleTestBase<CancellationTokenCallbackExample>
    {
        [Test]
        public void TwoRegisteredMethodsInCancellationTokenAreFired()
        {
            // Arrange
            var countMessages = new CatchStateObserver(x => x == CancellationTokenCallbackExample.CancellationCallbackMessage);
            StateMachine.AddObserver(countMessages);

            // Act
            RunExampleInThread();
            WaitForExitConfirmation(1000);

            // Assert
            Assert.AreEqual(2, countMessages.Count);
        }
    }
}