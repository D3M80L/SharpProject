using System.Data.Entity;
using EntitySamples.Infrastructure;
using NUnit.Framework;

namespace Samples.Tests.TestsInfrastructure
{
    public abstract class UnitOfWorkTestBase
    {
        protected IUnitOfWork UnitOfWork;

        static UnitOfWorkTestBase()
        {
            Database.SetInitializer(new SampleContextDropAndCreateDatabase());
        }

        [SetUp]
        public void SetUp()
        {
            UnitOfWork = new UnitOfWork();
            UnitOfWork.Context<SampleContext>().Database.Initialize(force: true);
        }

        [TearDown]
        public void TearDown()
        {
            UnitOfWork.Dispose();
            UnitOfWork = null;
        }
    }
}