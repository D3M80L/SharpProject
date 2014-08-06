using MultithreadingExamples.Examples.ReadWrites;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.ReadWrites
{
    [TestFixture]
    public class UnsafeIncrementExampleTests : ExampleTestBase<UnsafeIncrementExample>
    {
        [Ignore]
        [Test(Description = @"This test can succeed on some less core machines.")]
        public void AddOperationIsNotAtomicAndAsAResultTestFailsSometimes()
        {
            // Arrange
            var xMessageObserver = new CatchStateObserver(x => x == ReadWritesBase.XMessage + ReadWritesBase.HowManyThreadsToUse);
            StateMachine.AddObserver(xMessageObserver);

            var yMessageObserver = new CatchStateObserver(x => x == ReadWritesBase.YMessage + ReadWritesBase.HowManyThreadsToUse);
            StateMachine.AddObserver(yMessageObserver);

            // Act
            RunExampleInThread();
            WaitForExitConfirmation(5000);

            // Assert
            Assert.AreEqual(1, xMessageObserver.Count);
            Assert.AreEqual(1, yMessageObserver.Count);
        }
    }
}