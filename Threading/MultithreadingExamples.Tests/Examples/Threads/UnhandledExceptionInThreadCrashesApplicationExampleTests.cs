using MultithreadingExamples.Examples.Threads;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.Threads
{
    [TestFixture]
    public sealed class UnhandledExceptionInThreadCrashesApplicationExampleTests : ExampleTestBase<UnhandledExceptionInThreadCrashesApplicationExample>
    {
        /// <summary>
        /// Run this example not in standalone WpfApp.
        /// IMPORTANT: This test will succeed in Nunit and Resharper
        /// SET legacyUnhandledExceptionPolicy enabled="1" in JetBrains config file
        /// see http://gojisoft.com/blog/2010/05/14/resharper-test-runner-hidden-thread-exceptions/ for details
        /// </summary>
        [Test]
        public void ThreadThrowsExceptionButTheTestsSucceeds()
        {
            // Arrange
            var finishedOtherWorkState = new CatchStateObserver(x => x == UnhandledExceptionInThreadCrashesApplicationExample.FinishedOtherWorkState);
            StateMachine.AddObserver(finishedOtherWorkState);

            // Act
            RunExampleInThread();

            // Assert
            Assert.IsTrue(finishedOtherWorkState.Wait(5000));
        }
    }
}