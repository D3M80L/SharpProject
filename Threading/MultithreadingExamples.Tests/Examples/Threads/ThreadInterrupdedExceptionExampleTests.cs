using MultithreadingExamples.Examples.Threads;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.Threads
{
    [TestFixture]
    public sealed class ThreadInterrupdedExceptionExampleTests : ExampleTestBase<ThreadInterrupdedExceptionExample>
    {
        [Test]
        public void OneThreadInterrupdedExceptionMessageShouldBeCaught()
        {
            // Arrange
            var exitingWorkerThreadState = new CatchStateObserver(x => x == ThreadInterrupdedExceptionExample.ExitingWorkerThreadState);
            StateMachine.AddObserver(exitingWorkerThreadState);

            var countInterruptedExceptionMessageStates = new CatchStateObserver(x => x == ThreadInterrupdedExceptionExample.ThreadInterruptedExceptionState);
            StateMachine.AddObserver(countInterruptedExceptionMessageStates);

            // Act
            RunExampleInThread();

            // Assert
            Assert.IsTrue(exitingWorkerThreadState.Wait(5000));
            Assert.AreEqual(1, countInterruptedExceptionMessageStates.Count);
        }
    }
}