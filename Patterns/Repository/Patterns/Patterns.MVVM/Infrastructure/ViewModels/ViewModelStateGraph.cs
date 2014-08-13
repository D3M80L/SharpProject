using System.Collections.Generic;
using System.Linq;

namespace Patterns.MVVM.Infrastructure.ViewModels
{
    public class ViewModelStateGraph
    {
        private static readonly Dictionary<ViewModelState, ViewModelState[]> StateMap = new Dictionary<ViewModelState, ViewModelState[]>
        {
            { ViewModelState.Created, new[] { ViewModelState.Disposing, ViewModelState.Initializing } },
            { ViewModelState.Initializing, new[] { ViewModelState.Initialized, ViewModelState.Disposing } },
            { ViewModelState.Activating, new[] { ViewModelState.Activated, ViewModelState.Disposing } },
            { ViewModelState.Deactivating, new[] { ViewModelState.Deactivated, ViewModelState.Disposing } },
            { ViewModelState.Disposing, new[] { ViewModelState.Disposed } },
        };

        public static bool CanChangeTo(ViewModelState currentState, ViewModelState newState)
        {
            if (StateMap.ContainsKey(currentState))
            {
                return StateMap[currentState].Contains(newState);
            }

            return false;
        }
    }
}