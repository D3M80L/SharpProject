using MultithreadingExamples.Examples.Tasks;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.Tasks
{
    [TestFixture]
    public sealed class ValidTaskContinuationOptionsExampleTests : ExampleTestBase<ValidTaskContinuationOptionsExample>
    //public sealed class ValidTaskContinuationOptionsExampleTests : ExampleTestBase<InvalidTaskContinuationOptionsExample>
    {
        [Test]
        public void OnFaultedShouleBeFired()
        {
            // Arrange
            var onFaultedStateObserver = new CatchStateObserver(x => x.Contains(TaskContinuationOptionsExampleBase.OnFaultedMessage));
            StateMachine.AddObserver(onFaultedStateObserver);

            // Act
            RunExampleInThread();
            WaitForExitConfirmation(1000);

            // Assert
            Assert.AreEqual(1, onFaultedStateObserver.Count);
        }
    }
}