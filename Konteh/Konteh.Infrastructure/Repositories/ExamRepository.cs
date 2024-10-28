using Konteh.Domain;
using Microsoft.EntityFrameworkCore;

namespace Konteh.Infrastructure.Repositories;

public class ExamRepository : BaseRepository<Exam>
{
    public ExamRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<Exam?> GetById(long id) => await _context.Set<Exam>()
        .Include(x => x.ExamQuestions)
        .ThenInclude(x => x.Question.Answers)
        .SingleOrDefaultAsync(x => x.Id == id);
}