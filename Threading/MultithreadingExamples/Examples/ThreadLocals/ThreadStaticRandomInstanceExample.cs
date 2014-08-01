using System;
using MultithreadingExamples.Infrastructure;

namespace MultithreadingExamples.Examples.ThreadLocals
{
    /// <summary>
    /// You can mark your static field with the attibute ThreadStatic.
    /// Each thread stores its own value and instance in this field.
    /// 
    /// IMPORTANT: Please note, that the example below only assigns the field with the new instance ONLY in the main thread.
    /// Each thread needs to call and create the instance separately.
    /// </summary>
    public sealed class ThreadStaticRandomInstanceExample : ThreadLocalsExampleBase, IImportantExample, IHasSolutionIn<ThreadLocalRandomInstanceExample>
    {
        [ThreadStatic]
        private static Random _randomInstance = new Random(); // THE INSTANCE IS VISIBLE ONLY IN FIRST THREAD!

        protected override Random BuildRandom()
        {
            return _randomInstance;
        }
    }
}