using System;
using MultithreadingExamples.Infrastructure;

namespace MultithreadingExamples.ConsoleApp.Infrastructure
{
    internal sealed class Interaction : IInteraction
    {
        public void Confirmation()
        {
            Console.Write("> ");
            Console.ReadLine();
        }
    }
}