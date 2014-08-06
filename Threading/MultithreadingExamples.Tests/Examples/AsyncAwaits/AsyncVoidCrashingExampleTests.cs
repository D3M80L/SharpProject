using MultithreadingExamples.Examples.AsyncAwaits;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.AsyncAwaits
{
    [TestFixture]
    public sealed class AsyncVoidCrashingExampleTests : ExampleTestBase<AsyncVoidCrashingExample>
    {
        [Test]
        public void CrashesTheApplication()
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
