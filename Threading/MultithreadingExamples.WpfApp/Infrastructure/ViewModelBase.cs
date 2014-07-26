using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace MultithreadingExamples.WpfApp.Infrastructure
{
    public abstract class ViewModelBase : IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _busyCounter = 0;
        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            protected set
            {
                if (value)
                {
                    Interlocked.Increment(ref _busyCounter);
                }
                else
                {
                    Interlocked.Decrement(ref _busyCounter);
                }

                var isBusy = Interlocked.CompareExchange(ref _busyCounter, 0, 0) == 0;
                Set(ref _isBusy, isBusy);
            }
        }

        public void Loaded()
        {
            OnLoaded();
        }

        protected virtual void OnLoaded()
        {
            
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                UiDispatcher.RunAsyncInUi(()=> handler(this, new PropertyChangedEventArgs(propertyName)));
            }
        }

        protected bool Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }
            field = newValue;

            NotifyPropertyChanged(propertyName);

            return true;
        }
    }
}