using System.Windows;
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
            UiDispatcher.SetDispatcher(Dispatcher);

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
