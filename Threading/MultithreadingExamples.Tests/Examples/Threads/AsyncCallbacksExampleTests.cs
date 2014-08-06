using MultithreadingExamples.Examples.Threads;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.Threads
{
    [TestFixture]
    public sealed class AsyncCallbacksExampleTests : ExampleTestBase<AsyncCallbacksExample>
    {
        [Test]
        public void ImportantExceptionHasBeenCatched()
        {
            // Arrange
            var afterEndInvoke = new CatchStateObserver(x => x == AsyncCallbacksExample.AfterEndInvokeState);
            StateMachine.AddObserver(afterEndInvoke);

            var importantException = new CatchStateObserver(x => x == ExampleBase.ImportantExceptionState);
            StateMachine.AddObserver(importantException);

            // Act
            RunExampleInThread();

            // Assert
            Assert.IsTrue(importantException.Wait(3000), "Important exception not caught in time.");
            Assert.IsFalse(afterEndInvoke.Wait(3000), "After end invoke not caught in time.");
        }
    }
}
