using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE012b
{
    /// <summary>
    /// Note, this is a theoretical implementation.
    /// It is hard to simulate the behaviour.
    /// In the example I am creating two threads. Please note, that creating a thread
    /// takes few resources. During the creation - there may be enough time to
    /// refresh the data and 'synchronize' all values.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var example = new RawFields();
            //var example = new VolatileFields();

            for (int i = 0; i < 100; i++)
            {
                ThreadPool.QueueUserWorkItem(_ => example.Read());
            }
            
            example.Set();

            Thread.Sleep(250);
        }
    }

    class RawFields
    {
        private bool flag = false;
        private long result = 0;
        public void Set()
        {
            result = 8;
            flag = true; // may be optimized and reordered
        }

        public void Read()
        {
            if (flag)
            {
                long y = result + 1;
                Console.Write(y); // NOTE: the value may be 1
            }
        }
    }

    /// <summary>
    /// Volatile keyword is not CLS compilant and available in C# and not in VB, what is more
    /// having a volatile field like
    /// private volatile int sum = 5;
    /// there is no guarantee, that the total sum
    /// sum = sum + sum;
    /// will be 10. Volatile fields does not allow bit shifting and few other operations.
    /// The volatile keyword gives no value on Intel x86 and x64 processors, where a fence is 
    /// used on read and writes.
    /// </summary>
    class VolatileFields
    {
        private /*volatile*/ int flag = 0;
        private int value = 0;

        public void Set()
        {
            value = 8;
            Volatile.Write(ref flag, 1); // other sets above
        }

        public void Read()
        {
            if (Volatile.Read(ref flag) == 1)
            {
                Console.WriteLine(value);
            }
            else
            {
                Console.WriteLine("?");
            }
        }
    }
}
