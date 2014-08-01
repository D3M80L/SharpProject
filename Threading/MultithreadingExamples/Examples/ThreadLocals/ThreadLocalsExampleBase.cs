using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.ThreadLocals
{
    public abstract class ThreadLocalsExampleBase : ExampleBase
    {
        public int Id
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
            Log.Info(Id.ToString());
        }

        protected abstract Random BuildRandom();
    }
}
