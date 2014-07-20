using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadingExamples.Infrastructure
{
    public interface IExample : IDisposable
    {
        void Run();
    }
}
