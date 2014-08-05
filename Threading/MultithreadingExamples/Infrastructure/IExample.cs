using System;

namespace MultithreadingExamples.Infrastructure
{
    public interface IExample : IDisposable
    {
        void Run();
        ILog Log { get; set; }

        IInteraction Interaction { get; set; }
    }
}
