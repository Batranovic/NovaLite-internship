using Konteh.Domain;
using Microsoft.EntityFrameworkCore;

namespace Konteh.Infrastructure.Repositories;

public class ExamRepository : BaseRepository<Exam>
{
    public ExamRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Exam>> GetAll()
    {
        return await _context.Exams
                         .Include(e => e.Candiate)
                         .Include(e => e.ExamQuestions)
                         .ToListAsync();
    }
}