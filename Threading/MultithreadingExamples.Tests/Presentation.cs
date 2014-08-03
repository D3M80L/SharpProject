using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MultithreadingExamples.Examples.OptimizationSensibles;
using MultithreadingExamples.Examples.ReadWrites;
using MultithreadingExamples.Examples.Signaling;
using MultithreadingExamples.Examples.Tasks;
using MultithreadingExamples.Examples.Threads;
using MultithreadingExamples.Tests.Examples.AsyncAwaits;
using MultithreadingExamples.Tests.Examples.Collections;
using MultithreadingExamples.Tests.Examples.CooperativeCancellations;
using MultithreadingExamples.Tests.Examples.OptimizationSensibles;
using MultithreadingExamples.Tests.Examples.Patterns;
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
                .ExplainTest<CancellationTokenCallbackExamplesTests>()
                .ExplainTest<CancellationTokenCallbackThrowingExceptionTests>()
                .ExplainExample<UnsafeIncrement>()
                .ExplainExample<SafeIncrement>()
                .ExplainExample<InappropriateLockingExample>()
                .ExplainExample<LockingExample>();

            AfterShortBreak()
                .ExplainTest<OptimizationSensibleTests>()
                .ExplainTest<TimerGarbageCollectorSensibleExampleTests>();

            AfterShortBreak()
                .ExplainTest<CatchExceptionFromTaskExampleTests>()
                .ExplainTest<TaskContinuationExampleTests>()
                .ExplainExample<InvalidTaskContinuationOptionsExample>()
                .ExplainTest<ValidTaskContinuationOptionsExampleTests>()
                .ExplainTest<WaitingForANotStartedTaskBlocksExampleTests>();

            AfterShortBreak()
                .ExplainTest<AsyncTaskExampleTests>()
                .ExplainTest<AsyncVoidExampleTests>();

            AfterShortBreak()
                .ExplainTest<ThreadLocalsTests>()
                .ExplainTest<LockingExampleTests>()
                .ExplainTest<DoubleCheckedLazyTests>()
                .ExplainTest<BlockingCollectionExampleTests>();

            AfterShortBreak()
                .ExplainTest<OnlyOneInstanceCanBeExecutedOnMachine>()
                .ExplainTest<ManualResetEventExampleTests>()
                .ExplainTest<ManualResetEventSlimExampleTests>()
                .ExplainTest<PulseExampleTests>()
                .ExplainExample<BarrierExample>();
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
