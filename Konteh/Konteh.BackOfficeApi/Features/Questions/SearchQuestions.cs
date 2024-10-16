using Konteh.Domain;
using Konteh.Domain.Enumerations;
using Konteh.Infrastructure.Repositories;
using MediatR;

namespace Konteh.BackOfficeApi.Features.Questions
{
    public class SearchQuestions
    {
        public class Query : IRequest<IEnumerable<Response>>
        {
            public Query(string questionText)
            {
                QuestionText = questionText;
            }

            public string QuestionText { get; }
        }

    public class Response
        {
            public long Id { get; set; }
            public string Text { get; set; } = string.Empty;
            public QuestionCategory Category { get; set; }
        }

        public class RequestHandler : IRequestHandler<Query, IEnumerable<Response>>
        {
            private readonly IRepository<Question> _repository;

            public RequestHandler(IRepository<Question> repository)
            {
                _repository = repository;
            }

            public async Task<IEnumerable<Response>> Handle(Query request, CancellationToken cancellationToken)
            {
                var questions = await _repository.Search(q => q.Text.Contains(request.QuestionText));
                return questions.Select(q => new Response { Id = q.Id, Category = q.Category, Text = q.Text });
            }
        }

    }
}
