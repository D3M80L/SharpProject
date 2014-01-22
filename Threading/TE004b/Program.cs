using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TE004b
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(_ => Run());
            MyClass.I = 2;

            Console.WriteLine("Finishing");
        }

        static void Run()
        {
            Console.WriteLine("Accessing static property");

            MyClass.I = 1;
        }
    }

    public class MyClass
    {
        public static int I;
        static MyClass()
        {
            Console.WriteLine("Static constructor ThreadId={0}, is from threadpool={1}", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(2000); // emulate some hard work
        }

    }
}
