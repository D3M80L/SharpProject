using System;
using System.Linq;
using EntitySamples.Infrastructure;
using NUnit.Framework;
using Samples.Tests.TestsInfrastructure;
using System.Collections.Generic;

namespace Samples.Tests
{
    [TestFixture]
    public sealed class SqlLoggingExamples : UnitOfWorkTestBase
    {
        [Test]
        public void LogSqlSentToDatabase()
        {
            // Arrange
            var context = UnitOfWork.Context<SampleContext>();
            var log = new List<string>();
            context.Database.Log = x =>
            {
                Console.WriteLine("---");
                Console.WriteLine(x);
                log.Add(x);
                Console.WriteLine("---");
            };

            // Act
            var file = context.Files.First();
            file.CreatedUtc = DateTime.UtcNow;
            UnitOfWork.Save();

            // Assert
            Assert.IsTrue(log.Any(), "Seems that nothig has been logged.");
        }   
    }
}