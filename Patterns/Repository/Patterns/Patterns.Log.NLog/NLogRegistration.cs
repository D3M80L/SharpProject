using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Patterns.Log.NLog.Infrastructure;

namespace Patterns.Log.NLog
{
    public sealed class NLogRegistration : NLogRegistrationBase
    {
        public NLogRegistration() : base(new NLogAdapter())
        {
        }
    }
}
