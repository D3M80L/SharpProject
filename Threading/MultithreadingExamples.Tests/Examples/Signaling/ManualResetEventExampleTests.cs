using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultithreadingExamples.Examples.Signaling;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.Signaling
{
    [TestFixture]
    public sealed class ManualResetEventExampleTests : ExampleTestBase<ManualResetEventExample>
    {
        [Test]
        public void WaitForSignal()
        {
            // Arrange
            var state = new CatchStateObserver(x => x == ResetEventExampleBase.AfterWaiting);
            StateMachine.AddObserver(state);

            // Act
            RunExampleInThread();

            // Assert
            Assert.IsTrue(state.Wait(5000));
        }
    }
}
