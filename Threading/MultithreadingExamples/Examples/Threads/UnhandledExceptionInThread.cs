using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Exceptions;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Threads
{
    // There are few exceptions4exceptions - see: http://msdn.microsoft.com/en-us/library/ms228965.aspx
    // for explanation see: http://msdn.microsoft.com/en-us/library/system.appdomain.unhandledexception.aspx
    // MSDN: In the .NET Framework versions 1.0 and 1.1, an unhandled exception that occurs in a thread other than the main application thread 
    // is caught by the runtime and therefore does not cause the application to terminate. 
    // Thus, it is possible for the UnhandledException event to be raised without the application terminating. \
    // Starting with the .NET Framework version 2.0, this backstop for unhandled exceptions in child threads was removed, 
    // because the cumulative effect of such silent failures included performance degradation, corrupted data, and lockups, all of which were difficult to debug. 
    public sealed class UnhandledExceptionInThread : ThreadExampleBase, IImportantExample
    {
        protected override void OnRun()
        {
            StartThread();

            MakeSomeOtherWork();
        }

        private void StartThread()
        {
            var thread = new Thread(start: RunInThread);
            Log.Info(StartingThread);
            thread.Start();
        }

        private void MakeSomeOtherWork()
        {
            Log.Info("DoingSomethingElse");
            Thread.Sleep(5000);
        }

        private void RunInThread()
        {
            Thread.Sleep(1000);
            Log.Info("ThrowingException");
            throw new VeryImportantException(); // An unhandled exception crashes the whole application
        }
    }
}
