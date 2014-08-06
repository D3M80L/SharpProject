using System;
using System.Collections.Generic;
using MultithreadingExamples.Infrastructure;

namespace MultithreadingExamples.Tests.Infrastructure
{
    internal sealed class Logger : ILog
    {
        private readonly List<string> _messages = new List<string>();

        public void Log(string message)
        {
            Console.WriteLine(message);
            _messages.Add(message);
        }
    }
}
