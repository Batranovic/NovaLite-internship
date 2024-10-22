using Konteh.Domain;
using Konteh.Domain.Enumerations;
using Konteh.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Konteh.BackOfficeApi.Features.Questions
{
    public static class CreateUpdateQuestion
    {
        public class Command : IRequest<Response>
        {
            public long Id { get; set; }
            public string Text { get; set; } = string.Empty;
            public QuestionCategory Category { get; set; }
            public QuestionType Type { get; set; }
            public List<AnswerDto> Answers { get; set; } = [];
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

        public class RequestHandler : IRequestHandler<Command, Response>
        {
            private readonly IRepository<Question> _repository;

            public RequestHandler(IRepository<Question> repository)
            {
                _repository = repository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var existingQuestion = await _repository.Query()
                    .Include(q => q.Answers)
                    .FirstOrDefaultAsync(q => q.Id == request.Id);

                if (existingQuestion == null)
                {
                    return await CreateQuestion(request);
                }

                existingQuestion.Text = request.Text;
                existingQuestion.Category = request.Category;
                existingQuestion.Type = request.Type;

                foreach (var answer in request.Answers)
                {
                    var existingAnswer = existingQuestion.Answers
                        .FirstOrDefault(a => a.Id == answer.Id);

                    if (existingAnswer != null)
                    {
                        existingAnswer.Text = answer.Text;
                        existingAnswer.IsCorrect = answer.IsCorrect;
                        existingAnswer.IsDeleted = answer.IsDeleted;
                    }
                    else
                    {
                        existingQuestion.Answers.Add(new Answer
                        {
                            Text = answer.Text,
                            IsCorrect = answer.IsCorrect,
                            IsDeleted = answer.IsDeleted
                        });
                    }
                }

                await _repository.SaveChanges();

                return new Response
                {
                    Id = existingQuestion.Id,
                    Text = existingQuestion.Text,
                    Type = existingQuestion.Type,
                    Answers = existingQuestion.Answers.Select(a => new AnswerDto
                    {
                        Id = a.Id,
                        Text = a.Text,
                        IsCorrect = a.IsCorrect,
                        IsDeleted = a.IsDeleted,
                    }).ToList(),
                };
            }

            private async Task<Response> CreateQuestion(Command request)
            {
                var question = new Question
                {
                    Text = request.Text,
                    Category = request.Category,
                    Type = request.Type,
                    Answers = request.Answers.Select(a => new Answer
                    {
                        Text = a.Text,
                        IsCorrect = a.IsCorrect,
                        IsDeleted = false,
                    }).ToList()
                };

                _repository.Create(question);
                await _repository.SaveChanges();
                return new Response
                {
                    Id = question.Id,
                    Text = question.Text,
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
