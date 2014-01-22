using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE006
{
    /// <summary>
    /// NOTE: Run two separate instances of this applicaion.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Acquiring a mutext.");
            using (var mutex = new Mutex(true, "MyMut3x"))
            {
                Console.WriteLine("Running... Press enter to finish.");
                Console.ReadLine();
            }
        }
    }
}
