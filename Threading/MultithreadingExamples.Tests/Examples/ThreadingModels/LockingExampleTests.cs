using MultithreadingExamples.Tests.Infrastructure;
using MultithreadingExamples.Tests.Infrastructure.StateObservers;
using MultithreadingExamples.ThreadingModels;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.ThreadingModels
{
    [TestFixture]
    public sealed class LockingExampleTests : ExampleTestBase<LockingExample>
    {
        [Test]
        public void BlockOnWpfOnly()
        {
            // Arrange
            var responseStateObserver = new LockingStateObserver(x => x == LockingExample.ResponseState);
            StateMachine.AddObserver(responseStateObserver);

            // Act
            RunExampleInThread();

            // Assert
            Assert.IsTrue(responseStateObserver.Wait(15000, ()=>{}));
        }
    }
}
