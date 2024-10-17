using Konteh.Domain;
using Konteh.Domain.Enumerations;
using Konteh.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Konteh.BackOfficeApi.Features.Questions
{
    public class GetQuestionById
    {
        public class Query : IRequest<Response>
        {
            public long Id { get; set; }
        }
        public class Response
        {
            public long Id { get; set; }
            public string Text { get; set; } = string.Empty;
            public QuestionCategory Category { get; set; }
            public QuestionType Type { get; set; }
            public List<Answer> Answers { get; set; } = [];
        }

        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly IRepository<Question> _repository;

            public RequestHandler(IRepository<Question> repository)
            {
                _repository = repository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var question = await _repository.Query()
                    .Include(q => q.Answers.Where(a => !a.IsDeleted))
                    .FirstOrDefaultAsync(q => q.Id == request.Id);

                if (question == null)
                {
                    throw new Exception("Question not found.");
                }

                return new Response
                {
                    Id = question.Id,
                    Text = question.Text,
                    Category = question.Category,
                    Type = question.Type,
                    Answers = question.Answers,
                };
            }
        }
    }
}
