using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.DataAccess.Tests.TestData
{
    public class ContextMock
    {
        public virtual void Save() { }

        public virtual void Dispose() { }
    }
}
