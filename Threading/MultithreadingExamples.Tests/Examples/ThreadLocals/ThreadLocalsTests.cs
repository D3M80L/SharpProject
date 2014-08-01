using System;
using System.Linq;
using System.Threading;
using MultithreadingExamples.Examples.ThreadLocals;
using NUnit.Framework;

namespace MultithreadingExamples.Tests.Examples.ThreadLocals
{
    [TestFixture]
    public sealed class ThreadLocalsTests
    {
        public const int HowManyThreadsToUse = 5;

        [Ignore]
        [Test]
        public void SharedStaticRandomInstanceExampleTests()
        {
            TestExample<SharedStaticRandomInstanceExample>();
        }

        [Ignore]
        [Test]
        public void ThreadStaticRandomInstanceExampleTests()
        {
            TestExample<ThreadStaticRandomInstanceExample>();
        }

        [Test]
        public void ThreadLocalRandomInstanceExampleTests()
        {
            TestExample<ThreadLocalRandomInstanceExample>();
        }

        private void TestExample<TExample>()
            where TExample : ThreadLocalsExampleBase, new()
        {
            // Arrange
            var exampleKernel = new ExampleKernel<TExample>(HowManyThreadsToUse);
            exampleKernel.Arrange();

            // Act
            exampleKernel.Act();

            var distinctIds = exampleKernel.Results
                .Select(s => s)
                .Distinct()
                .ToArray();

            var countDistinctIds = distinctIds
                .Count();


            // Assert
            Assert.AreEqual(exampleKernel.Results.Length, countDistinctIds, String.Join(",", distinctIds));
        }

        public sealed class ExampleKernel<TExample>
        where TExample : ThreadLocalsExampleBase, new()
        {
            private int[] _threadResults;
            private Thread[] _threads;
            private TExample _example;

            public int[] Results
            {
                get { return _threadResults; }
            }

            public ExampleKernel(int n)
            {
                _example = new TExample();
                _threads = new Thread[n];
                _threadResults = new int[n];
            }

            public void Arrange()
            {
                for (int i = 0; i < _threads.Length; ++i)
                {
                    var ti = i;
                    _threads[i] = new Thread(() => RunExamleIn(ti));
                }
            }

            public void Act()
            {
                _example.Run();

                for (int i = 0; i < _threads.Length; ++i)
                {
                    _threads[i].Start();
                }

                for (int i = 0; i < _threads.Length; ++i)
                {
                    _threads[i].Join();
                }
            }

            private void RunExamleIn(int i)
            {
                _threadResults[i] = _example.Id;
            }
        }
    }
}