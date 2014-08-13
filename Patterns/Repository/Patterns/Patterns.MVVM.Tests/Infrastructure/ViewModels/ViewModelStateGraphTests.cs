using NUnit.Framework;
using Patterns.MVVM.Infrastructure.ViewModels;

namespace Patterns.MVVM.Tests.Infrastructure.ViewModels
{
    [TestFixture]
    public class ViewModelStateGraphTests
    {
        [TestCase(ViewModelState.Created, ViewModelState.Disposing)]
        [TestCase(ViewModelState.Disposing, ViewModelState.Disposed)]
        [TestCase(ViewModelState.Activating, ViewModelState.Activated)]
        [TestCase(ViewModelState.Deactivating, ViewModelState.Deactivated)]
        [TestCase(ViewModelState.Initializing, ViewModelState.Initialized)]
        public void AllowedChanges(ViewModelState currentState, ViewModelState newState)
        {
            // Arrange

            // Act
            var result = ViewModelStateGraph.CanChangeTo(currentState, newState);

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase(ViewModelState.Created, ViewModelState.Disposed)]
        public void ForbiddenChanges(ViewModelState currentState, ViewModelState newState)
        {
            // Arrange

            // Act
            var result = ViewModelStateGraph.CanChangeTo(currentState, newState);

            // Assert
            Assert.IsFalse(result);
        }
    }
}