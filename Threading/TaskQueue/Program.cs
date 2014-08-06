using System;
using System.Threading;

namespace TaskQueue
{
    class Program
    {
        private static readonly UnsafeCoordinator _coordinator = new UnsafeCoordinator();
        //private static readonly SafeCoordinator _coordinator = new SafeCoordinator();

        static void Main(string[] args)
        {
            #region Unsafe
            for (int i = 0; i < 100; i++)
            {
                _coordinator.Enqueue();
                ThreadPool.QueueUserWorkItem(_ => Run());
            }

            Thread.Sleep(1000);
            Console.WriteLine("FIN"); 
            #endregion


            #region Safe
            //for (int i = 0; i < 100; i++)
            //{
            //    _coordinator.Enqueue();
            //    ThreadPool.QueueUserWorkItem(_ => Run());
            //}

            //_coordinator.EndQueing(); // This line is important

            //Thread.Sleep(1000);
            //Console.WriteLine("FIN");  
            #endregion
        }

        static void Run()
        {
            try
            {
                // DO SOME STUFF HERE
            }
            finally
            {
                _coordinator.Dequeue();
            }
        }
    }

    public class UnsafeCoordinator
    {
        private int _counter;

        public void Enqueue()
        {
            Interlocked.Increment(ref _counter);
        }

        public void Dequeue()
        {
            if (Interlocked.Decrement(ref _counter) == 0)
            {
                Console.WriteLine("Finished");
            }
        }
    }

    public class SafeCoordinator
    {
        /// <summary>
        /// Set the counter to initial value = 1.
        /// Otherwise, like in the UnsafeCoordinator, the _counter ma be queal to 0 in the Dequeue before another is being queue.
        /// </summary>
        private int _counter = 1;

        public void Enqueue()
        {
            if (Interlocked.Increment(ref _counter) == 1)
            {
                throw new InvalidOperationException("The enqueue action is not allowed in this state.");
            }
        }

        public void Dequeue()
        {
            if (Interlocked.Decrement(ref _counter) == 0)
            {
                Console.WriteLine("Finished");
            }
        }

        public void EndQueing()
        {
            Dequeue();
        }
    }
}
