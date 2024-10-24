using Konteh.Domain;

namespace Konteh.Infrastructure.Repositories;
public class AnswerRepository : BaseRepository<Answer>
{
    public AnswerRepository(AppDbContext context) : base(context)
    {
    }
}
