using System;
using System.Threading;

namespace MultithreadingExamples.Examples.Patterns
{
    public sealed class Singleton
    {
        private static readonly Lazy<Singleton> LazyInstance = new Lazy<Singleton>(()=> new Singleton(), true);

        public static Singleton Instance
        {
            get { return LazyInstance.Value; }
        }

        private static int _counter;
        private Singleton()
        {
            Interlocked.Increment(ref _counter);

            Thread.Sleep(2000); // simulate some heavy build
        }

        public int Counter
        {
            get { return _counter; }
        }

        public void DoSomething() { }
    }
}