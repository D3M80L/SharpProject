using MultithreadingExamples.Examples.Tasks;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.Tasks
{
    [TestFixture]
    public sealed class CatchExceptionFromTaskExampleTests : ExampleTestBase<CatchExceptionFromTaskExample>
    {
        [Test]
        public void AnyExceptionFromTaskIsRethrownInAggregateException()
        {
            // Arrange
            var aggregateExceptionState = new CatchStateObserver(x=>x == ExampleBase.AggregateExceptionMessage);
            StateMachine.AddObserver(aggregateExceptionState);

            // Act
            RunExampleInThread();
            WaitForExitConfirmation(5000);

            // Asert
            Assert.AreEqual(1, aggregateExceptionState.Count);
        }
    }
}
