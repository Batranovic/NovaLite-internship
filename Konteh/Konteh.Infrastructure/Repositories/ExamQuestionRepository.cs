using Konteh.Domain;
using Microsoft.EntityFrameworkCore;

namespace Konteh.Infrastructure.Repositories;

public class ExamQuestionRepository : BaseRepository<ExamQuestion>, IExamQuestionRepository
{
    public ExamQuestionRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<ExamQuestion>> GetByIds(IEnumerable<long> ids)
    {
        return await _context.Set<ExamQuestion>()
                             .Where(eq => ids.Contains(eq.Id))
                             .ToListAsync();
    }

}
