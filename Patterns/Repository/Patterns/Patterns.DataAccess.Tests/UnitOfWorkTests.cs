using NUnit.Framework;
using Patterns.DataAccess.Infrastructure;
using Patterns.DataAccess.Tests.TestData;
using Rhino.Mocks;

namespace Patterns.DataAccess.Tests
{
    [TestFixture]
    public sealed class UnitOfWorkTests
    {
        [Test]
        public void CallSaveOnUnitOfWork_SaveShouldBeCalledOnContext()
        {
            // Arrange
            var contextMock = MockRepository.GenerateMock<ContextMock>();
            IUnitOfWork unitOfWork = new OneContextedUnitOfWork<ContextMock>(contextMock, x => x.Save());

            // Act
            unitOfWork.Save();

            // Assert
            contextMock.AssertWasCalled(x=>x.Save());
        }

        [Test]
        public void GetContextFromUnitOfWork_InstanceOfContextShouldBeReturned()
        {
            // Arrange
            var contextMock = MockRepository.GenerateMock<ContextMock>();
            IUnitOfWork unitOfWork = new OneContextedUnitOfWork<ContextMock>(contextMock, x => x.Save());

            // Act
            var context = unitOfWork.Context<ContextMock>();

            // Assert
            Assert.AreSame(contextMock, context);
        }
    }
}
