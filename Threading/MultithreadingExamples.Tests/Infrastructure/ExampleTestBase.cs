using System;
using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Tests.Infrastructure.StateObservers;
using NUnit.Framework;
using Rhino.Mocks;

namespace MultithreadingExamples.Tests.Infrastructure
{
    public abstract class ExampleTestBase
    {
        
    }

    public abstract class ExampleTestBase<TExample> : ExampleTestBase
        where TExample : ExampleBase, new()
    {
        protected TExample Example { get; private set; }

        protected ILog Log { get; private set; }

        protected IInteraction Interaction { get; private set; }

        protected StateMachine StateMachine { get; private set; }

        private LockingStateObserver _exitLockingStateObserver;

        [SetUp]
        public void TestSetup()
        {
            Example = new TExample();

            StateMachine = new StateMachine();
            StateMachine.AddObserver(new OnlyDisplayStateObserver());
            _exitLockingStateObserver = StateMachine.AddObserver(new LockingStateObserver(x => x == ExampleBase.PressEnterToExit));

            Log          = new StateMachineLoggerMock(StateMachine);
            Interaction = MockRepository.GenerateStub<IInteraction>();
            Interaction
                .Stub(x => x.ConfirmationRequest());

            Example.Log = Log;
            Example.Interaction = Interaction;

            OnTestSetup();
        }

        protected virtual void OnTestSetup()
        {
            
        }

        [TearDown]
        public void TearDown()
        {
            _exitLockingStateObserver = null;
            Log = null;
            Interaction = null;
            Example.Dispose();

            OnTearDown();
        }

        protected virtual void OnTearDown()
        {
            
        }

        protected void RunExampleInThread()
        {
            ThreadPool.QueueUserWorkItem(_ => Example.Run());
        }

        protected void WaitForExitConfirmation(int timeout)
        {
            if (!_exitLockingStateObserver.Wait(timeout, () => { }))
            {
                throw new TimeoutException("WaitForExitConfirmation");
            }
        }
    }
}