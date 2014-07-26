using System.Net.Http;
using System.Threading.Tasks;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.ThreadingModels
{
    public sealed class LockingExample : ThreadingModelsBase, IHasSolutionIn<SolutionForLockingExample>
    {
        public const string Response = "Response";

        /// <summary>
        ///  This method, when called from UI thread will lock the application
        /// </summary>
        protected override void OnRun()
        {
            var response = Retrieve().Result;

            Log.Info(response);
            Log.Info(Response);
        }

        private async Task<string> Retrieve()
        {
            var response = await new HttpClient().GetAsync("http://wikipedia.pl/");
            // DEADLOCK
            return await response.Content.ReadAsStringAsync();
        }
    }
}