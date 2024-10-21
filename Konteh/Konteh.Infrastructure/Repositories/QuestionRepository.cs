using Konteh.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Konteh.Infrastructure.Repositories;

public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
{
    public QuestionRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<(IEnumerable<Question> SearchedQuestions, int QuestionCount)> PaginateItems(int page, float pageSize, string? questionText = null)
    {
        var query = _context.Set<Question>().AsQueryable();

        if (!string.IsNullOrEmpty(questionText))
        {
            query = query.Where(q => q.Text.Contains(questionText));
        }

        query = query.Where(q => q.IsDeleted == false);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * (int)pageSize)
            .Take((int)pageSize)
            .ToListAsync();
        return (items, totalCount);
    }


    public new async Task<bool> Delete(long questionId)
    {
        var question = await GetById(questionId);
        if (question != null)
        {
            question.IsDeleted = true;
            await SaveChanges();
            return true;
        }
        return false;
    }

    private IQueryable<Question> GetBaseQuery(string? questionText)
    {
        var query = _context.Set<Question>().AsQueryable();

        var filter = PrepareFilter(questionText);
        if (filter != null)
        {
            query = query.Where(filter);
        }

        return query;
    }

    private Expression<Func<Question, bool>>? PrepareFilter(string? questionText)
    {
        if (string.IsNullOrEmpty(questionText))
        {
            return null;
        }

        return q => q.Text.Contains(questionText);
    }
}
