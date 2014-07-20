using MultithreadingExamples.Infrastructure;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Infrastructure
{
    public abstract class ExampleTestBase<TExample>
        where TExample : ExampleBase, new()
    {
        protected TExample Example { get; private set; }

        [SetUp]
        public void TestSetup()
        {
            Example = new TExample();

            Example.Log = new Logger();
        }

        [TearDown]
        public void TearDown()
        {
            Example.Dispose();
        }
    }
}