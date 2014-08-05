using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Collections
{
    public sealed class BlockingCollectionExample : CollectionsExampleBase
    {
        public const string ConsumedState            = "ConsumedState";
        public const string ProducingState           = "ProducingState";
        public const string ConsumptionFinishedState = "ConsumptionFinishedState";
        public const string ProductionFinishedState  = "ProductionFinishedState";

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
                Log.Info(ProducingState);
                _blockingCollection.Add(ProducingState);
                Thread.Sleep(500); // Simulte some work
            }

            Log.Info(ProductionFinishedState);
        }

        private void Consume()
        {
            foreach (var item in _blockingCollection.GetConsumingEnumerable())
            {
                Log.Info(ConsumedState);
            }

            Log.Info(ConsumptionFinishedState);
        }
    }
}