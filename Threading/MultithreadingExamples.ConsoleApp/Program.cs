using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultithreadingExamples.ConsoleApp.Infrastructure;
using MultithreadingExamples.Examples.CooperativeCancellations;
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
            Example<ValidTaskContinuationOptionsExample>,
            Example<InvalidTaskContinuationOptionsExample>,
            Example<TaskCancellation>,
            Example<TaskContinuation>,
            Example<WaitingForANotStartedTaskBlocks>,
            Example<CatchExceptionFromTask>,
            Example<ThrowExceptionInTask>,
            Example<CreateTaskUsingFactory>,
            Example<ModifiedClosureFix>,
            Example<ModifiedClosureExample>,
            Example<ThreadAbortExceptionExample>,
            Example<ThreadInterrupdedExceptionExample>,
            Example<SafeIncrement>,
            Example<UnsafeIncrement>,
            Example<BackgroundThreadAndFinallyBlock>,
            Example<ForegroundThreadPreventsProcessToStop>,
            Example<UnhandledExceptionInThread>,
            Example<CancellationTokenCallbackThrowingException>,
            Example<RegisteringCallbackAfterTheTokenSourceHasBeenCancelled>,
            Example<UseThrowIfCancellationRequested>,
            Example<CancellationTokenCallback>,
            Example<UseIsCancellationRequested>
        };

        static void Main(string[] args)
        {
            var example = Examples.First()();
            example.Log = new Logger();
            example.ConsoleInput = new ConsoleInput();

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
