namespace Patterns.DataAccess
{
    public interface IRepository
    {
        
    }

    public interface IRepository<TModel> : IRepository
        where TModel : class
    {

    }
}