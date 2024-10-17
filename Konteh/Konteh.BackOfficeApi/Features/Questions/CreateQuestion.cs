using Konteh.Domain;
using Konteh.Domain.Enumerations;
using Konteh.Infrastructure.Repositories;
using MediatR;

namespace Konteh.BackOfficeApi.Features.Questions
{
    public class CreateQuestion
    {
        public class Command : IRequest<Response>
        {
            public string Text { get; set; } = string.Empty;
            public QuestionCategory Category { get; set; }
            public QuestionType QuestionType { get; set; }
            public List<Answer> Answers { get; set; } = [];
        }


        public class Response
        {
            public long Id { get; set; }
        }

        public class RequestHandler : IRequestHandler<Command, Response>
        {
            private readonly IRepository<Question> _questionsRepository;

            public RequestHandler(IRepository<Question> questionsRepository)
            {
                _questionsRepository = questionsRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var question = new Question
                {
                    Text = request.Text,
                    Category = request.Category,
                    Type = request.QuestionType,
                    Answers = request.Answers.Select(a => new Answer
                    {
                        Text = a.Text,
                        IsCorrect = a.IsCorrect,
                        IsDeleted = false,

                    }).ToList()
                };

                _questionsRepository.Create(question);
                await _questionsRepository.SaveChanges();

                return new Response
                {
                    Id = question.Id
                };

            }
        }
    }
}
