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
                            .AsQueryable();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync();
    }


}