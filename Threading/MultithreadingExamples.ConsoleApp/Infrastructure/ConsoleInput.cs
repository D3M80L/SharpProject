using System;
using MultithreadingExamples.Infrastructure;

namespace MultithreadingExamples.ConsoleApp.Infrastructure
{
    internal sealed class ConsoleInput : IConsoleInput
    {
        public string ReadLine()
        {
            Console.Write("> ");
            return Console.ReadLine();
        }
    }
}