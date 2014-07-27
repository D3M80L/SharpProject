using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultithreadingExamples.Examples.AsyncAwaits;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.AsyncAwaits
{
    [TestFixture]
    public sealed class AsyncVoidExampleTests : ExampleTestBase<AsyncVoidCrashingExample>
    {
        [Test]
        public void OnUiThreadThisExampleCrashesTheApplication()
        {
            // Arrange
            var waitForException = new CatchStateObserver(x => x == AsyncVoidCrashingExample.VeryImportantException);
            StateMachine.AddObserver(waitForException);

            // Act
            RunExampleInThread();

            // Assert
            Assert.IsTrue(waitForException.Wait(5000));

        }
    }
}
