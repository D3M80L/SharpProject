using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE013
{
    class Program
    {
        static readonly Barrier _barrier = new Barrier(4, PostAction);

        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(_ => Wait());
            ThreadPool.QueueUserWorkItem(_ => Wait());
            ThreadPool.QueueUserWorkItem(_ => Wait());
            ThreadPool.QueueUserWorkItem(_ => Wait());

            Thread.Sleep(5000);
        }

        static void PostAction(Barrier barrier)
        {
            Console.WriteLine();
        }

        static void Wait()
        {
            for (int i = 0; i < 4; i++)
            {
                Console.Write("[T{0}]{1} ", Thread.CurrentThread.ManagedThreadId, i);
                _barrier.SignalAndWait();
            }
        }
    }
}
