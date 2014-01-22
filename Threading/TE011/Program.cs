using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE011
{
    class Program
    {
        static void Main(string[] args)
        {
            // threadstatic/threadlocal<t>
            var codeGenerator = new CodeGenerator();

            ThreadPool.QueueUserWorkItem(_ => codeGenerator.Generate());
            ThreadPool.QueueUserWorkItem(_ => codeGenerator.Generate());
            Thread.Sleep(1000);
        }
    }

    class CodeGenerator
    {
        // NOTE: The thread static is initialized for each thread in different way
        // when initialized, then another thread will see null
        //[ThreadStatic]
        //private static Random _rnd = new Random();

        //private Random _rnd = new Random(); // NOTE: Random is not thread safe
        private ThreadLocal<Random> _rnd = new ThreadLocal<Random>(() => new Random());

        public void Generate()
        {
            Console.WriteLine("this.GetHashCode()={0}, _rnd.GetHashCode()={1}", this.GetHashCode(), _rnd.Value.GetHashCode());
        }
    }
}
