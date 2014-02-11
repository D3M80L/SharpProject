using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE004c
{
    class Program
    {
        static void Main(string[] args)
        {
            InvalidCall();
            //ValidCall();
        }

        /// <summary>
        /// Many developers do the same.  They point to a variable which is modified inside a loop.
        /// In our example the result will be non deterministic. 
        /// Few threads may display the same value, because they pointing to the current value of 'i' variable.
        /// </summary>
        private static void InvalidCall()
        {
            for (int i = 0; i < 100; i++)
            {
                ThreadPool.QueueUserWorkItem(_ => Run(i));
            }
        }

        /// <summary>
        /// Many developers do the same.  They point to a variable which is modified inside a loop.
        /// The trick here is to remember the value in a temp variable and point to the temp variable.
        /// Unfortunatelly: There is no guarantee that the values will be displayed in order.
        /// The thread may be sheduled in different order!
        /// </summary>
        private static void ValidCall()
        {
            for (int i = 0; i < 100; i++)
            {
                var temp = i;
                ThreadPool.QueueUserWorkItem(_ => Run(temp));
            }
        }

        static void Run(object o)
        {
            Console.WriteLine(o);
        }
    }
}
