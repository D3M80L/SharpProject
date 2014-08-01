using System;
using System.Threading;
using MultithreadingExamples.Infrastructure;

namespace MultithreadingExamples.Examples.ThreadLocals
{
    /// <summary>
    /// Each thread will have its own instance of thread. 
    /// This is wrapped in the ThreadLocal class.
    /// 
    /// NOTE: please take a look, at the seed which is passed as an argument in the Random class.
    /// This is because two threads, when would create an instance of Random during a very short period of time, then they could 
    /// generate same values. That's because the Random by default uses time as the seed.
    /// </summary>
    public sealed class ThreadLocalRandomInstanceExample : ThreadLocalsExampleBase, ISolutionFor<ThreadStaticRandomInstanceExample>, ISolutionFor<SharedStaticRandomInstanceExample>, ISolutionFor<ThreadLocalRandomInstanceExample>
    {
        private ThreadLocal<Random> _randomInstance = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));

        protected override Random BuildRandom()
        {
            return _randomInstance.Value;
        }
    }
}