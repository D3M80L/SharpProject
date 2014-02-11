using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE014
{
    /// <summary>
    /// How to elegantly finish a thread/task.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Create a token source
            var cancellationTokenSource = new CancellationTokenSource();

            // NOTE: this only shows how to register a piece of code which should be executed, then a token has been cancelled
            cancellationTokenSource.Token.Register(() => Console.WriteLine("Cancelled."));

            // 2. Run the thread and pass the token
            ThreadPool.QueueUserWorkItem(_ => Run(cancellationTokenSource.Token));

            Console.WriteLine("Enter to cancel.");
            Console.ReadLine();

            // 3. Call cancel on the token source
            cancellationTokenSource.Cancel();

            // Here we should wait for the threads to finish. For clarity I have skipped the code.

            Console.WriteLine("Enter to exit.");
            Console.ReadLine();
            Console.WriteLine("Exitting");
        }

        /// <summary>
        /// In this method perform some operations in thread.
        /// </summary>
        /// <param name="token"></param>
        static void Run(CancellationToken token)
        {
            while (true)
            {
                // In places where it is possible, just check the cancellation status.
                // When the token has been cancelled, then just return from the method. 
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("CANCELLED");
                    return;
                }

                // Do some time computing tasks...
                Console.Write(".");
                Thread.Sleep(250);
            }
        }
    }
}
