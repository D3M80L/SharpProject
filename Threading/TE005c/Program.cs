using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE005c
{
    class Program
    {
        private static int _lock = 0;

        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(_ => Run());
            ThreadPool.QueueUserWorkItem(_ => Run());
            ThreadPool.QueueUserWorkItem(_ => Run());
            ThreadPool.QueueUserWorkItem(_ => Run());

            Thread.Sleep(5000);
        }

        static void Run()
        {
            try
            {
                Monitor.Enter(_lock);

                Console.WriteLine("ThreadID {0} owned the lock.", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(1000); // simulate work
                Console.WriteLine("ThreadID {0} is releasing the lock.", Thread.CurrentThread.ManagedThreadId);

                Monitor.Exit(_lock);
            }
            catch
            {
                Console.WriteLine("Ough! Something went wrong.");
            }
        }
    }
}
