using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE009
{
    /// <summary>
    /// How Abort and Interrupt works
    /// NOTE: pay attention on aborts! 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var thread = new Thread(Run);
            thread.Start();

            thread.Interrupt();
            Thread.Sleep(1000);
            thread.Abort();

            Thread.Sleep(1000);
        }

        static void Run()
        {
            try
            {
                Thread.Sleep(10000);
            }
            catch (ThreadInterruptedException interrupted)
            {
                Console.WriteLine("Interrupted exception.");
            }

            try
            {
                try
                {
                    Thread.Sleep(10000); // simulate work
                }
                catch (ThreadAbortException abortedException)
                {
                    Console.WriteLine("Aborted exception 1");
                }

                Console.WriteLine("ThreadAbort has been cancelled.");
            }
            catch (Exception ex)
            {
                // NOTE: exception ThreadAbortException has ben caught
                Console.WriteLine("Exception {0}", ex.GetType());
            }
        }


        static void AbortUnsafe()
        {
            // NOTE: take a look at the decompiled code of this block (opaque code)
            using (var stream = File.CreateText("some path"))
            {

            }
        }
    }
}
