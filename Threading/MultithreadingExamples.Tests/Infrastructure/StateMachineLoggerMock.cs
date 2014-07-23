using MultithreadingExamples.Infrastructure;

namespace MultithreadingExamples.Tests.Infrastructure
{
    public sealed class StateMachineLoggerMock : ILog
    {
        private StateMachine _stateMachine = new StateMachine();

        public StateMachineLoggerMock(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Log(string message)
        {
            _stateMachine.SetState(message);
        }
    }
}