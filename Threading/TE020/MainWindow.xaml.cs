using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TE020
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var result = GetContent().Result; // this will be performed synchronously, but well, ok - we will wait for the response

            Result.Text = result;
        }

        private async Task<string> GetContent()
        {
            // Here is presented the pitfall - a deadlock.
            // A good explanation can be found in this posts: 
            // - http://blogs.msdn.com/b/pfxteam/archive/2011/01/13/10115163.aspx
            // - http://stackoverflow.com/questions/15021304/an-async-await-example-that-causes-a-deadlock
            var result = await (new HttpClient()).GetStringAsync("http://marcindembowski.wordpress.com/");

            // This can be good on GUI apps
            // http://stackoverflow.com/questions/13489065/best-practice-to-call-configureawait-for-all-server-side-code
            // Synchronization Context - http://msdn.microsoft.com/en-us/magazine/gg598924.aspx
            //.ConfigureAwait(false); 

            return result;
        }
    }
}
