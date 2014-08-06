using System;
using System.Windows.Threading;

namespace MultithreadingExamples.WpfApp.Infrastructure
{
    public sealed class UiDispatcher
    {
        private static Dispatcher _dispatcher;
        public static void SetDispatcher(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public static void RunAsyncInUi(Action callback)
        {
            _dispatcher.InvokeAsync(callback);
        }
    }
}
