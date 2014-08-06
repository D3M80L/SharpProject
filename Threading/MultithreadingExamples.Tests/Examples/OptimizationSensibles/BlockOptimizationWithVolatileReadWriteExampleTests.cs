using MultithreadingExamples.Examples.OptimizationSensibles;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.OptimizationSensibles
{
    [TestFixture]
    public sealed class BlockOptimizationWithVolatileReadWriteExampleTests : OptimizationSensibleCounterExampleTestBase<BlockOptimizationWithVolatileReadWriteExample>
    {
        [Test]
        public void VolatileReadWrite_ShouldReachFinishState()
        {
            // Arrange

            // Act
            RunExampleInThread();
            WaitForCountingFinish();

            // Assert
            Assert.IsTrue(CountingFinishedState.Wait(5000));
        }
    }
}