using System;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Infrastructure
{
    public abstract class ExampleBase : IExample
    {
        public const string Running = "Running";
        public const string Finished = "Finished";
        public const string PressEnterToContinue = "PressEnterToContinue";

        public ILog Log { get; set; }

        public IConsoleInput ConsoleInput { get; set; }

        public void Run()
        {
            Log.Info(Running);
            OnRun();
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
    }
}