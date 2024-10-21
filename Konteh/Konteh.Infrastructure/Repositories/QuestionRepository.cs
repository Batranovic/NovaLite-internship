using Konteh.Domain;
using Microsoft.EntityFrameworkCore;

namespace Konteh.Infrastructure.Repositories;

public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
{
    public QuestionRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<(IEnumerable<Question> SearchedQuestions, int QuestionCount)> PaginateItems(int page, int pageSize, string? questionText)
    {
        var query = _context.Set<Question>().Where(q => q.IsDeleted == false);

        if (!string.IsNullOrEmpty(questionText))
        {
            query = query.Where(q => q.Text.Contains(questionText));
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }


    public override void Delete(Question entity)
    {
        entity.IsDeleted = true;
    }
}
