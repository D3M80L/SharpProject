using System.Data.Entity;
using EntitySamples.DataModel;

namespace EntitySamples.Infrastructure
{
    public sealed class SampleContext : DbContext
    {
        public DbSet<File> Files { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Customer> Customers { get; set; }
    }
}