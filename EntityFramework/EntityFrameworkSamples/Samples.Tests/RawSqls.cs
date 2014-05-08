using System;
using System.Linq;
using EntitySamples.Infrastructure;
using NUnit.Framework;
using Samples.Tests.Models;
using Samples.Tests.TestsInfrastructure;

namespace Samples.Tests
{
    [TestFixture]
    public sealed class RawSqls : UnitOfWorkTestBase
    {
        [Test]
        public void RawSql()
        {
            // Arrange
            const string sql = "SELECT * FROM Files WHERE ID=@p0";
            const int requestedFileId = 1;

            var context = UnitOfWork.Context<SampleContext>();

            // Act
            var file = context.Files
                .SqlQuery(sql, 1)
                .First();

            // Assert
            Assert.AreEqual(requestedFileId, file.Id);
        }

        [Test]
        public void GroupCustomModel()
        {
            // Arrange
            const string sql = "SELECT IsDeleted, COUNT(*) AS Count FROM FileNotes GROUP BY IsDeleted";

            var context = UnitOfWork.Context<SampleContext>();

            // Act
            var data = context.Database
                .SqlQuery<GroupingFilesByDeleteStatus>(sql)
                .ToList();

            // Assert
            Assert.AreEqual(2, data.Count());
        }

        [Test]
        public void UpdateAll()
        {
            // Arrange
            var modDateUtc = DateTime.UtcNow.AddDays(1).Date;
            var context = UnitOfWork.Context<SampleContext>();

            // Act
            context.Database.ExecuteSqlCommand("UPDATE Files SET CreatedUtc = {0}", modDateUtc);

            var thereIsAFileWithDifferentDate = context.Files
                .Any(x => x.CreatedUtc != modDateUtc);

            // Assert
            Assert.IsFalse(thereIsAFileWithDifferentDate, "All dates should be set by the ExecuteSqlCommand.");
        }
    }
}