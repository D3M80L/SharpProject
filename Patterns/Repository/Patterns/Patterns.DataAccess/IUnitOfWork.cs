using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.DataAccess
{
    public interface IUnitOfWork
    {
        TContext Context<TContext>()
            where TContext : class;

        void Save();
    }
}
