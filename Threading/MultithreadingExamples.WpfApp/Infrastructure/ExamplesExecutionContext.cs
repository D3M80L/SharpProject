using System;
using System.Threading;
using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure;

namespace MultithreadingExamples.WpfApp.Infrastructure
{
    public sealed class ExamplesExecutionContext : IDisposable, ILog, IInteraction
    {
        private IExample _example;
        private Action<string> _logHandler;
        private Action _confirmationRequestHandler;

        public ExamplesExecutionContext(IExample example, Action<string> logHandler, Action confirmationRequestHandler)
        {
            _example = example;
            _example.Log = this;
            _example.Interaction = this;
            _logHandler = logHandler;
            _confirmationRequestHandler = confirmationRequestHandler;
        }

        public void Run()
        {
            Task.Run(()=>_example.Run());
        }

        public void Dispose()
        {
            if (_example != null)
            {
                _logHandler = null;
                _confirmationRequestHandler = null;
                _example.Log = null;
                _example.Interaction = null;
                _example.Dispose();
            }

            _example = null;
        }

        public void Log(string message)
        {
            try
            {
                var logAction = _logHandler;
                if (logAction != null)
                {
                    logAction(message);
                }
            }
            catch
            {
                
            }
        }

        private ManualResetEventSlim _manualReset = new ManualResetEventSlim(false);
        public void ConfirmationRequest()
        {
            _confirmationRequestHandler();
            _manualReset.Wait();
        }

        public void Confirm()
        {
            _manualReset.Set();
        } 
    }
}