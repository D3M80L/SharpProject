using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultithreadingExamples.Examples.Threads;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.Threads
{
    [TestFixture]
    public sealed class AsyncCallbacksExampleTests : ExampleTestBase<AsyncCallbacksExample>
    {
        [Test]
        public void RunExample()
        {
            // Arrange
            var afterEndInvoke = new CatchStateObserver(x => x == AsyncCallbacksExample.AfterEndInvoke);
            StateMachine.AddObserver(afterEndInvoke);

            var importantException = new CatchStateObserver(x => x == AsyncCallbacksExample.ImportantException);
            StateMachine.AddObserver(importantException);

            // Act
            RunExampleInThread();

            // Assert
            Assert.IsTrue(importantException.Wait(2000));
            Assert.IsFalse(afterEndInvoke.Wait(2000));
        }
    }
}
