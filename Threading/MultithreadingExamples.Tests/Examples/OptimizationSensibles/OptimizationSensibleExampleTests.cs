using MultithreadingExamples.Examples.OptimizationSensibles;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.OptimizationSensibles
{
    [TestFixture]
    public sealed class OptimizationSensibleExampleTests : OptimizationSensibleCounterExampleTestBase<OptimizationSensibleExample>
    {
        [Test]
        public void OptimizationSensible_FailsInRelease()
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