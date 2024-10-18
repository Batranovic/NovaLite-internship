using Konteh.Domain;

namespace Konteh.Infrastructure.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<(IEnumerable<Question>, int)> PaginateItems(int page, float pageSize, string? questionText = null);
    }
}
