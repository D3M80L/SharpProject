using System;
using System.Diagnostics;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Infrastructure
{
    public abstract class ExampleBase : IExample
    {
        public const string Running = "Running";
        public const string Finished = "Finished";
        public const string PressEnterToContinue = "PressEnterToContinue";
        public const string PressEnterToExit = "PressEnterToExit";
        public const string ImportantExceptionState = "ImportantExceptionState";
        public const string AggregateExceptionMessage = "AggregateExceptionMessage";

        public ILog Log { get; set; }

        public IInteraction Interaction { get; set; }

        public void Run()
        {
            Log.Info(Running);
            LogDebugWhenCompiledDebug();
            OnRun();
            ConfirmExit();
            Log.Info(Finished);
        }

        protected abstract void OnRun();

        private bool _isDisposed;
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;
            try
            {
                OnDispose();
            } catch
            { }
        }

        protected virtual void OnDispose()
        {

        }

        [Conditional("DEBUG")]
        private void LogDebugWhenCompiledDebug()
        {
            Log.Info("DEBUG");
        }


        private void ConfirmExit()
        {
            Log.Info(PressEnterToExit);
            if (Interaction != null)
            {
                Interaction.ConfirmationRequest();
            }
        }
    }
}