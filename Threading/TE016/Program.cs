using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE016
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MAIN ThreadId={0}", Thread.CurrentThread.ManagedThreadId);

            //Example1();
            //Example2();
            Example3();

            Thread.Sleep(1000);
        }

        static void Example1()
        {
            new Task(Run, 8).Start();

            Task.Run(() => Run(9));
        }

        static void Example2()
        {
            var task = new Task(Run, 8);

            task.Wait(); // probably will block
        }

        static void Example3()
        {
            var task = Task.Run(() =>
            {
                Console.WriteLine("Let it throw, let it throw, let it throw...");
                throw null;
            });

            try
            {
                task.Wait();
            }
            catch (AggregateException ae)
            {
                Console.WriteLine("Aggregate exception");
            }
        }

        static void Run(object i)
        {
            Console.WriteLine("{0}:IsThreadPool={1} Value={2}", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread, i);
        }
    }
}
