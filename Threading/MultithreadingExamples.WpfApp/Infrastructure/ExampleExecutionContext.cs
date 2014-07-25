using System;
using MultithreadingExamples.Infrastructure;

namespace MultithreadingExamples.WpfApp.Infrastructure
{
    public sealed class ExampleExecutionContext : IDisposable, ILog, IInteraction
    {
        private IExample _example;
        public ExampleExecutionContext(IExample example)
        {
            _example = example;
            _example.Log = this;
            _example.Interaction = this;
        }

        public void Run()
        {
            _example.Run();
        }

        public void Dispose()
        {
            if (_example != null)
            {
                _example.Log = null;
                _example.Interaction = null;
                _example.Dispose();
            }

            _example = null;
        }

        public void Log(string message)
        {
            // TODO: Notify
        }

        public void Confirmation()
        {
            
        }
    }
}