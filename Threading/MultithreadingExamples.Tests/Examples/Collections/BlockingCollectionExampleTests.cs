using MultithreadingExamples.Examples.Collections;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.Collections
{
    [TestFixture]
    public sealed class BlockingCollectionExampleTests : ExampleTestBase<BlockingCollectionExample>
    {
        [Test]
        public void Run()
        {
            // Arrange
            var productionFinishedState = new CatchStateObserver(x => x == BlockingCollectionExample.ProductionFinishedState);
            StateMachine.AddObserver(productionFinishedState);

            // Act
            RunExampleInThread();

            // Assert
            Assert.IsTrue(productionFinishedState.Wait(5000));
        }
    }
}
