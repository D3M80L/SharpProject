using MultithreadingExamples.Examples.Signaling;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.Signaling
{
    [TestFixture]
    public sealed class ManualResetEventSlimExampleTests : ExampleTestBase<ManualResetEventSlimExample>
    {
        [Test]
        public void WaitForSignal()
        {
            // Arrange
            var state = new CatchStateObserver(x => x == ResetEventExampleBase.AfterWaitingState);
            StateMachine.AddObserver(state);

            // Act
            RunExampleInThread();

            // Assert
            Assert.IsTrue(state.Wait(5000));
        }
    }
}