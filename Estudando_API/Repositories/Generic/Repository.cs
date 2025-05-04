using Estudando_API.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Estudando_API.Repositories.Generic
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsTracking().ToList();
        }

        public T? Get(System.Linq.Expressions.Expression<Func<T, bool>> precidate)
        {
            return _context.Set<T>().FirstOrDefault(precidate);
        }

        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();

            return entity;
        }

        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();

            return entity;
        }
    }
}
