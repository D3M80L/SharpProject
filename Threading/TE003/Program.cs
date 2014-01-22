using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE003
{
    /// <summary>
    /// Demonstrates:
    /// - thread pool
    /// - async delegates
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(Run);
            Thread.Sleep(1000);

            // asynchronous delegates
            Func<int, int> calculator = Calculate;
            calculator.BeginInvoke(10, CalculationCallback, calculator); // NOTE: the callback
            Console.WriteLine("Main thread is doing some stuff up here...");
            Thread.Sleep(3000);
            Console.WriteLine("Finishing");
        }

        static void Run(object state)
        {
            Console.WriteLine("Running in a thread from pool? = {0}", Thread.CurrentThread.IsThreadPoolThread);
            //throw null; // NOTE: uncomment this to see, that the app crashes when an exception is thrown from a pooled thread
        }


        static int Calculate(int x)
        {
            Console.WriteLine("Calculating in a pool? = {0}", Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(1000); // just simulate some heavy calculations
            //throw null; // NOTE: throw exception, but the method was thrown from a pooled thread
            return ++x;
        }

        static void CalculationCallback(IAsyncResult asyncResult)
        {
            var target = (Func<int, int>)asyncResult.AsyncState;
            try
            {
                int result = target.EndInvoke(asyncResult); // NOTE: the exception will be pushed from the pooled thread
                Console.WriteLine("Finished calculation with value {0}. Is from pool? {1}", result, Thread.CurrentThread.IsThreadPoolThread);
            }
            catch(NullReferenceException nullReferenceException)
            {
                Console.WriteLine("Exception occured.");
            }
        }
    }
}
