using MultithreadingExamples.Examples.ThreadLocals;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.ThreadLocals
{
    [TestFixture]
    public sealed class SharedStaticRandomInstanceExampleTests : ThreadLocalsExampleTestBase<SharedStaticRandomInstanceExample>
    {
        [Test]
        public void SharedStaticInitialization_ShouldBeOneInstanceButCanBeMore()
        {
            
            // Act
            ActTest();
            var distinctIds = GetDistinctIds();

            // Assert
            Assert.AreEqual(1, distinctIds.Length);
            Assert.AreNotEqual(-1, distinctIds[0]);
        }
    }
}