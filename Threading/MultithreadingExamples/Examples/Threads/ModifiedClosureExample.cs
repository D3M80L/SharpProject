using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Threads
{
    public sealed class ModifiedClosureExample : ModifiedClosureExampleBase, IImportantExample, IHasSolutionIn<ModifiedClosureFixExample>
    {
        /// <summary>
        /// Many developers do the same.  They point to a variable which is modified inside a loop.
        /// In our example the result will be non deterministic. 
        /// Few threads may display the same value, because they pointing to the current value of 'i' variable.
        /// </summary>
        protected override void OnRun()
        {
            for (int i = 0; i < HowManyThreadsToUse; i++)
            {
                ThreadPool.QueueUserWorkItem(state => RunInThread(i)); // The i value is taken from loop
            }
        }
    }
}