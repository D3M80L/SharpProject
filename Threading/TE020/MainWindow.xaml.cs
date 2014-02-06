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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var result = GetContent().Result; // this will be performed synchronously

            Result.Text = result;
        }

        private async Task<string> GetContent()
        {
            var result = await (new HttpClient()).GetStringAsync("http://marcindembowski.wordpress.com/")
            .ConfigureAwait(false); // http://stackoverflow.com/questions/13489065/best-practice-to-call-configureawait-for-all-server-side-code

            return result;
        }
    }
}
