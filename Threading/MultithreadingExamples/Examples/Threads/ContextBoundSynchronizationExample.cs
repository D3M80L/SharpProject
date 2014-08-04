using System;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Threads
{
    public sealed class ContextBoundSynchronizationExample : ExampleBase
    {
        private ContextBoundClass _contextBoundClass;
        protected override void OnRun()
        {
            _contextBoundClass = new ContextBoundClass(Log);
            ThreadPool.QueueUserWorkItem(_ => RunInThread());
            ThreadPool.QueueUserWorkItem(_ => RunInThread());
            ThreadPool.QueueUserWorkItem(_ => RunInThread());
            ThreadPool.QueueUserWorkItem(_ => RunInThread());
        }

        private void RunInThread()
        {
            _contextBoundClass.Run();
        }

        [Synchronization]
        public sealed class ContextBoundClass : ContextBoundObject
        {
            public const string Entered = "Entered";
            public const string Exitting = "Exitting";

            private ILog _log;
            public ContextBoundClass(ILog log)
            {
                _log = log;
            }

            /// <summary>
            /// Only one thread can perform some operations in this class
            /// </summary>
            public void Run()
            {
                _log.Info(Entered);
                Thread.Sleep(250);
                _log.Info(Exitting);
            }
        }
    }
}