using MultithreadingExamples.Examples.Tasks;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.Tasks
{
    [TestFixture]
    public sealed class TaskContinuationExampleTests : ExampleTestBase<TaskContinuationExample>
    {
        [Test]
        public void RunExample()
        {
            // Arrange

            // Act
            RunExampleInThread();
            WaitForExitConfirmation(1000);

            // Assert

        }
    }
}