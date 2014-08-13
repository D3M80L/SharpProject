using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Patterns.MVVM.Infrastructure.ViewModels;

namespace Patterns.MVVM
{
    public abstract class ViewModelBase : NotifyPropertyChangedBase, IViewModel
    {
        private readonly Guid _id = Guid.NewGuid();
        public Guid Id
        {
            get { return _id; }
        }

        private int _busyCount = 0;
        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            protected set
            {
                ThrowIfDisposed();

                if (value)
                {
                    Interlocked.Increment(ref _busyCount);
                }
                else
                {
                    Interlocked.Decrement(ref _busyCount);
                }

                var newBusyState = _busyCount != 0; // TODO: for me it's not thread safe
                if (Set(ref _isBusy, newBusyState))
                {
                    OnBusyChanged(newBusyState);
                }
            }
        }

        private ViewModelState _viewModelState = ViewModelState.Created;
        /// <summary>
        /// Only the base class should be able to set the VM state.
        /// The developer should not set the state directly.
        /// </summary>
        /// <returns></returns>
        public ViewModelState State
        {
            get { return _viewModelState; }
            private set
            {
                if (Set(ref _viewModelState, value))
                {
                    try
                    {
                        OnStateChanged(value);
                    } catch { }
                }
            }
        }

        public bool CanNavigate(NavigationContext navigationContext)
        {
            ThrowIfDisposed();

            return OnCanNavigate(navigationContext);
        }

        public void Navigate(NavigationContext navigationContext)
        {
            ThrowIfDisposed();

            if (!OnCanNavigate(navigationContext))
            {
                throw new InvalidOperationException("Navigation not allowed. Call OnCanNavigate first to determine if navigation is allowed.");
            }

            OnNavigate(navigationContext);
        }

        public void Activate()
        {
            ThrowIfDisposed();

            if (!CheckInitialization(RequestActivate))
            {
                return;
            }

            RequestActivate();
        }

        private void RequestActivate()
        {
            OnActivate();
        }

        public void Deactivate()
        {
            ThrowIfDisposed();

            throw new NotImplementedException();
        }

        public void Close()
        {
            ThrowIfDisposed();

            throw new NotImplementedException();
        }

        private bool _isDisposed = false;
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;

            try
            {
                OnDispose();
            }
            catch
            {
                
            }
        }


        protected virtual void OnActivate() { }

        protected virtual void OnBusyChanged(bool newValue) { }

        protected virtual bool OnCanNavigate(NavigationContext navigationContext)
        {
            return true;
        }

        protected virtual void OnDispose() { }

        protected virtual void OnInitialize(ViewModelInitializationDefferal defferal)
        {
            defferal.Initialized();
        }

        protected virtual void OnNavigate(NavigationContext navigationContext) { }

        protected virtual void OnStateChanged(ViewModelState newState) { }

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

        [DebuggerHidden]
        protected void ThrowIfDisposed()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        private bool SetState(ViewModelState viewModelState)
        {
            // TODO: check the state graph
            State = viewModelState;
            return true;
        }

        private bool CheckInitialization(Action continuationAction)
        {
            if (State == ViewModelState.Initializing)
            {
                return false;
            }

            if (State == ViewModelState.Created)
            {
                var context = new ViewModelInitializationDefferal(continuationAction);
                SetState(ViewModelState.Initializing);
                OnInitialize(context);
                return false;
            }

            return true;
        }
    }
}