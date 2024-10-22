using Konteh.Domain;

namespace Konteh.Infrastructure.Repositories;

public class QuestionRepository : BaseRepository<Question>
{

    public QuestionRepository(AppDbContext context) : base(context)
    {
    }
}