using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE014
{
    class Program
    {
        static void Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Token.Register(() => Console.WriteLine("Cancelled."));

            ThreadPool.QueueUserWorkItem(_ => Run(cancellationTokenSource));
            Console.WriteLine("Enter to cancel.");
            Console.ReadLine();
            cancellationTokenSource.Cancel();
            // NOTE: here wait for the threads to finish
            Console.WriteLine("Enter to exit.");
            Console.ReadLine();
            Console.WriteLine("Exitting");
        }

        static void Run(CancellationTokenSource token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("CANCELLED");
                    return;
                }
                Console.Write(".");
                Thread.Sleep(250);
            }
        }
    }
}
