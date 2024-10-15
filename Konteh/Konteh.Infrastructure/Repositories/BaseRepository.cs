using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Konteh.Infrastructure.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Create(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>> PaginateItems(int page, float pageSize)
    {
        if (_context.Set<T>() == null)
        {
            return new List<T>();
        }

        var totalCount = _context.Set<T>().Count();
        var pageCount = Math.Ceiling(totalCount / pageSize);


        var items = await _context.Set<T>()
            .Skip((page - 1) * (int)pageSize)
            .Take((int)pageSize)
            .ToListAsync();

        return items;
    }

    public async Task<int> GetPageCount(float pageSize)
    {
        if (_context.Set<T>() == null || pageSize <= 0)
        {
            return 0; 
        }

        var totalCount = await _context.Set<T>().CountAsync();
        return (int)Math.Ceiling(totalCount / pageSize);
    }

    public async Task<T?> GetById(long id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task SaveChanges() => await _context.SaveChangesAsync();

    public async Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().Where(predicate).ToListAsync();
    }

 

    
}
