using MultithreadingExamples.Infrastructure;

namespace MultithreadingExamples.Tests.Infrastructure
{
    public static class PresentationExtensions
    {
        public static Presentation ExplainTest<TTest>(this Presentation presentation)
            where TTest : class, new()
        {
            return presentation;
        }

        public static Presentation ExplainExample<TExample>(this Presentation presentation)
            where TExample : class, IExample, new()
        {
            return presentation;
        }
    }
}