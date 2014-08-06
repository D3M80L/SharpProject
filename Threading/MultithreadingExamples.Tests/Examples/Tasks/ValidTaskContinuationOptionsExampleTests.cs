using MultithreadingExamples.Examples.Tasks;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.Tasks
{
    [TestFixture]
    public sealed class ValidTaskContinuationOptionsExampleTests : ExampleTestBase<ValidTaskContinuationOptionsExample>
    {
        [Test]
        public void OnFaultedShouleBeFired()
        {
            // Arrange
            var onFaultedStateObserver = new CatchStateObserver(x => x.Contains(TaskContinuationOptionsExampleBase.OnFaultedMessage));
            StateMachine.AddObserver(onFaultedStateObserver);

            // Act
            RunExampleInThread();
            
            // Assert
            Assert.IsTrue(onFaultedStateObserver.Wait(2000));
            Assert.AreEqual(1, onFaultedStateObserver.Count);
        }
    }
}