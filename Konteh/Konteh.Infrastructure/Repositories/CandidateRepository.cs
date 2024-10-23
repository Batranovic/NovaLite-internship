using Konteh.Domain;

namespace Konteh.Infrastructure.Repositories;
public class CandidateRepository : BaseRepository<Candidate>
{
    public CandidateRepository(AppDbContext context) : base(context)
    {
    }
}
