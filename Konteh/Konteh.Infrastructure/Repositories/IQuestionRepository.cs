using Konteh.Domain;

namespace Konteh.Infrastructure.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<(IEnumerable<Question> SearchedQuestions, int QuestionCount)> PaginateItems(int page, float pageSize, string? questionText = null);
    }
}
