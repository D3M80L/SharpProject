using MultithreadingExamples.Examples.OptimizationSensibles;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.OptimizationSensibles
{
    [TestFixture]
    public sealed class BlockOptimizationWithMemoryBarierExampleTests : OptimizationSensibleCounterExampleTestBase<BlockOptimizationWithMemoryBarierExample>
    {
        [Test]
        public void MemoryBarier_ShouldReachFinishState()
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