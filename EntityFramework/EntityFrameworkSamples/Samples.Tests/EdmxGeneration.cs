using System.Data.Entity.Infrastructure;
using System.Text;
using System.Xml;
using EntitySamples.Infrastructure;
using NUnit.Framework;
using Samples.Tests.TestsInfrastructure;

namespace Samples.Tests
{
    [TestFixture]
    public sealed class EdmxGeneration : UnitOfWorkTestBase
    {
        [Test]
        public void GenerateEdmxFromCodeFirstContext()
        {
            // Arrange
            const string filePath = @".\Model.edmx";
            var context = UnitOfWork.Context<SampleContext>();

            // Act
            using (var writer = new XmlTextWriter(filePath, Encoding.Default))
            {
                EdmxWriter.WriteEdmx(context, writer);
            }

            // Assert   
            Assert.IsTrue(System.IO.File.Exists(filePath));
        }
    }
}