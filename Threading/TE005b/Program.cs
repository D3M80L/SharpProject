using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE005b
{
    class Program
    {
        private static readonly UnsafeLock _unsafeLock = new UnsafeLock();

        static void Main(string[] args)
        {
            lock (_unsafeLock)
            {
                ThreadPool.QueueUserWorkItem(_ => _unsafeLock.Run());
                Thread.Sleep(2000); // some other stuff
            }
        }
    }

    class UnsafeLock
    {
        public void Run()
        {
            lock (this) 
            {
                Console.WriteLine("ThreadID {0} owned the lock.", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(1000); // simulate work
                Console.WriteLine("ThreadID {0} is releasing the lock.", Thread.CurrentThread.ManagedThreadId);
            }
        }
    }
}
