using Konteh.Domain;
using Microsoft.EntityFrameworkCore;

namespace Konteh.Infrastructure.Repositories;

public class ExamQuestionRepository : BaseRepository<ExamQuestion>
{
    public ExamQuestionRepository(AppDbContext context) : base(context)
    {
    }

    public override IEnumerable<ExamQuestion> GetByIds(List<long> ids)
    {
        return _context.Set<ExamQuestion>()
                       .Include(eq => eq.Question)                
                       .ThenInclude(q => q.Answers)              
                       .Where(eq => ids.Contains(eq.Id))         
                       .ToList();                                 
    }

}
