using System;
using MultithreadingExamples.Infrastructure;

namespace MultithreadingExamples.Examples.ThreadLocals
{
    /// <summary>
    /// Please note, that this implementation is not thread save.
    /// The random class has some private fields which are updated after each call of Next()
    /// 
    /// Solution: An easy solution would be to wrap the call of Next() in a lock. 
    /// Locking has influence on performance.
    /// </summary>
    public sealed class SharedStaticRandomInstanceExample : ThreadLocalsExampleBase, IImportantExample,IHasSolutionIn<ThreadLocalRandomInstanceExample>
    {
        private static Random _randomInstance = new Random(); // RANDOM IS NOT THREAD SAFE!

        protected override Random BuildRandom()
        {
            return _randomInstance;
        }
    }
}