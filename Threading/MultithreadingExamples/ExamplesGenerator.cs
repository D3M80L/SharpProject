using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MultithreadingExamples.Infrastructure;

namespace MultithreadingExamples
{
    public sealed class ExamplesGenerator : IExampleGenerator
    {
        private static readonly Type ExmpleInterfaceTypeDefinition = typeof(IExample);

        public IList<Type> GetExamples()
        {
            return Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(TypeIsExampleClass)
                .ToList();
        }

        public IExample BuildExample(Type exampleType)
        {
            if (!TypeIsExampleClass(exampleType))
            {
                throw new ArgumentException("The type must be a class which is an ExampleClass");
            }

            return Activator.CreateInstance(exampleType) as IExample;
        }

        private static bool TypeIsExampleClass(Type type)
        {
            return ExmpleInterfaceTypeDefinition.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract;
        }
    }
}
