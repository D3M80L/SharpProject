using System;
using NUnit.Framework;
using Patterns.DataAccess.Infrastructure;
using Patterns.DataAccess.Tests.TestData;
using Rhino.Mocks;

namespace Patterns.DataAccess.Tests.Infrastructure
{

    [TestFixture]
    public sealed class OneContextedUnitOfWorkTests
    {
        private OneContextedUnitOfWork<ContextMock> BuildUnitOfWork(ContextMock contextMock)
        {
            return new OneContextedUnitOfWork<ContextMock>(contextMock, saveHandler: x => x.Save(), disposeHandler: x => x.Dispose());
        }

        [Test]
        public void CallSaveOnUnitOfWork_SaveShouldBeCalledOnContext()
        {
            // Arrange
            var contextMock        = MockRepository.GenerateMock<ContextMock>();
            IUnitOfWork unitOfWork = BuildUnitOfWork(contextMock);

            // Act
            unitOfWork.Save();

            // Assert
            contextMock.AssertWasCalled(x=>x.Save());
        }

        [Test]
        public void GetContextFromUnitOfWork_InstanceOfContextShouldBeReturned()
        {
            // Arrange
            var contextMock        = MockRepository.GenerateMock<ContextMock>();
            IUnitOfWork unitOfWork = BuildUnitOfWork(contextMock);

            // Act
            var context = unitOfWork.Context<ContextMock>();

            // Assert
            Assert.AreSame(contextMock, context);
        }

        [Test]
        public void DisposeUnitOfWork_DisposeShouldBeCalledOnContext()
        {
            // Arrange
            var contextMock = MockRepository.GenerateMock<ContextMock>();
            var unitOfWork  = BuildUnitOfWork(contextMock);

            // Act
            unitOfWork.Dispose();

            // Assert
            contextMock.AssertWasCalled(x=>x.Dispose());
        }

        [Test]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void DisposeUnitOfWork_SaveShouldThrowException()
        {
            // Arrange
            var contextMock = MockRepository.GenerateMock<ContextMock>();
            var unitOfWork = BuildUnitOfWork(contextMock);
            unitOfWork.Dispose();

            // Act
            unitOfWork.Save();

            // Assert
            Assert.Fail("Exception was not thrown.");
        }

        [Test]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void DisposeUnitOfWork_ContextShouldThrowException()
        {
            // Arrange
            var contextMock = MockRepository.GenerateMock<ContextMock>();
            var unitOfWork = BuildUnitOfWork(contextMock);
            unitOfWork.Dispose();

            // Act
            unitOfWork.Context<ContextMock>();

            // Assert
            Assert.Fail("Exception was not thrown.");
        }
    }
}
