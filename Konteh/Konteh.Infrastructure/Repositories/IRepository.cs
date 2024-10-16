using System.Linq.Expressions;

namespace Konteh.Infrastructure.Repositories;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(long id);
    void Create(T entity);
    void Delete(T entity);
    Task SaveChanges();
    Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate);
    IQueryable<T> Query();
}
