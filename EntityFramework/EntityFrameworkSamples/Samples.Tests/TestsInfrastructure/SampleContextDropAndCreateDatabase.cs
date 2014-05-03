using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitySamples.Infrastructure;

namespace Samples.Tests.TestsInfrastructure
{
    /// <summary>
    /// http://stackoverflow.com/questions/5288996/database-in-use-error-with-entity-framework-4-code-first
    /// </summary>
    public sealed class SampleContextDropAndCreateDatabase : IDatabaseInitializer<SampleContext>
    {
        public void InitializeDatabase(SampleContext context)
        {
            if (context.Database.Exists())
            {
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "ALTER DATABASE [" + context.Database.Connection.Database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "USE master DROP DATABASE [" + context.Database.Connection.Database + "]");
            }

            context.Database.Create();

            Seed(context);
        }

        private void Seed(SampleContext context)
        {
            (new DatabaseFill()).Fill(context);
            context.SaveChanges();
        }
    }
}
