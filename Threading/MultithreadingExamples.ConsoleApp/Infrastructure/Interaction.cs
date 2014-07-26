using System;
using MultithreadingExamples.Infrastructure;

namespace MultithreadingExamples.ConsoleApp.Infrastructure
{
    internal sealed class Interaction : IInteraction
    {
        public void ConfirmationRequest()
        {
            Console.Write("> ");
            Console.ReadLine();
        }
    }
}