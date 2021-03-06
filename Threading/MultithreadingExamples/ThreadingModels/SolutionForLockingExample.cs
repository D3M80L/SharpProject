﻿using System.Net.Http;
using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.ThreadingModels
{
    public sealed class SolutionForLockingExample : ThreadingModelsBase, ISolutionFor<LockingExample>
    {
        public const string Response = "ResponseState";

        protected override void OnRun()
        {
            var response = Retrieve().Result;

            Log.Info(response);
            Log.Info(Response);
        }

        private async Task<string> Retrieve()
        {
            var response = await new HttpClient().GetAsync("http://wikipedia.pl/")
                .ConfigureAwait(false);

            return await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);
        }
    }
}