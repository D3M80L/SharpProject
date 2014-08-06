using MultithreadingExamples.Examples.ThreadLocals;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.ThreadLocals
{
    [TestFixture]
    public sealed class ThreadLocalRandomInstanceExampleTests : ThreadLocalsExampleTestBase<ThreadLocalRandomInstanceExample>
    {
        [Test]
        public void EachThreadShouldHaveSeparateInstance()
        {
            // Act
            ActTest();
            var distinctIds = GetDistinctIds();

            // Assert
            Assert.AreEqual(HowManyThreadsToUse, distinctIds.Length);
        }
    }
}