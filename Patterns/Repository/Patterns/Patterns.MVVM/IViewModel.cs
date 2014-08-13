using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Patterns.MVVM.Infrastructure.ViewModels;

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

        bool CanNavigate(NavigationContext navigationContext);

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
}
