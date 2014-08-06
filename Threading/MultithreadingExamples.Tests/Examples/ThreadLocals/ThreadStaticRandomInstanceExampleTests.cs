using MultithreadingExamples.Examples.ThreadLocals;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.ThreadLocals
{
    [TestFixture]
    public sealed class ThreadStaticRandomInstanceExampleTests : ThreadLocalsExampleTestBase<ThreadStaticRandomInstanceExample>
    {
        [Test]
        public void ThreadStaticDecoration_AllOtherThreadsShouldReturnMinusOne()
        {

            // Act
            ActTest();
            var distinctIds = GetDistinctIds();

            // Assert
            Assert.AreEqual(1, distinctIds.Length);
            Assert.AreEqual(-1, distinctIds[0]);
        }
    }
}