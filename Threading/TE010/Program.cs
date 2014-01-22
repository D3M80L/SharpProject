using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE010
{
    class Program
    {
        static void Main(string[] args)
        {

            ThreadPool.QueueUserWorkItem(_ => Run());
            ThreadPool.QueueUserWorkItem(_ => Run());
            ThreadPool.QueueUserWorkItem(_ => Run());

            Console.ReadLine();
        }

        static void Run()
        {
            Console.WriteLine("Thread={0}, Singleton hash code={1}, Singleton Created={2}", Thread.CurrentThread.ManagedThreadId, Singleton.Instance.GetHashCode(), Singleton.Instance.Created);

        }
    }

    public class Singleton
    {
        private static Lazy<Singleton> _singleInstance = new Lazy<Singleton>(() => new Singleton(), true);

        public static Singleton Instance
        {
            get
            {
                return _singleInstance.Value;
            }
        }

        private Singleton()
        {
            Created = DateTime.Now;
            Thread.Sleep(2000); // simulate some heavy build
        }

        public DateTime Created { get; private set; }
    }
}
