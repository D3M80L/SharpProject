using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE005
{
    /// <summary>
    /// Example usage of wait-pulse: produced/consumer queue
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(_ => new Locker().Pulsar()); // NOTE: when pulsed, and no one is waiting, then the pulse is swallowed
            
            ThreadPool.QueueUserWorkItem(_ => new Locker().Wait("A"));
            ThreadPool.QueueUserWorkItem(_ => new Locker().Wait("B"));
            ThreadPool.QueueUserWorkItem(_ => new Locker().Wait("C"));
            ThreadPool.QueueUserWorkItem(_ => new Locker().Wait("D"));

            // NOTE: move the pulsar here

            Thread.Sleep(3000);
            Console.WriteLine("Exit");
        }
    }

    class Locker
    {
        private static object _padLock = new object();

        public void Pulsar()
        {
            Console.WriteLine("# {0} - {1}", Thread.CurrentThread.ManagedThreadId, "Start");

            lock (_padLock)
            {
                Console.WriteLine("# {0} - {1}", Thread.CurrentThread.ManagedThreadId, "Pulsing");

                Monitor.PulseAll(_padLock); // NOTE: Pulse will pulse only one waiting thread
                //Monitor.Pulse(_padLock); // NOTE: Pulse will pulse all thread waiting at the moment

                Console.WriteLine("# {0} - {1}", Thread.CurrentThread.ManagedThreadId, "Exiting");
            }
        }

        public void Wait(object name)
        {
            Console.WriteLine("{2} {0} - {1}", Thread.CurrentThread.ManagedThreadId, "Start", name);

            lock (_padLock)
            {
                Console.WriteLine("{2} {0} - {1}", Thread.CurrentThread.ManagedThreadId, "Entered", name);

                Monitor.Wait(_padLock);

                Console.WriteLine("B {0} - {1}", Thread.CurrentThread.ManagedThreadId, "Pulsed");
            }
        }
    }
}
