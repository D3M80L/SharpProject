using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultithreadingExamples.Examples.CooperativeCancellations;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.CooperativeCancellations
{
    [TestFixture]
    public sealed class CancellationTokenCallbackThrowingExceptionTests : ExampleTestBase<CancellationTokenCallbackThrowingExceptionExample>
    {
        [Test]
        public void ThrowOnFirstExceptionSetToFalse_AggregateExceptionShouldBeThrown()
        {
            // Arrange
            var aggregateExceptionObserver = new CatchStateObserver(x => x == ExampleBase.AggregateExceptionMessage);
            StateMachine.AddObserver(aggregateExceptionObserver);

            var importantExceptionObserver = new CatchStateObserver(x => x == ExampleBase.ImportantException);
            StateMachine.AddObserver(importantExceptionObserver);

            Example.ThrowOnFirstException = false;

            // Act
            RunExampleInThread();
            WaitForExitConfirmation(1000);

            // Assert
            Assert.AreEqual(0, importantExceptionObserver.Count);
            Assert.AreEqual(1, aggregateExceptionObserver.Count);
        }

        [Test]
        public void ThrowOnFirstExceptionSetToTrue_FirstExceptionShouldBeThrown()
        {
            // Arrange
            var aggregateExceptionObserver = new CatchStateObserver(x => x == ExampleBase.AggregateExceptionMessage);
            StateMachine.AddObserver(aggregateExceptionObserver);

            var importantExceptionObserver = new CatchStateObserver(x => x == ExampleBase.ImportantException);
            StateMachine.AddObserver(importantExceptionObserver);

            Example.ThrowOnFirstException = true;

            // Act
            RunExampleInThread();
            WaitForExitConfirmation(1000);

            // Assert            
            Assert.AreEqual(1, importantExceptionObserver.Count);
            Assert.AreEqual(0, aggregateExceptionObserver.Count);
        }

        [Test]
        public void OnlyOneCancellationCallbackIsFired_OnlyOneCallbackThrowingExceptionMessageIsCaught()
        {
            // Arrange
            var aggregateExceptionObserver = new CatchStateObserver(x => x == CancellationTokenCallbackThrowingExceptionExample.CallbackThrowingExceptionMessage);
            StateMachine.AddObserver(aggregateExceptionObserver);

            Example.ThrowOnFirstException = true;

            // Act
            RunExampleInThread();
            WaitForExitConfirmation(1000);

            // Assert            
            Assert.AreEqual(1, aggregateExceptionObserver.Count);
        }
    }

}
