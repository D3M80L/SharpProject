using MultithreadingExamples.Examples.Tasks;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.Tasks
{
    [TestFixture]
    public sealed class WaitingForANotStartedTaskBlocksExampleTests : ExampleTestBase<WaitingForANotStartedTaskBlocksExample>
    {
        [Ignore]
        [Test]
        public void BlocksInSomeCases()
        {
            // Arrange
            var afterWaitState = new CatchStateObserver(x => x == WaitingForANotStartedTaskBlocksExample.AfterWaitState);
            StateMachine.AddObserver(afterWaitState);

            // Act
            RunExampleInThread();

            // Assert
            Assert.IsTrue(afterWaitState.Wait(5000));
        }
    }
}