using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TE012
{
    class Program
    {
        static void Main(string[] args)
        {
            var example = new RawFieldsExample();
            //var example = new MemoryBarrierFieldsExample();

            ThreadPool.QueueUserWorkItem(_ => example.Run());
            Thread.Sleep(500);
            example.Set();
            Console.WriteLine("Waiting for detonation...");
            Console.ReadLine();
        }
    }


    /// <summary>
    /// This example shows how an optimization (IL, JIT) can work.
    /// </summary>
    class RawFieldsExample // NOTE: compilation for x86 and x64 may be different
    {
        private /*volatile*/ bool flag = false;

        public void Set()
        {
            flag = true;
            Console.WriteLine("The bomb has been planted...");
        }

        /// <summary>
        /// Note the the message Boom will not be displayed.
        /// You need to compile the program with optimizations enabled.
        /// </summary>
        public void Run()
        {
            int sum = 0;
            while (!flag) ++sum; 
            Console.WriteLine("Boom {0}.", sum);
        }
    }

    class MemoryBarrierFieldsExample
    {
        private bool flag = false;

        public void Set()
        {
            Thread.MemoryBarrier();
            flag = true;
            Thread.MemoryBarrier();
        }

        public void Run()
        {
            int sum = 0;
            Thread.MemoryBarrier();
            while (!flag)
            {
                Thread.MemoryBarrier();
                ++sum;
            }

            Console.WriteLine(sum);
        }
    }
}
