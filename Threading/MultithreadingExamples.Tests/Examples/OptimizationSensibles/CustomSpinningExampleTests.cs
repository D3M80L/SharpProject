using MultithreadingExamples.Examples.OptimizationSensibles;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.OptimizationSensibles
{
    [TestFixture]
    public sealed class CustomSpinningExampleTests : OptimizationSensibleCounterExampleTestBase<CustomSpinningExample>
    {
        [Test]
        public void RunExample()
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