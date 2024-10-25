using Konteh.Domain;

namespace Konteh.Infrastructure.Repositories;
public interface IExamQuestionRepository : IRepository<ExamQuestion>
{
    Task<List<ExamQuestion>> GetByIds(IEnumerable<long> ids);
}
