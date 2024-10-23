using Konteh.Domain;
using Konteh.Domain.Enumerations;
using Konteh.Infrastructure.Repositories;
using MediatR;

namespace Konteh.BackOfficeApi.Features.Questions
{
    public static class GetQuestionById
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
            public List<AnswerDto> Answers { get; set; } = [];
        }

        public class AnswerDto
        {
            public long Id { get; set; }
            public string Text { get; set; } = string.Empty;
            public bool IsCorrect { get; set; }
            public bool IsDeleted { get; set; }
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
                var question = await _repository.GetById(request.Id);

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
                    Answers = question.Answers.Select(a => new AnswerDto
                    {
                        Id = a.Id,
                        Text = a.Text,
                        IsCorrect = a.IsCorrect,
                        IsDeleted = a.IsDeleted,
                    }).ToList(),
                };
            }
        }
    }
}
