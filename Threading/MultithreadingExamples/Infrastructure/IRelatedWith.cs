namespace MultithreadingExamples.Infrastructure
{
    public interface IRelatedWith<TExample> : IExample
        where TExample : IExample
    {
        
    }
}