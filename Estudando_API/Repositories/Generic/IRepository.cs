using System.Linq.Expressions;

namespace Estudando_API.Repositories.Generic
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(Expression<Func<T, bool>> precidate);
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
