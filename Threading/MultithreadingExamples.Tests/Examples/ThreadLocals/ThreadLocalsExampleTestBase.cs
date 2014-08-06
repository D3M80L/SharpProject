using System.Linq;
using System.Threading;
using MultithreadingExamples.Examples.ThreadLocals;
using MultithreadingExamples.Tests.Infrastructure;

namespace MultithreadingExamples.Tests.Examples.ThreadLocals
{
    public abstract class ThreadLocalsExampleTestBase<TThreadLocalsExample> : ExampleTestBase<TThreadLocalsExample>
        where TThreadLocalsExample : ThreadLocalsExampleBase, new()
    {
        private int[] _randomInstanceHashCodes;
        private Thread[] _threads;

        protected const int HowManyThreadsToUse = 5;

        protected override void OnTestSetup()
        {
            _threads                 = new Thread[HowManyThreadsToUse];
            _randomInstanceHashCodes = new int[HowManyThreadsToUse];

            for (int i = 0; i < HowManyThreadsToUse; ++i)
            {
                var ti = i;
                _threads[i] = new Thread(() => RunExamleIn(ti));
            }
        }

        protected void ActTest()
        {
            RunExampleInThread();

            for (int i = 0; i < HowManyThreadsToUse; ++i)
            {
                _threads[i].Start();
            }

            for (int i = 0; i < HowManyThreadsToUse; ++i)
            {
                _threads[i].Join();
            }
        }

        protected int[] GetDistinctIds()
        {
            var distinctIds = _randomInstanceHashCodes
                .Select(s => s)
                .Distinct()
                .ToArray();

            return distinctIds;
        }

        private void RunExamleIn(int i)
        {
            _randomInstanceHashCodes[i] = Example.RandomInstanceHashCode;
        }
    }
}