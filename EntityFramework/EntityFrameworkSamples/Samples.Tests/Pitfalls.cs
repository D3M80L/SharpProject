using System;
using System.Linq;
using EntitySamples.DataModel;
using EntitySamples.Infrastructure;
using NUnit.Framework;
using Samples.Tests.TestsInfrastructure;

namespace Samples.Tests
{
    [TestFixture]
    public sealed class Pitfalls : UnitOfWorkTestBase
    {
        /// <summary>
        /// This tes may fail sometimes.
        /// </summary>
        /// <remarks>
        /// This test may fail because:
        /// - When creating a file object we use value from .NET DateTime.UtcNow
        /// - When calling SQL the expression transforms DateTime.UtcNow to SQL representaion
        /// 
        /// The issue may appear when database server is physically on another machine.
        /// The database server may have a small time offset (one second or few miliseconds) resulting in such comparision
        /// .NET DateTime.UtcNow: 2014-12-12 12:12:12;
        /// Database SysUtcDateTime(): 2014-12-12 12:12:11;
        /// 
        /// As a result no file will be found.
        /// </remarks>
        [Test]
        public void DateTimeNow()
        {
            // Arrange
            var fileNumber = (new Random()).Next();
            var context = UnitOfWork.Context<SampleContext>();
            context.Database.Log = Console.WriteLine;

            context.Files
                .Add(new File
                {
                    ModUserId = 1,
                    Number = fileNumber,
                    CreatedUtc = DateTime.UtcNow,
                    FileDate = DateTime.UtcNow, // FIRST
                });
            UnitOfWork.Save();

            // Act
            var file = context.Files
                .FirstOrDefault(x => x.FileDate <= DateTime.UtcNow && x.Number == fileNumber); // SECOND

            // Assert
            Assert.IsNotNull(file);
        }

        /// <summary>
        /// This test may fail
        /// </summary>
        /// <remarks>
        /// DateTime milisecond precision: fileCreationDate = fileCreationDate.AddMilliseconds(-fileCreationDate.Millisecond);
        /// See: http://stackoverflow.com/questions/8043816/using-datetime-properties-in-code-first-entity-framework-and-sql-server
        /// </remarks>
        [Test]
        public void DateTimePrecision()
        {
            // Arrange
            var context = UnitOfWork.Context<SampleContext>();
            context.Database.Log = Console.WriteLine;

            var fileCreationDate = DateTime.UtcNow;

            context.Files.Add(new File
            {
                ModUserId = 1,
                CreatedUtc = fileCreationDate,
            });
            UnitOfWork.Save();

            // Act
            var file = context.Files
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .First();

            // Assert
            Assert.AreEqual(fileCreationDate, file.CreatedUtc);
        }
    }
}