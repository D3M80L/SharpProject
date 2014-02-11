using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE008
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new AutoLock();

            ThreadPool.QueueUserWorkItem(_ => context.Run());
            ThreadPool.QueueUserWorkItem(_ => context.Run());
            ThreadPool.QueueUserWorkItem(_ => context.Run());

            Console.ReadLine();
        }
    }


    /// <summary>
    /// Note: the Synchronization works only on ContextBoundObjects
    /// </summary>
    [Synchronization]
    public class AutoLock //: ContextBoundObject // NOTE: use context bound
    {
        public void Run()
        {
            Console.Write("Running thread {0} ...", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(1000);
            Console.WriteLine("... finishing {0}.", Thread.CurrentThread.ManagedThreadId);
        }
    }
}
