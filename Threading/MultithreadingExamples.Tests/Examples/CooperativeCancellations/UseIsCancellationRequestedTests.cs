using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultithreadingExamples.Examples.CooperativeCancellations;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.CooperativeCancellations
{
    [TestFixture]
    internal sealed class UseIsCancellationRequestedTests : ExampleTestBase<UseIsCancellationRequested>
    {
        [Test]
        public void CancelImmediately()
        {
            // Arrange

            // Act
            Example.Run();

            // Assert

        }
    }
}
