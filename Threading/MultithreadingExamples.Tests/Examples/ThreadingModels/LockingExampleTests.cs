using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Tests.Infrastructure;
using MultithreadingExamples.Tests.Infrastructure.StateObservers;
using MultithreadingExamples.ThreadingModels;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.ThreadingModels
{
    [TestFixture]
    public sealed class LockingExampleTests : ExampleTestBase<LockingExample>
    {
        [Test]
        public void BlockOnWpfOnly()
        {
            // Arrange
            var stateObserver = new LockingStateObserver(x => x == LockingExample.Response);
            StateMachine.AddObserver(stateObserver);

            // Act
            RunExampleInThread();

            // Assert
            Assert.IsTrue(stateObserver.Wait(15000, ()=>{}));
        }
    }
}
