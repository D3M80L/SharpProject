using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Patterns
{
    public sealed class DoubleCheckedLazyExample : ExampleBase
    {
        protected override void OnRun()
        {
            var singletonInstance = Singleton.Instance;

            Parallel.For(0, 10, _ => Singleton.Instance.DoSomething());

            Log.Info("COUNTER={0}", singletonInstance.Counter);
        }
    }
}
