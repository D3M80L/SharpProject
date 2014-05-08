using System;
using System.Linq;
using EntitySamples.Infrastructure;
using NUnit.Framework;
using Samples.Tests.TestsInfrastructure;

namespace Samples.Tests
{
    [TestFixture]
    public sealed class TrackingEntities : UnitOfWorkTestBase
    {

        [Test]
        public void SelectNormal()
        {
            // Arrange
            const int fileId = 1;
            var newTitle = Guid.NewGuid().ToString();

            var context = UnitOfWork.Context<SampleContext>();
            var file = context.Files
                .FirstOrDefault(x => x.Id == fileId);

            // Act
            file.Title = newTitle;
            var newFile = context.Files
                .FirstOrDefault(x => x.Id == fileId);

            // Assert
            Assert.AreEqual(newTitle, newFile.Title);
        }


        [Test]
        public void SelectAsNoTracking()
        {
            // Arrange
            const int fileId = 1;
            var newTitle = Guid.NewGuid().ToString();

            var context = UnitOfWork.Context<SampleContext>();
            var file = context.Files
                .AsNoTracking() // Note this
                .FirstOrDefault(x => x.Id == fileId);

            // Act
            file.Title = newTitle;
            var newFile = context.Files
                .FirstOrDefault(x => x.Id == fileId);

            // Assert
            Assert.AreNotEqual(newTitle, newFile.Title);
        }
    }
}