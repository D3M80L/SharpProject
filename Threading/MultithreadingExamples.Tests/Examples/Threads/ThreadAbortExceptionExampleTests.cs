using MultithreadingExamples.Examples.Threads;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.Threads
{
    [TestFixture]
    public sealed class ThreadAbortExceptionExampleTests : ExampleTestBase<ThreadAbortExceptionExample>
    {
        [Test]
        public void TwoThreadAbortExceptionMessagesShouldBeCaught()
        {
            // Arrange
            var countdownEvent = new CountdownStateObserver(x => x == ThreadAbortExceptionExample.ThreadAbortExceptionMessage, 2);
            StateMachine.AddObserver(countdownEvent);

            // Act
            RunExampleInThread();

            // Assert
            Assert.IsTrue(countdownEvent.Wait(2000));
        }
    }
}