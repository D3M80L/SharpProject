using System.Collections.Generic;

namespace EntitySamples.DataModel
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}