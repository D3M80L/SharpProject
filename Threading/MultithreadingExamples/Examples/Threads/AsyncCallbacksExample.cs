using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure.Exceptions;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.Examples.Threads
{
    public sealed class AsyncCallbacksExample : ThreadExampleBase
    {
        public const string Calculating = "Calculating";
        public const string CalculationCallbackMessage = "CalculationCallbackMessage";
        public const string AfterEndInvoke = "AfterEndInvoke";

        protected override void OnRun()
        {
            Func<int, int> calculator = Calculate;
            calculator.BeginInvoke(10, CalculationCallback, calculator); // NOTE: the callback
        }

        private int Calculate(int x)
        {
            Log.Info(Calculating);

            throw new VeryImportantException();
        }

        private void CalculationCallback(IAsyncResult asyncResult)
        {
            Log.Info(CalculationCallbackMessage);
            try
            {
                var target = (Func<int, int>)asyncResult.AsyncState;
                int result = target.EndInvoke(asyncResult); 

                Log.Info(AfterEndInvoke); // Never called, because the exception was thrown
            }
            catch (VeryImportantException)
            {
                Log.Info(ImportantException);
            }
        }
    }
}
