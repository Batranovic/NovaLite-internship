using Konteh.Domain;
using System.Linq.Expressions;

namespace Konteh.Infrastructure.Repositories;

public interface IQuestionRepository : IRepository<Question>
{
    Task<(IEnumerable<Question> SearchedQuestions, int QuestionCount)> PaginateItems(int page, int pageSize, string? questionText = null);
    IQueryable<Question> SearchIQueryable(Expression<Func<Question, bool>> predicate);
}
