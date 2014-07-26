using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using MultithreadingExamples.Infrastructure;
using MultithreadingExamples.WpfApp.Infrastructure;
using MultithreadingExamples.WpfApp.Models;

namespace MultithreadingExamples.WpfApp.ViewModels
{
    public sealed class ExamplesBoardViewModel : ViewModelBase
    {
        private readonly IExampleGenerator _exampleGenerator;
        private ExamplesExecutionContext _examplesExecutionContext;

        public ICollectionView ExamplesView
        {
            get { return _examplesView; }
            private set
            {
                Set(ref _examplesView, value);
            }
        }

        public ObservableCollection<string> Messages { get; private set; }

        public DelegateCommand RunExampleCommand { get; private set; }

        public DelegateCommand ConfirmationCommand { get; private set; }

        private ExampleItem _selectedExample;
        public ExampleItem SelectedExample
        {
            get { return _selectedExample; }
            set
            {
                if (Set(ref _selectedExample, value))
                {
                    NotifyCommandsChange();
                }
            }
        }

        private string _exampleFilter;
        private ICollectionView _examplesView;

        public string ExampleFilter
        {
            get { return _exampleFilter; }
            set
            {
                if (Set(ref _exampleFilter, value))
                {
                    ExamplesView.Refresh();
                }
            }
        }

        public ExamplesBoardViewModel()
        {
            _exampleGenerator = new ExamplesGenerator();

            Messages = new ObservableCollection<string>();
            RunExampleCommand = new DelegateCommand(RunExample, CanRunExample);
            ConfirmationCommand = new DelegateCommand(Confirmation, CanConfirmation);
        }

        protected override void OnLoaded()
        {
            var examples = _exampleGenerator
                .GetExamples()
                .Select(exampleType => new ExampleItem(exampleType))
                .ToList();

            var view = CollectionViewSource.GetDefaultView(examples);
            view.Filter = Filter;
            ExamplesView = view;

        }

        private bool Filter(object item)
        {
            if (string.IsNullOrWhiteSpace(ExampleFilter))
            {
                return true;
            }

            var exampleItem = item as ExampleItem;
            if (exampleItem == null)
            {
                return true;
            }

            return exampleItem.Name.IndexOf(ExampleFilter, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private bool CanRunExample()
        {
            return SelectedExample != null && !IsBusy;
        }

        private void RunExample()
        {
            IsBusy = true;
            NotifyCommandsChange();
            if (_examplesExecutionContext != null)
            {
                _examplesExecutionContext.Dispose();
            }

            var exampleInstance = _exampleGenerator.BuildExample(SelectedExample.ExampleDefinitionType);
            _examplesExecutionContext = new ExamplesExecutionContext(exampleInstance, LogHandler, ConfirmationRequestHandler);
            _examplesExecutionContext.Run();
        }

        public bool IsConfirmationRequested { get; private set; }
        private bool CanConfirmation()
        {
            return IsConfirmationRequested;
        }

        private void Confirmation()
        {
            IsConfirmationRequested = false;
            NotifyCommandsChange();
            _examplesExecutionContext.Confirm();
        }

        private void LogHandler(string message)
        {
            UiDispatcher.RunAsyncInUi(()=> Messages.Add(message));
        }

        private void ConfirmationRequestHandler()
        {
            IsConfirmationRequested = true;
            NotifyCommandsChange();
        }

        private void NotifyCommandsChange()
        {
            UiDispatcher.RunAsyncInUi(()=>
            {
                RunExampleCommand.NotifyCanExecuteChanged();
                ConfirmationCommand.NotifyCanExecuteChanged();
            });
        }
    }
}
