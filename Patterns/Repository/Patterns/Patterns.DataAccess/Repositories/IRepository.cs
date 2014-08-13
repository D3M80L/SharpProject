namespace Patterns.DataAccess.Repositories
{
    public interface IRepository
    {
        
    }

    public interface IRepository<TModel> : IRepository
        where TModel : class
    {

    }
}