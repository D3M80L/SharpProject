using System;
using System.Linq;
using EntitySamples.Infrastructure;
using NUnit.Framework;
using System.Data.Entity;
using Samples.Tests.TestsInfrastructure;

namespace Samples.Tests
{
    /// <summary>
    /// http://msdn.microsoft.com/pl-pl/data/jj574232.aspx
    /// </summary>
    [TestFixture]
    public class QueryingExamples : UnitOfWorkTestBase
    {
  

        /// <summary>
        /// Take a look at the results and generated result.
        /// This query presents you that in some cases Include (Eager load) has a negative impact on performance.
        /// Why:
        /// Imagine that the file table contains for example 50 columns with different data, and the FileNotes tables still remains simple.
        /// When a file will contain 100 simple notes, then the major part of the SQL result will be a file with duplicated data!
        /// </summary>
        [Test]
        public void EagerLoad()
        {
            // Arrange
            var context = UnitOfWork.Context<SampleContext>();

            // Act
            var files = context.Files
                .Include(i => i.FileNotes)
                .Include(i => i.ModUser)
                .ToList();

            // Assert
            Assert.IsTrue(files.Any());
        }

        /// <summary>
        /// In this example we just load the collection when necessary and when not loaded.
        /// Still we use eager loading for ModUser, because one file has only one user in it and in a result file data will not be duplicated.
        /// Please note that here we are performing a few SQL queries instead one large.
        /// Also note that users data may be duplicated in the result data.
        /// </summary>
        [Test]
        public void LoadCollectionWhenNecessary()
        {
            // Arrange
            var context = UnitOfWork.Context<SampleContext>();

            // Act
            var files = context.Files
                .Include(i => i.ModUser)
                .ToList();

            foreach (var file in files)
            {
                var reference = context.Entry(file).Collection(x=>x.FileNotes);
                if (!reference.IsLoaded)
                {
                    Console.WriteLine("Loading collection for file id ={0}", file.Id);
                    reference.Load();
                }
                else
                {
                    Console.WriteLine("Collection already loaded for file id ={0}", file.Id);
                }
            }

            // Assert
            Assert.IsTrue(files.Any());
        }

        [Test]
        public void LoadReferenceWhenNecessary()
        {
            // Arrange
            var context = UnitOfWork.Context<SampleContext>();

            // Act
            var files = context.Files
                .ToList();

            foreach (var file in files)
            {
                var reference = context.Entry(file).Reference(x => x.ModUser);
                if (!reference.IsLoaded)
                {
                    Console.WriteLine("Loading reference for file id ={0}", file.Id);
                    reference.Load();
                }
                else
                {
                    Console.WriteLine("Reference already loaded for file id ={0}", file.Id);
                }
            }

            // Assert
            Assert.IsTrue(files.Any());
        }

        [Test]
        public void ReferenceLoadedByEagerLoad()
        {
            // Arrange
            var context = UnitOfWork.Context<SampleContext>();

            // Act
            var files = context.Files
                .Include(i=>i.ModUser)
                .ToList();

            foreach (var file in files)
            {
                var reference = context.Entry(file).Reference(x => x.ModUser);
                if (!reference.IsLoaded)
                {
                    Assert.Fail("Seems that EF does not see the information, that the reference has been loaded using EagerLoad.");
                }
            }

            // Assert
            Assert.IsTrue(files.Any());
        }

        [Test]
        public void ModUserLoadedByLazyLoad()
        {
            // Arrange
            var context = UnitOfWork.Context<SampleContext>();

            // Act
            var file = context.Files
                .First();

            // Assert
            Assert.IsTrue(file.ModUserId > 0);
            // Break here and check what is printed in the sql trace
            Assert.NotNull(file.ModUser);
        }

        [Test]
        public void ModUserEntityNotAvailableBecauseLazyLoadIsOff()
        {
            // Arrange
            var context = UnitOfWork.Context<SampleContext>();
            context.Configuration.LazyLoadingEnabled = false;

            // Act
            var file = context.Files
                .First();

            // Assert
            Assert.IsTrue(file.ModUserId > 0);
            Assert.IsNull(file.ModUser);
        }

        [Test]
        public void LoadOnlyNotDeletedNotest()
        {
            // Arrange
            var context = UnitOfWork.Context<SampleContext>();
            context.Configuration.LazyLoadingEnabled = false; // IMPORTANT: must be disabled

            // Act
            var file = context.Files
                .FirstOrDefault(x => x.FileNotes.Any());

            context
                  .Entry(file)
                  .Collection(x => x.FileNotes)
                  .Query()
                  .Where(x => !x.IsDeleted)
                  .Load();

            // Assert
            Assert.IsTrue(file.FileNotes.Count > 0);
            Assert.IsTrue(file.FileNotes.All(x=>x.IsDeleted == false));
        }
    }
}
