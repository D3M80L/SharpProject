using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE015
{
    class Program
    {
        static void Main(string[] args)
        {
            CallContext.LogicalSetData("Nick", "D3M80L");
            
            ThreadPool.QueueUserWorkItem(_ => Run());
            new Thread(Run).Start();
            
            Thread.Sleep(1000);
            
            ExecutionContext.SuppressFlow();
            ThreadPool.QueueUserWorkItem(_ => Run());
            new Thread(Run).Start();

            Thread.Sleep(1000);
        }

        static void Run()
        {
            Console.WriteLine("ID:{1} {2}. Your Nick is {0}.", CallContext.LogicalGetData("Nick"), Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
        }
    }
}
