using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE017
{
    class Program
    {
        private static readonly Semaphore _semaphore = new Semaphore(2,4,"MySemaphore"); // can span multiple processes

        // private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(2, 4); // NOTE: only in one process, but faster

        static void Main(string[] args)
        {

            for (int i = 0; i < 100; i++)
            {
                ThreadPool.QueueUserWorkItem(_ => Run());
            }

            Console.ReadLine();
        }

        static void Run()
        {
            try
            {
                _semaphore.WaitOne();
                Console.WriteLine("Entered threadId={0}", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(1000);
                Console.WriteLine("Exitting threadId={0}", Thread.CurrentThread.ManagedThreadId);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
