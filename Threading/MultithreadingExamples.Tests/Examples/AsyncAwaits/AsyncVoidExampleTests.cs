﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultithreadingExamples.Examples.AsyncAwaits;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.AsyncAwaits
{
    [TestFixture]
    public sealed class AsyncVoidExampleTests : ExampleTestBase<AsyncVoidCrashingExample>
    {
        [Test]
        public void CrashesTheApplication()
        {
            // Arrange
            var waitForException = new CatchStateObserver(x => x == ExampleBase.ImportantException);
            StateMachine.AddObserver(waitForException);

            // Act
            RunExampleInThread();

            // Assert
            Assert.IsFalse(waitForException.Wait(5000));

        }
    }
}
