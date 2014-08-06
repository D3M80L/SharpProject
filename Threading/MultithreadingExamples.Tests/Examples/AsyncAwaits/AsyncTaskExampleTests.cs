using MultithreadingExamples.Examples.AsyncAwaits;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.AsyncAwaits
{
    [TestFixture]
    public sealed class AsyncTaskExampleTests : ExampleTestBase<AsyncTaskExample>
    {
        [Test]
        public void ExceptionThrownButStoredInTask()
        {
            // Arrange
            var waitForException = new CatchStateObserver(x => x == ExampleBase.ImportantExceptionState);
            StateMachine.AddObserver(waitForException);

            // Act
            RunExampleInThread();

            // Assert
            Assert.IsFalse(waitForException.Wait(5000));
        }
    }
}