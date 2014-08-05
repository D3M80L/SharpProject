using System.Threading;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Signaling
{
    public sealed class OnlyOneInstanceCanBeExecutedOnMachineExample : ExampleBase
    {
        public const string MutexName                    = "ThereCanBeOnlyOne";
        public const string AcquiringMutextState         = "AcquiringMutextState";
        public const string OwningMutexState             = "OwningMutexState";
        public const string MutexOwnedBySomeoneElseState = "MutexOwnedBySomeoneElseState";

        protected override void OnRun()
        {
            Log.Info(AcquiringMutextState);

            using (var mutex = new Mutex(true, MutexName))
            {
                if (mutex.WaitOne(5000))
                {
                    Log.Info(OwningMutexState);
                    Interaction.ConfirmationRequest();
                }
                else
                {
                    Log.Info(MutexOwnedBySomeoneElseState);
                }
            }
        }
    }
}