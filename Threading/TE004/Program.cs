using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE004
{
    /// <summary>
    /// Note: it's hard to simulate the behaviour for few threads. 
    /// Creating a fresh thread takes some time - as a result during the time slice, where the thread is created, all variables may be synchronized.
    /// The problem may appear on already running threads - this is why we are creating many threads.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //var calc = new ThreadUnsafe();

            var calc = new ThreadSafe();

            for (int i = 0; i < 100; i++)
            {
                ThreadPool.QueueUserWorkItem(_ => calc.Run());
            }
            
            Thread.Sleep(2000);

            Console.WriteLine(calc.I + " " + calc.J);
        }
    }

    class ThreadUnsafe 
    {
        public long I = 0;
        public long J = 0;

        public void Run()
        {

            ++I; // it is possible that two threads at the same modify the value

            // note: Torn read - in more complex calculations

            --J;
        }
    }

    class ThreadSafe
    {
        private object _padlock = new object();
        public long I = 0;
        public long J = 0;

        public void Run()
        {
            // NOTE: decompile the app, to see the lock implementation
            lock (_padlock)
            {
                ++I;
                --J;
            }
        }
    }
}
