using Konteh.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Konteh.Infrastructure.Repositories;

public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
{
    private AppDbContext _context;
    public QuestionRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<(IEnumerable<Question>, int)> PaginateItems(int page, float pageSize, string? questionText = null)
    {
        var query = _context.Set<Question>().AsQueryable();

        var filter = prepareFilter(questionText);
        if (filter != null)
        {
            query = query.Where(filter);
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * (int)pageSize)
            .Take((int)pageSize)
            .ToListAsync();

        return (items, (int)totalCount);
    }

    private Expression<Func<Question, bool>>? prepareFilter(string? questionText)
    {
        if (questionText == null)
        {
            return null;
        }
        return q => q.Text.Contains(questionText);
    }

}
