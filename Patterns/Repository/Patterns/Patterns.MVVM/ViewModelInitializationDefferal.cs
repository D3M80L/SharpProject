using System;

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
}