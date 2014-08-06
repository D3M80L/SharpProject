using MultithreadingExamples.Examples.OptimizationSensibles;
using MultithreadingExamples.Tests.Infrastructure;
using MultithreadingExamples.Tests.Infrastructure.StateObservers;

namespace MultithreadingExamples.Tests.Examples.OptimizationSensibles
{
    public abstract class OptimizationSensibleCounterExampleTestBase<TOptimizationSensibleExample> : ExampleTestBase<TOptimizationSensibleExample> 
        where TOptimizationSensibleExample : OptimizationSensibleCounterExampleBase, new()
    {
        protected CatchStateObserver CountingFinishedState;
        protected ExclusiveLockStateObserver ConfirmCountingStop;

        protected override void OnTestSetup()
        {
            CountingFinishedState = StateMachine.AddObserver(new CatchStateObserver(state => state == OptimizationSensibleCounterExampleBase.CountingFinished));
            ConfirmCountingStop = StateMachine.AddObserver(new ExclusiveLockStateObserver(state => state == OptimizationSensibleCounterExampleBase.ConfirmCountingStopMessage));
        }

        protected void WaitForCountingFinish()
        {
            ConfirmCountingStop.Wait(5000, () => { });
        }
    }
}