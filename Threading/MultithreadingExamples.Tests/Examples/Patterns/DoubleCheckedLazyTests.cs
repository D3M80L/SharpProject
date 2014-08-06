using MultithreadingExamples.Examples.Patterns;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.Patterns
{
    [TestFixture]
    public sealed class DoubleCheckedLazyTests : ExampleTestBase<DoubleCheckedLazyExample>
    {

        [Test]
        public void CounterShouldEqualOne()
        {
            // Arrange
            var waitForState = new CatchStateObserver(x => x == "COUNTER=1");
            StateMachine.AddObserver(waitForState);

            // Act
            RunExampleInThread();

            // Assert
            Assert.IsTrue(waitForState.Wait(5000));
        }
    }
}
