namespace MultithreadingExamples.Infrastructure
{
    public interface ISolutionFor<TExample> : IExample
        where TExample : IExample
    {

    }
}