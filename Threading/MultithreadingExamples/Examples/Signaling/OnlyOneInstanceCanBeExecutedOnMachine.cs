using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Signaling
{
    public sealed class OnlyOneInstanceCanBeExecutedOnMachine : ExampleBase
    {
        public const string MutexName = "ThereCanBeOnlyOne";
        public const string AcquiringMutext = "AcquiringMutext";
        public const string OwningMutex = "OwningMutex";
        public const string MutexOwnedBySomeoneElse = "MutexOwnedBySomeoneElse";

        protected override void OnRun()
        {
            Log.Info(AcquiringMutext);

            using (var mutex = new Mutex(true, MutexName))
            {
                if (mutex.WaitOne(5000))
                {
                    Log.Info(OwningMutex);
                    Interaction.ConfirmationRequest();
                }
                else
                {
                    Log.Info(MutexOwnedBySomeoneElse);
                }
            }
        }
    }
}