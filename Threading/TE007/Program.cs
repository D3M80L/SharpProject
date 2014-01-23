using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE007
{
    class Program
    {
        private static EventWaitHandle _waitHandle;
        static void Main(string[] args)
        {
            //_waitHandle = new AutoResetEvent(false); // NOTE: false/true
            _waitHandle = new ManualResetEvent(false); // NOTE: var manualResetEventSlim = new ManualResetEventSlim();

            ThreadPool.QueueUserWorkItem(_ => Run("A"));
            ThreadPool.QueueUserWorkItem(_ => Run("B"));
            ThreadPool.QueueUserWorkItem(_ => Run("C"));

            Thread.Sleep(1000);
            Console.WriteLine("Press ENTER to Set.");
            Console.ReadLine();
            _waitHandle.Set();

            // auto reset event/ manual reset (slim)/cross process wait handle
            Thread.Sleep(1000);
        }

        static void Run(object data)
        {
            Console.WriteLine("{0}:Enered ThreadID={1}", data, Thread.CurrentThread.ManagedThreadId);
            _waitHandle.WaitOne();
            Console.WriteLine("{0}:Exiting ThreadID={1}", data, Thread.CurrentThread.ManagedThreadId);
        }
    }
}
