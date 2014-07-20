using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Threads
{
    public sealed class ModifiedClosureExample : ExampleBase, IImportantExample, IHasSolutionIn<ModifiedClosureFix>
    {
        /// <summary>
        /// Many developers do the same.  They point to a variable which is modified inside a loop.
        /// In our example the result will be non deterministic. 
        /// Few threads may display the same value, because they pointing to the current value of 'i' variable.
        /// </summary>
        protected override void OnRun()
        {
            for (int i = 0; i < 100; i++)
            {
                ThreadPool.QueueUserWorkItem(state => RunInThread(i)); // The i value is taken from loop
            }

            Thread.Sleep(2000);
        }

        private void RunInThread(int i)
        {
            Log.Info("I={0}", i);
        }
    }
}