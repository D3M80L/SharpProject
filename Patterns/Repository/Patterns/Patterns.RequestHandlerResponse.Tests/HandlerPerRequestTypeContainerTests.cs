using System.Reflection;
using NUnit.Framework;
using Patterns.RequestHandlerResponse.Tests.TestData.Requests;

namespace Patterns.RequestHandlerResponse.Tests
{
    [TestFixture]
    public class HandlerPerRequestTypeContainerTests
    {
        [Test]
        public void Autoregistration_CreateHandlerForType_ReturnsInstance()
        {
            // Arrange
            var collection = new HandlerPerRequestTypeContainer();
            collection.RegisterAllHandlersFromAssembly(Assembly.GetExecutingAssembly());
            var requestType = typeof (TestRequest);

            // Act
            var handlerInstance = collection.BuildHandlerFor(requestType);

            // Assert
            Assert.IsNotNull(handlerInstance);
        }

        [Test]
        public void Autoregistration_RequestWithoutHandlerDefinition_ReturnsNull()
        {
            // Arrange
            var collection = new HandlerPerRequestTypeContainer();
            collection.RegisterAllHandlersFromAssembly(Assembly.GetExecutingAssembly());
            var requestType = typeof(TestRequestWithoutHandlerRequest);

            // Act
            var handlerInstance = collection.BuildHandlerFor(requestType);

            // Assert
            Assert.IsNull(handlerInstance);
        }
    }
}