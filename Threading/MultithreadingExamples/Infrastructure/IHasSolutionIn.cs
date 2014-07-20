namespace MultithreadingExamples.Infrastructure
{
    public interface IHasSolutionIn<TExample> : IExample
        where TExample : IExample
    {
        
    }
}