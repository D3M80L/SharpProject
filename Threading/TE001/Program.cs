using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE001
{
    /// <summary>
    /// Demonstrates:
    /// - how background and foreground thread works.
    /// - how the finally block works in a background and foreground thread
    /// 
    /// Some discussion about threads.
    /// Why Windows Threads are better than POSIX threads: http://software.intel.com/en-us/blogs/2006/10/19/why-windows-threads-are-better-than-posix-threads
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var thread = new Thread(FromBackgroundThread);
                //thread.IsBackground = true; // NOTE - uncomment to see the differences.
                thread.Start();
                //Environment.FailFast("Just because"); // NOTE: simulate process termination
                Console.WriteLine("Finishing...");
            }
            finally
            {
                Console.WriteLine("Cleaning main thread."); // NOTE: this block is not fired, when the thread is terminated
            }
        }

        static void FromBackgroundThread()
        {
            try
            {
                Console.WriteLine("Working in background thread...");
                Thread.Sleep(2000); // some work happens here
            }
            finally
            {
                Console.WriteLine("Cleaning background thread."); // NOTE: the finally block is not fired when the thread is a background thread
            }
        }
    }
}
