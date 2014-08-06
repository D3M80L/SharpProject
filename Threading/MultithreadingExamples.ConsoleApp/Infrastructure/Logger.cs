using System;
using MultithreadingExamples.Infrastructure;

namespace MultithreadingExamples.ConsoleApp.Infrastructure
{
    internal sealed class Logger : ILog
    {
        public void Log(string message)
        {
            Console.WriteLine("{0} - {1}", DateTime.Now, message);
        }
    }
}
