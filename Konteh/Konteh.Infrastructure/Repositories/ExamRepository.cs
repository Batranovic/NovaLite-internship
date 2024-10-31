using Konteh.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Konteh.Infrastructure.Repositories;

public class ExamRepository : BaseRepository<Exam>
{
    public ExamRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Exam>> Search(Expression<Func<Exam, bool>> predicate)
    {
        var query = _context.Exams
                            .Include(e => e.Candiate)
                            .Include(e => e.ExamQuestions)
                                .ThenInclude(eq => eq.SubmittedAnswers)
                             .Include(e => e.ExamQuestions)
                                .ThenInclude(eq => eq.Question.Answers)
                            .AsQueryable();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync();
    }


    public override async Task<Exam?> GetById(long id) => await _context.Set<Exam>()
        .Include(x => x.ExamQuestions)
        .ThenInclude(x => x.Question.Answers)
        .Include(x => x.ExamQuestions)
        .ThenInclude(x => x.SubmittedAnswers)
        .Include(x => x.Candiate)
        .SingleOrDefaultAsync(x => x.Id == id);

    public override async Task<IEnumerable<Exam>> GetAll() => await _context.Set<Exam>()
        .Include(e => e.ExamQuestions)
        .ThenInclude(eq => eq.SubmittedAnswers)
        .Include(e => e.ExamQuestions)
        .ThenInclude(eq => eq.Question.Answers)
        .ToListAsync();

}