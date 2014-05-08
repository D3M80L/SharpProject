using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitySamples.DataModel
{
    public class File
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedUtc { get; set; }

        public int Number { get; set; }

        public int ModUserId { get; set; }

        public virtual User ModUser { get; set; }

        public DateTime? FileDate { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

        public virtual ICollection<FileNote> FileNotes { get; set; }
    }
}