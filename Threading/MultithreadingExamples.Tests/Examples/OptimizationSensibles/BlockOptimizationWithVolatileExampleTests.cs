using MultithreadingExamples.Examples.OptimizationSensibles;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.OptimizationSensibles
{
    [TestFixture]
    public sealed class BlockOptimizationWithVolatileExampleTests : OptimizationSensibleCounterExampleTestBase<BlockOptimizationWithVolatileExample>
    {
        [Test]
        public void VolatileFields_ShouldReachFinishState()
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