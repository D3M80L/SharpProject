using System;
using System.Collections.Generic;
using MultithreadingExamples.Infrastructure;

namespace MultithreadingExamples
{
    public interface IExampleGenerator
    {
        IList<Type> GetExamples();
        IExample BuildExample(Type exampleType);
    }
}