using NUnit.Framework;
using Patterns.MVVM.Infrastructure.ViewModels;
using Patterns.MVVM.Tests.TestData;

namespace Patterns.MVVM.Tests
{
    [TestFixture]
    public class ViewModelBaseTests
    {

        [Test]
        public void StateShouldBeCreatedAfterCreatingViewModelInstance()
        {
            // Arrange
            var viewModel = new MyViewModel();

            // Act
            var state = viewModel.State;

            // Assert
            Assert.AreEqual(ViewModelState.Created, state);
        }
    }
}