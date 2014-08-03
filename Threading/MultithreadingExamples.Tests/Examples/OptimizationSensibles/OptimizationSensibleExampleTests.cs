using System;
using MultithreadingExamples.Examples.OptimizationSensibles;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Tests.Infrastructure;
using MultithreadingExamples.Tests.Infrastructure.StateObservers;
using NUnit.Framework;
using Rhino.Mocks;

namespace MultithreadingExamples.Tests.Examples.OptimizationSensibles
{
    /// <summary>
    /// Dobug / Release results differs
    /// </summary>
    [TestFixture]
    public sealed class OptimizationTests
    {
        [Test]
        public void OptimizationSensibleExample()
        {
            TestExample<OptimizationSensibleExample>();
        }

        [Test]
        public void BlockOptimizationWithVolatileReadWriteExample()
        {
            TestExample<BlockOptimizationWithVolatileReadWriteExample>();
        }

        [Test]
        public void BlockOptimizationWithVolatileExample()
        {
            TestExample<BlockOptimizationWithVolatileExample>();
        }

        [Test]
        public void BlockOptimizationWithMemoryBarierExample()
        {
            TestExample<BlockOptimizationWithMemoryBarierExample>();
        }

        private void TestExample<TExample>()
            where TExample : OptimizationSensibleCounterExampleBase, new()
        {
            var example = new OptimizationSensibleCounterExample<TExample>();
            example.TestSetup();

            example.Arrange();

            example.Act();

            Assert.IsTrue(example.Assert());

            example.TearDown();
        }

        private sealed class OptimizationSensibleCounterExample<TExample> : ExampleTestBase<TExample>
            where TExample : OptimizationSensibleCounterExampleBase, new()
        {
            private CatchStateObserver _countingFinished;
            private ExclusiveLockStateObserver _confirmCountingStop;

            public void Arrange()
            {
                _countingFinished = StateMachine.AddObserver(new CatchStateObserver(state => state == OptimizationSensibleCounterExampleBase.CountingFinished));
                _confirmCountingStop = StateMachine.AddObserver(new ExclusiveLockStateObserver(state => state == OptimizationSensibleCounterExampleBase.ConfirmCountingStopMessage));
            }

            public void Act()
            {
                RunExampleInThread();
                _confirmCountingStop.Wait(5000, action: () => { });
            }

            public bool Assert()
            {
                return _countingFinished.Wait(5000);
            }
        }

    }
}