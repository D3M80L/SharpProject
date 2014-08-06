using System;
using System.Collections.Generic;
using System.Linq;
using MultithreadingExamples.ConsoleApp.Infrastructure;
using MultithreadingExamples.Examples.CooperativeCancellations;
using MultithreadingExamples.Examples.OptimizationSensibles;
using MultithreadingExamples.Examples.ReadWrites;
using MultithreadingExamples.Examples.Tasks;
using MultithreadingExamples.Examples.Threads;
using MultithreadingExamples.Infrastructure;

namespace MultithreadingExamples.ConsoleApp
{
    class Program
    {
        private static readonly List<Func<ExampleBase>> Examples = new List<Func<ExampleBase>>
        {
            //Example<OptimizationSensibleExample>,
            Example<TimerGarbageCollectorSensibleExample>,
            Example<ValidTaskContinuationOptionsExample>,
            Example<InvalidTaskContinuationOptionsExample>,
            Example<TaskCancellation>,
            Example<TaskContinuationExample>,
            Example<WaitingForANotStartedTaskBlocksExample>,
            Example<CatchExceptionFromTaskExample>,
            Example<ThrowExceptionInTaskExample>,
            Example<CreateTaskUsingFactoryExample>,
            Example<ModifiedClosureFixExample>,
            Example<ModifiedClosureExample>,
            Example<ThreadAbortExceptionExample>,
            Example<ThreadInterrupdedExceptionExample>,
            Example<SafeIncrementExample>,
            Example<UnsafeIncrementExample>,
            Example<BackgroundThreadAndFinallyBlockExample>,
            Example<ForegroundThreadPreventsProcessToStopExample>,
            Example<UnhandledExceptionInThreadCrashesApplicationExample>,
            Example<CancellationTokenCallbackThrowingExceptionExample>,
            Example<CancellationTokenRegisteringCallbackAfterTheTokenSourceHasBeenCancelledExample>,
            Example<UseThrowIfCancellationRequestedExample>,
            Example<CancellationTokenCallbackExample>,
            Example<UseIsCancellationRequestedExample>
        };

        static void Main(string[] args)
        {
            var example = Examples.First()();
            example.Log = new Logger();
            example.Interaction = new Interaction();

            example.Run();

            example.Dispose();
        }

        private static TExample Example<TExample>()
            where TExample : ExampleBase, new()
        {
            return new TExample();
        }
    }
}
