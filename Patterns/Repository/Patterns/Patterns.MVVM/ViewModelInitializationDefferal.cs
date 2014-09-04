using System;
using System.Diagnostics;
using Patterns.MVVM.Infrastructure.ViewModels;

namespace Patterns.MVVM
{
    public sealed class ViewModelInitializationDefferal
    {
        private Action _continuationAction;
        internal ViewModelInitializationDefferal(Action continuationAction)
        {
            _continuationAction = continuationAction;
        }

        public void Initialized()
        {
            _continuationAction();
        }
    }


    public sealed class ViewModelClosingDefferal
    {
        private Action _closedAction;
        private Action<ViewModelState> _closingRejectedAction;
        private bool _deferralHasBeenUsed = false;
        private ViewModelState _stateBeforeCloseRequest;

        public ViewModelClosingDefferal(Action closedAction, Action<ViewModelState> closingRejectedAction, ViewModelState currentState)
        {
            _closedAction            = closedAction;
            _closingRejectedAction   = closingRejectedAction;
            _stateBeforeCloseRequest = currentState;
        }

        public void Close()
        {
            AssertThatWasUsedOnlyOnce();

            if (_closedAction != null)
            {
                _closedAction();
            }
        }

        public void Reject()
        {
            AssertThatWasUsedOnlyOnce();

            if (_closingRejectedAction != null)
            {
                _closingRejectedAction(_stateBeforeCloseRequest);
            }
        }

        [DebuggerHidden]
        private void AssertThatWasUsedOnlyOnce()
        {
            if (_deferralHasBeenUsed)
            {
                throw new InvalidOperationException("Close or Reject can be used only once.");
            }

            _deferralHasBeenUsed = true;
        }
    }
}