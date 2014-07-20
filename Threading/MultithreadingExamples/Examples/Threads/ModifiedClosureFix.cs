using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Threads
{
    public sealed class ModifiedClosureFix : ThreadExampleBase, ISolutionFor<ModifiedClosureExample>
    {
        /// <summary>
        /// Many developers do the same.  They point to a variable which is modified inside a loop.
        /// The trick here is to remember the value in a temp variable and point to the temp variable.
        /// Unfortunatelly: There is no guarantee that the values will be displayed in order.
        /// The thread may be sheduled in different order!
        /// </summary>
        protected override void OnRun()
        {
            for (int i = 0; i < 100; i++)
            {
                int i1 = i;
                ThreadPool.QueueUserWorkItem(state => RunInThread(i1)); 
            }

            Thread.Sleep(2000);
        }

        private void RunInThread(int i)
        {
            Log.Info("I={0}", i);
        }
    }
}