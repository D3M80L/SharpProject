using System;
using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Tests.Infrastructure.StateObservers;
using NUnit.Framework;
using Rhino.Mocks;

namespace MultithreadingExamples.Tests.Infrastructure
{
    public abstract class ExampleTestBase<TExample>
        where TExample : ExampleBase, new()
    {
        protected TExample Example { get; private set; }

        protected ILog Log { get; private set; }

        protected IConsoleInput ConsoleInput { get; private set; }

        protected StateMachine StateMachine { get; private set; }

        [SetUp]
        public void TestSetup()
        {
            Example = new TExample();

            StateMachine = new StateMachine();
            StateMachine.AddObserver(new OnlyDisplayStateObserver());
            StateMachine.AddObserver(new LockingStateObserver(x => x == ExampleBase.PressEnterToExit));

            Log          = new StateMachineLoggerMock(StateMachine);
            ConsoleInput = MockRepository.GenerateStub<IConsoleInput>();
            ConsoleInput
                .Stub(x => x.ReadLine())
                .Return(Environment.NewLine);

            Example.Log = Log;
            Example.ConsoleInput = ConsoleInput;
        }

        [TearDown]
        public void TearDown()
        {
            Log = null;
            ConsoleInput = null;
            Example.Dispose();
        }

        protected void RunExampleInThread()
        {
            ThreadPool.QueueUserWorkItem(_ => Example.Run());
        }
    }
}