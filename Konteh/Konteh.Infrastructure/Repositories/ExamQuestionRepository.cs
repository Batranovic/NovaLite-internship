using Konteh.Domain;

namespace Konteh.Infrastructure.Repositories;

public class ExamQuestionRepository : BaseRepository<ExamQuestion>
{
    public ExamQuestionRepository(AppDbContext context) : base(context)
    {
    }
}
