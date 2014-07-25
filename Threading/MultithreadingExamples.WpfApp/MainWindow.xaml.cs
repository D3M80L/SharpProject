using System;
using System.Collections.Generic;
using System.Linq;
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
using MultithreadingExamples.WpfApp.Infrastructure;
using MultithreadingExamples.WpfApp.ViewModels;

namespace MultithreadingExamples.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IViewModel _viewModel;

        public MainWindow()
        {
            _viewModel = new ExamplesBoardViewModel();
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel.Loaded();
        }
    }
}
