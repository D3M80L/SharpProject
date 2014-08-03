using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultithreadingExamples.Examples.Threads;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.Threads
{
    [TestFixture]
    public sealed class ForegroundThreadPreventsProcessToStopExampleTests : ExampleTestBase<ForegroundThreadPreventsProcessToStopExample>
    {
        [Test]
        public void RunExample()
        {
            // Arrange

            // Act
            RunExampleInThread();
            WaitForExitConfirmation(1000);

            // Assert
        }
    }
}
