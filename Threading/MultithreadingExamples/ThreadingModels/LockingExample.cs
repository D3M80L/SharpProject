using System.Net.Http;
using System.Threading.Tasks;
using MultithreadingExamples.Examples.AsyncAwaits;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.Infrastructure.Extensions;

namespace MultithreadingExamples.ThreadingModels
{
    /// <summary>
    /// A GUI application - like WinForms or WPF have a specific threading model, where a thread which created a control
    /// is the owner of all update actions perfomed on the control.
    /// 
    /// In most cases, a modern UI application performs multiple operations by calling some actions on a database, webservice or even local files.
    /// (Yes actions on local files - even those simple looking, like create file - can take a while).
    /// Those actions can take some time to complete, in meanwhile the user should be able to do something else, and the UI should be responsive.
    /// 
    /// Creating a thread where the call to an externall resource is made, and setting a result on the UI after the thread finishes the call, can 
    /// perform an exception.
    /// 
    /// As a rescue comes a <see cref="System.Threading.SynchronizationContext"/>. This object is used during 'await'ing a Task. It is then guaranteed, that
    /// the thread which called the async method will continue the task in the same thread. For UI applications it will be UI thread, for ASP.NET 
    /// a thread which owns the user identity.
    /// </summary>
    public sealed class LockingExample : ThreadingModelsBase, IHasSolutionIn<SolutionForLockingExample>, IRelatedWith<AsyncVoidCrashingExample>
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
            // Here is presented the pitfall - a deadlock.
            // A good explanation can be found in this posts: 
            // - http://blogs.msdn.com/b/pfxteam/archive/2011/01/13/10115163.aspx
            // - http://stackoverflow.com/questions/15021304/an-async-await-example-that-causes-a-deadlock
            var response = await new HttpClient().GetAsync("http://wikipedia.pl/");

            // DEADLOCK

            // This can be good on GUI apps
            // http://stackoverflow.com/questions/13489065/best-practice-to-call-configureawait-for-all-server-side-code
            // http://msdn.microsoft.com/en-us/magazine/jj991977.aspx
            // Synchronization Context - http://msdn.microsoft.com/en-us/magazine/gg598924.aspx
            return await response.Content.ReadAsStringAsync();
        }
    }
}