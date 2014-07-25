using System;

namespace MultithreadingExamples.WpfApp.Models
{
    public class ExampleItem
    {
        public ExampleItem(Type exampleTypeDefinition)
        {
            ExampleDefinitionType = exampleTypeDefinition;
            Name = exampleTypeDefinition.FullName;
        }

        public string Name { get; private set; }

        public Type ExampleDefinitionType { get; private set; }
    }
}