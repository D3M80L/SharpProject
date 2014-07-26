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

        protected IInteraction Interaction { get; private set; }

        protected StateMachine StateMachine { get; private set; }

        [SetUp]
        public void TestSetup()
        {
            Example = new TExample();

            StateMachine = new StateMachine();
            StateMachine.AddObserver(new OnlyDisplayStateObserver());
            StateMachine.AddObserver(new LockingStateObserver(x => x == ExampleBase.PressEnterToExit));

            Log          = new StateMachineLoggerMock(StateMachine);
            Interaction = MockRepository.GenerateStub<IInteraction>();
            Interaction
                .Stub(x => x.ConfirmationRequest());

            Example.Log = Log;
            Example.Interaction = Interaction;
        }

        [TearDown]
        public void TearDown()
        {
            Log = null;
            Interaction = null;
            Example.Dispose();
        }

        protected void RunExampleInThread()
        {
            ThreadPool.QueueUserWorkItem(_ => Example.Run());
        }
    }
}