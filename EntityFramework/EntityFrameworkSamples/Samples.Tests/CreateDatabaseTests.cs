using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitySamples.DataModel;
using EntitySamples.Infrastructure;
using NUnit.Framework;
using Samples.Tests.TestsInfrastructure;

namespace Samples.Tests
{
    [TestFixture]
    public sealed class CreateDatabaseTests
    {
        [Test]
        public void TestConnection()
        {
            // Arrange
            var unitOfWork = new UnitOfWork();

            // Act
            unitOfWork.Context<SampleContext>().Users.Any();

            // Assert
            unitOfWork.Dispose();
        }

        [Test]
        public void FillDatabase()
        {
            // Arrange
            var unitOfWork = new UnitOfWork();
            var context = unitOfWork.Context<SampleContext>();
            if (context.Files.Any())
            {
                throw new InvalidOperationException("Seems that thre are some data in database.");
            }
        
            (new DatabaseFill()).Fill(context);

            // Assert
            unitOfWork.Save();
        }
    }
}
