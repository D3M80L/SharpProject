using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitySamples.DataModel;
using EntitySamples.Infrastructure;

namespace Samples.Tests.TestsInfrastructure
{
    public sealed class DatabaseFill
    {
        public void Fill(SampleContext context)
        {
            var users = new List<User>()
            {
                new User() { Name  = "User 1" },
                new User() { Name  = "User 2" },
                new User() { Name  = "User 3" },
                new User() { Name  = "User 4" },
                new User() { Name  = "User 5" },
                new User() { Name  = "User 6" },
                new User() { Name  = "User 7" },
            };

            var customers = new List<Customer>()
            {
                new Customer() { Name  = "A" },
                new Customer() { Name  = "B" },
                new Customer() { Name  = "C" },
                new Customer() { Name  = "D" },
            };

            var files = new List<File>()
            {
                new File() { CreatedUtc  = DateTime.UtcNow, Title = "Title 1", Number = 1, ModUser = users[0], Customers = new List<Customer>(new [] { customers[0], customers[1] })},
                new File() { CreatedUtc  = DateTime.UtcNow, Title = "Title 2", Number = 2, ModUser = users[1], Customers = new List<Customer>(new [] { customers[2], customers[3] })},
                new File() { CreatedUtc  = DateTime.UtcNow, Title = "Title 3", Number = 3, ModUser = users[2], Customers = new List<Customer>(new [] { customers[3] })},
                new File() { CreatedUtc  = DateTime.UtcNow, Title = "Title 4", Number = 4, ModUser = users[3] },
                new File() { CreatedUtc  = DateTime.UtcNow, Title = "Title 5", Number = 4, ModUser = users[3] },
            };

            var fileNotes = new List<FileNote>()
            {
                new FileNote() { Note  = "Note 1" },
                new FileNote() { Note  = "Note 2" },
                new FileNote() { Note  = "Note 3" },
                new FileNote() { Note  = "Note 4" },
                new FileNote() { Note  = "Note 4 deleted", IsDeleted = true },
                new FileNote() { Note  = "Note 3 deleted", IsDeleted = true },
            };
            files[0].FileNotes = fileNotes;

            // Act
            users.ForEach(user => context.Users.Add(user));
            customers.ForEach(customer => context.Customers.Add(customer));
            files.ForEach(file => context.Files.Add(file));
        }
    }
}
