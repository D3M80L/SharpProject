using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TE002
{
    /// <summary>
    /// Demonstrates:
    /// - exception handling in threads
    /// </summary>
    
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var thread =  new Thread(Run);
                thread.IsBackground = true;
                thread.Start();
            }
            catch
            {
                Console.WriteLine("Some excpetion occured.");
            }

            Thread.Sleep(2000);
            Console.WriteLine("###### Finishing main app.");
        }

        static void Run()
        {
            try
            {
                throw null;
            }
            catch
            {
                Console.WriteLine("Exception thrown in thread.");
                throw; // re-thow for demo will crash the whole app
                
                // There are few exceptions4exceptions - see: http://msdn.microsoft.com/en-us/library/ms228965.aspx
                // for explanation see: http://msdn.microsoft.com/en-us/library/system.appdomain.unhandledexception.aspx
                // MSDN: In the .NET Framework versions 1.0 and 1.1, an unhandled exception that occurs in a thread other than the main application thread 
                // is caught by the runtime and therefore does not cause the application to terminate. 
                // Thus, it is possible for the UnhandledException event to be raised without the application terminating. \
                // Starting with the .NET Framework version 2.0, this backstop for unhandled exceptions in child threads was removed, 
                // because the cumulative effect of such silent failures included performance degradation, corrupted data, and lockups, all of which were difficult to debug. 
            }
        }
    }
}
