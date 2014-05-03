using System.Linq;
using EntitySamples.Infrastructure;
using NUnit.Framework;
using Samples.Tests.TestsInfrastructure;

namespace Samples.Tests
{
    [TestFixture]
    public class UpdateExamples : UnitOfWorkTestBase
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
        public void ChangeModUserByEntityWithDisabledLazyLoading()
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
    }
}