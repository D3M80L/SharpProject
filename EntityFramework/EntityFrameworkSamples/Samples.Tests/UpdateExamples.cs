using System;
using System.Data.Entity;
using System.Linq;
using EntitySamples.Infrastructure;
using NUnit.Framework;
using Samples.Tests.TestsInfrastructure;

namespace Samples.Tests
{
    [TestFixture]
    public sealed class UpdateExamples : UnitOfWorkTestBase
    {
        /// <summary>
        /// http://coding.abel.nu/2012/03/ef-code-first-navigation-properties-and-foreign-keys/
        /// </summary>
        [Test]
        public void ChangeModUserByEntity_ReferenceIdDoesNotChange()
        {
            // Arrange
            var context = UnitOfWork.Context<SampleContext>();
            var allUsers = context.Users.ToList();
            var findUser = allUsers[0];
            var replaceUser = allUsers[1];

            // Act
            var file = context.Files.First(x => x.ModUserId == findUser.Id);
            file.ModUser = replaceUser;

            // Assert
            Assert.AreNotEqual(replaceUser.Id, file.ModUserId);
            UnitOfWork.Save();
            Assert.AreEqual(replaceUser.Id, file.ModUserId);
        }


        [Test]
        public void ChangeModUserIdBeforeAccessingTheReferenceProperyModUser()
        {
            // Arrange
            var context = UnitOfWork.Context<SampleContext>();
            var allUsers = context.Users.ToList();
            var findUser = allUsers[0];
            var replaceUser = allUsers[1];

            // Act
            var file = context.Files.First(x => x.ModUserId == findUser.Id);
            file.ModUserId = replaceUser.Id;

            // Assert
            Assert.AreEqual(replaceUser.Id, file.ModUserId);
        }

        [Test]
        public void ChangeModUserId_ThenLoadTheReference_ChangedEntityShouldBeLoaded()
        {
            // Arrange
            var context = UnitOfWork.Context<SampleContext>();
            var allUsers = context.Users.ToList();
            var findUser = allUsers[0];
            var replaceUser = allUsers[1];

            // Act
            var file = context.Files.First(x => x.ModUserId == findUser.Id);

            // Assert
            Assert.IsNotNull(file.ModUser);
            file.ModUserId = replaceUser.Id;

            var reference = context.Entry(file).Reference(x => x.ModUser);
            if (!reference.IsLoaded)
            {
                reference.Load();
            }

            Assert.AreEqual(replaceUser.Id, file.ModUser.Id);
        }

        [Test]
        public void GetAnyFileAndModifyAProperty_TheStateShouldBeModified()
        {
            // Arrange
            var context = UnitOfWork.Context<SampleContext>();
            var file = context.Files.First();

            // Act
            file.CreatedUtc = DateTime.UtcNow;
            var state = context.Entry(file).State;

            // Assert
            Assert.AreEqual(EntityState.Modified, state);
        }

        [Test]
        public void CreateANewFile_TheStateShouldBeDetached()
        {
            // Arrange
            var context = UnitOfWork.Context<SampleContext>();
            var file = context.Files.Create();

            // Act
            var state = context.Entry(file).State;

            // Assert
            Assert.AreEqual(EntityState.Detached, state);
        }

        [Test]
        public void CreateANewFileAndAddToContext_TheStateShouldBeAdded()
        {
            // Arrange
            var context = UnitOfWork.Context<SampleContext>();
            var file = context.Files.Create();

            // Act
            context.Files.Add(file);
            var state = context.Entry(file).State;

            // Assert
            Assert.AreEqual(EntityState.Added, state);
        }

        [Test]
        public void CreateANewFileAndAddToContext_LoadModUserReference()
        {
            // Arrange
            var context = UnitOfWork.Context<SampleContext>();
            context.Configuration.LazyLoadingEnabled = false;

            var file = context.Files.Create();
            var userId = context.Users.Select(x => x.Id).First();
            file.ModUserId = userId;

            // Act
            context.Files.Add(file); // NOTE: InvalidOperationException will occur when the file is not added to context

            // Assert
            Assert.IsNull(file.ModUser, "ModUser is not null.");

            var reference = context.Entry(file).Reference(x => x.ModUser);
            if (!reference.IsLoaded)
            {
                reference.Load();
            }

            Assert.IsNotNull(file.ModUser, "ModUser is not null.");
        }
    }
}