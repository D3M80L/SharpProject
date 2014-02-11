using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TE018
{
    /// <summary>
    /// This is only for demo. Take a look at decompiled code (with show compiler-generated code option enabled).
    /// IAsyncStateMachine - http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.iasyncstatemachine(v=vs.110).aspx
    /// 
    /// For details about async/await:
    /// - http://msdn.microsoft.com/en-us/library/hh156513.aspx
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetText(@"D:\SomeFile.txt"));

            RunAsync();
        }

        static void SomeOtherMethod()
        {


        }

        private static async Task<string> GetText(string fileName)
        {
            using (var stream = File.OpenText(fileName))
            {
                Console.WriteLine("Before AWAIT");
                var result = await stream.ReadToEndAsync();

                Console.WriteLine("After AWAIT");

                return result;
            }
        }

        private static async void RunAsync()
        {
            Console.WriteLine("RunAsync BEFORE");

            await Task.Delay(1000);

            Console.WriteLine("RunAsync AFTER");

        }
    }
}
