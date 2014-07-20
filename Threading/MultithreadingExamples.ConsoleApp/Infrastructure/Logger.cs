﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
