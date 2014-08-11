using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.DataAccess
{
    /// <summary>
    /// A simple example of an generic Unit Of Work pattern
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Get a specific instance of registered context
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <returns>Instance of specific context</returns>
        TContext Context<TContext>()
            where TContext : class;

        /// <summary>
        /// Perform Commit/Save operation on all registered cotextes in UnitOfWork
        /// </summary>
        void Save();
    }
}
