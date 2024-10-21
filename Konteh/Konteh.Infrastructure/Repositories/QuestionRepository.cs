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
        var query = GetBaseQuery(questionText);

        query = FilterNonDeletedItems(query);

        var totalCount = await GetTotalCount(query);

        var items = await GetPagedItems(query, page, pageSize);

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

    private IQueryable<Question> FilterNonDeletedItems(IQueryable<Question> query)
    {
        return query.Where(q => q.IsDeleted == false);
    }

    private async Task<int> GetTotalCount(IQueryable<Question> query)
    {
        return await query.CountAsync();
    }

    private async Task<List<Question>> GetPagedItems(IQueryable<Question> query, int page, float pageSize)
    {
        return await query
            .Skip((page - 1) * (int)pageSize)
            .Take((int)pageSize)
            .ToListAsync();
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
