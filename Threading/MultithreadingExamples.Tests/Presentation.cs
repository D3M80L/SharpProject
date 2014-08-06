using MultithreadingExamples.Examples.CooperativeCancellations;
using MultithreadingExamples.Examples.Signaling;
using MultithreadingExamples.Examples.Tasks;
using MultithreadingExamples.Examples.Threads;
using MultithreadingExamples.Tests.Examples.AsyncAwaits;
using MultithreadingExamples.Tests.Examples.Collections;
using MultithreadingExamples.Tests.Examples.CooperativeCancellations;
using MultithreadingExamples.Tests.Examples.OptimizationSensibles;
using MultithreadingExamples.Tests.Examples.Patterns;
using MultithreadingExamples.Tests.Examples.ReadWrites;
using MultithreadingExamples.Tests.Examples.Signaling;
using MultithreadingExamples.Tests.Examples.Tasks;
using MultithreadingExamples.Tests.Examples.ThreadingModels;
using MultithreadingExamples.Tests.Examples.ThreadLocals;
using MultithreadingExamples.Tests.Examples.Threads;
using MultithreadingExamples.Tests.Infrastructure;

namespace MultithreadingExamples.Tests
{
    /// <summary>
    /// This is just a stub class used for navigating between examples and tests during presentation
    /// </summary>
    public sealed class Presentation
    {
        public void Start()
        {
            AfterIntroduction()
                .ExplainTest<ForegroundThreadPreventsProcessToStopExampleTests>()
                .ExplainTest<UnhandledExceptionInThreadCrashesApplicationExampleTests>()
                .ExplainTest<ModifiedClosureExampleTests>()
                .ExplainTest<AsyncCallbacksExampleTests>()
                .ExplainTest<ThreadAbortExceptionExampleTests>()
                .ExplainTest<ThreadInterrupdedExceptionExampleTests>()
                .ExplainTest<CancellationTokenCallbackExampleTests>()
                .ExplainTest<CancellationTokenCallbackThrowingExceptionExampleTests>()
                .ExplainTest<CancellationTokenRegisteringCallbackAfterTheTokenSourceHasBeenCancelledExampleTests>()
                .ExplainExample<UseIsCancellationRequestedExample>()
                .ExplainExample<UseThrowIfCancellationRequestedExample>()
                .ExplainTest<UnsafeIncrementExampleTests>()
                .ExplainTest<SafeIncrementExampleTests>()
                .ExplainTest<UnsafeVolatileExampleTests>()
                .ExplainExample<InappropriateLockingExample>()
                .ExplainExample<LockExample>();

            AfterShortBreak()
                .ExplainTest<OptimizationSensibleExampleTests>()
                .ExplainTest<BlockOptimizationWithVolatileExampleTests>()
                .ExplainTest<BlockOptimizationWithVolatileReadWriteExampleTests>()
                .ExplainTest<BlockOptimizationWithMemoryBarierExampleTests>()
                .ExplainTest<TimerGarbageCollectorSensibleExampleTests>();

            AfterShortBreak()
                .ExplainTest<CatchExceptionFromTaskExampleTests>()
                .ExplainTest<TaskContinuationExampleTests>()
                .ExplainExample<InvalidTaskContinuationOptionsExample>()
                .ExplainTest<ValidTaskContinuationOptionsExampleTests>()
                .ExplainTest<WaitingForANotStartedTaskBlocksExampleTests>();

            AfterShortBreak()
                .ExplainTest<AsyncTaskExampleTests>()
                .ExplainTest<AsyncVoidCrashingExampleTests>();

            AfterShortBreak()
                .ExplainTest<SharedStaticRandomInstanceExampleTests>()
                .ExplainTest<ThreadStaticRandomInstanceExampleTests>()
                .ExplainTest<ThreadLocalRandomInstanceExampleTests>()
                .ExplainTest<LockingExampleTests>()
                .ExplainTest<DoubleCheckedLazyTests>()
                .ExplainTest<BlockingCollectionExampleTests>();

            AfterShortBreak()
                .ExplainExample<OnlyOneInstanceCanBeExecutedOnMachineExample>()
                .ExplainTest<ManualResetEventExampleTests>()
                .ExplainTest<ManualResetEventSlimExampleTests>()
                .ExplainTest<PulseExampleTests>()
                .ExplainExample<BarrierExample>()
                .ExplainExample<SemaphoreExample>()
                .ExplainExample<SemaphoreSlimExample>()
                .ExplainExample<ContextBoundSynchronizationExample>();
        }

        private Presentation AfterIntroduction()
        {
            return this;
        }

        private Presentation AfterShortBreak()
        {
            return this;
        }
    }
}
