using System;
using System.Windows.Input;

namespace MultithreadingExamples.WpfApp.Infrastructure
{
    public sealed class DelegateCommand : ICommand
    {
        private Func<bool> _canExecuteDelegate;
        private Action _executeDelegate;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action executeDelegate, Func<bool> canExecuteDelegate = null)
        {
            _executeDelegate = executeDelegate;
            _canExecuteDelegate = canExecuteDelegate;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteDelegate == null || _canExecuteDelegate();
        }

        public void Execute(object parameter)
        {
            _executeDelegate();
        }

        public void NotifyCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}