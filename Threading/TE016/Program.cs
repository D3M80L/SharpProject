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

        /// <summary>
        /// This method shows how create and run a task.
        /// </summary>
        static void Example1()
        {
            var task = new Task(ComputeRocketTrajectory, 8);
            // do here something else
            task.Start();

            // Immediately Start the task
            Task.Run(() => ComputeRocketTrajectory(9));
        }


        /// <summary>
        /// When you do not Start the task, and then Wait for the result, then the task may block
        /// </summary>
        static void Example2()
        {
            var task = new Task(ComputeRocketTrajectory, 8);

            task.Wait(); // probably will block
        }

        /// <summary>
        /// Unhandled excpetion in tasks are handled
        /// </summary>
        static void Example3()
        {
            var task = Task.Run(() =>
            {
                Console.WriteLine("Let it throw, let it throw, let it throw...");
                // here we are throwing an exception
                throw null;
            });

            // we can perform some operations here

            try
            {
                // the exception in the Task is rethrown and packed to an AggregateException
                task.Wait();
            }
            catch (AggregateException)
            {
                Console.WriteLine("Aggregate exception");
            }
        }

        static void ComputeRocketTrajectory(object i)
        {
            // note that mostly, a thread from thread pool is used
            Console.WriteLine("{0}:IsThreadPool={1} Value={2}", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread, i);
        }
    }
}
