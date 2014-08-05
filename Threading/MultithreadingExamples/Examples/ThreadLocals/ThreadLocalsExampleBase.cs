using System;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.ThreadLocals
{
    public abstract class ThreadLocalsExampleBase : ExampleBase
    {
        public int RandomInstanceHashCode
        {
            get
            {
                var random = BuildRandom();
                if (random != null)
                {
                    return random.GetHashCode();
                }

                return -1;
            }
        }

        protected override void OnRun()
        {
            Log.Info(RandomInstanceHashCode.ToString());
        }

        protected abstract Random BuildRandom();
    }
}
