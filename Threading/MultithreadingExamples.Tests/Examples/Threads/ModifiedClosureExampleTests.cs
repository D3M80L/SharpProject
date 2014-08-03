using System.Linq;
using MultithreadingExamples.Examples.Threads;
using MultithreadingExamples.Tests.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.Threads
{
    [TestFixture]
    public sealed class ModifiedClosureExampleTests : ExampleTestBase<ModifiedClosureExample>
    {

        [Ignore]
        [Test]
        public void TestFailsBecauseThereAreNoUniqueMessages()
        {
            // Arrange
            var countdownEvents = new CountdownAndRememberStateObserver(x=>x.StartsWith(ModifiedClosureExampleBase.RunInThreadMessage), ModifiedClosureExampleBase.HowManyThreadsToUse);
            StateMachine.AddObserver(countdownEvents);

            // Act
            RunExampleInThread();

            // Assert
            Assert.IsTrue(countdownEvents.Wait(5000), message: "Timeouted with retrieving messages.");

            var distinctRunInThreadMessageCount = countdownEvents.GetStates()
                .Distinct()
                .Count();

            Assert.AreEqual(ModifiedClosureExampleBase.HowManyThreadsToUse, distinctRunInThreadMessageCount);
        }
    }
}