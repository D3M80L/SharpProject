using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Patterns.MVVM
{

    public class NavigationContext
    {
        
    }

    public interface IIsBusyIndication
    {
        bool IsBusy { get; }
    }

    public interface IViewModel : IDisposable, IIsBusyIndication
    {
        Guid Id { get; }

        void Navigate(NavigationContext navigationContext);

        void Activate();

        void Deactivate();

        void Close();

        ViewModelState State { get; }
    }

    public interface IRegion : IViewModel
    {
        void AddViewModel(IViewModel viewModel);

        IEnumerable<IViewModel> AvailableViewModels { get; }
    }


    public enum ViewModelState
    {
        Created,

        Initializing,

        Initialized,

        Activating,

        Activated,

        Deactivating,

        Deactivated,

        Closing,

        Closed,

        Disposing,

        Disposed
    }
}
