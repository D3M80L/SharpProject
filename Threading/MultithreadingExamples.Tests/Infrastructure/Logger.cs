using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
