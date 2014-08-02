using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Collections
{
    public sealed class BlockingCollectionExample : CollectionsExampleBase
    {
        public const string Consumed = "Consumed";
        public const string Producing = "Producing";
        public const string ConsumptionFinished = "ConsumptionFinished";
        public const string ProductionFinished = "ProductionFinished";

        private readonly BlockingCollection<string> _blockingCollection = new BlockingCollection<string>();

        protected override void OnRun()
        {
            Task.Run(() => Consume());
            Produce();
        }

        public void Produce()
        {
            for (int i = 0; i < 5; ++i)
            {
                Log.Info(Producing);
                _blockingCollection.Add(Producing);
                Thread.Sleep(500); // Simulte some work
            }

            Log.Info(ProductionFinished);
        }

        private void Consume()
        {
            foreach (var item in _blockingCollection.GetConsumingEnumerable())
            {
                Log.Info(Consumed);
            }

            Log.Info(ConsumptionFinished);
        }
    }
}