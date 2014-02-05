using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE019
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting ID:{0}",Thread.CurrentThread.ManagedThreadId);
                Run(); // note that we are running a method
                //await RunAsync(); // only on async methods possible
                //ExceptionHanlde();
                Console.WriteLine("Doing something else. ID:{0}", Thread.CurrentThread.ManagedThreadId);

                Console.ReadLine();
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Ough..."); // never caught
            }
        }


        /// <summary>
        /// Take a look, that this is an async 'void' method.
        /// Such methods may be used in event handlers, but should be omitted.
        /// </summary>
        static async void Run() 
        {
            Console.WriteLine("Running ID:{0}", Thread.CurrentThread.ManagedThreadId);

            await Task.Delay(1000);
            Console.WriteLine("Throwing ID:{0}", Thread.CurrentThread.ManagedThreadId); // note the thread ID
            
            // We are throwing an exception on a different thread. But it is sent to the main thread through
            // a synchronization context. As a result it crashes the whole app
            throw null;
        }

        static async Task RunAsync()
        {
            Console.WriteLine("Running ID:{0}", Thread.CurrentThread.ManagedThreadId);

            await Task.Delay(1000);
            Console.WriteLine("Throwing ID:{0}", Thread.CurrentThread.ManagedThreadId);
            throw null;
        }


        static async Task ExceptionHanlde()
        {
            try
            {
                await RunAsync();
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Caught");
            }
        }
    }
}
