using Konteh.Domain;
using Microsoft.EntityFrameworkCore;

namespace Konteh.Infrastructure.Repositories;

public class QuestionRepository : BaseRepository<Question>
{
    private AppDbContext _context;
    public QuestionRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<(IEnumerable<Question>, int)> PaginateItems(int page, float pageSize)
    {
        if (_context.Set<Question>() == null)
        {
            return (new List<Question>(), 0);
        }

        var pageCount = Math.Ceiling(_context.Set<Question>().Count() / pageSize);

        var items = await _context.Set<Question>()
            .Skip((page - 1) * (int)pageSize)
            .Take((int)pageSize).ToListAsync();

        return (items, (int)pageCount);
    }

}
