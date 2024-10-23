using FluentValidation;
using Konteh.Domain;
using Konteh.Domain.Enumerations;
using Konteh.Infrastructure.Repositories;
using MediatR;

namespace Konteh.BackOfficeApi.Features.Questions;

public static class CreateUpdateQuestion
{
    public class Command : IRequest<Unit>
    {
        public long? Id { get; set; } = null;
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

    public class RequestHandler : IRequestHandler<Command, Unit>
    {
        private readonly IRepository<Question> _repository;
        private readonly IValidator<Command> _validator;

        public RequestHandler(IRepository<Question> repository, IValidator<Command> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            if (request.Id == null)
            {
                CreateQuestion(request);
            }
            else
            {
                await UpdateQuestion(request);
            }

            await _repository.SaveChanges();
            return Unit.Value;
        }

        private async Task UpdateQuestion(Command request)
        {
            var existingQuestion = await _repository.GetById(request.Id!.Value)
                ?? throw new KeyNotFoundException($"Question with ID {request.Id} not found.");
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
        }

        private void CreateQuestion(Command request)
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
        }
    }
}
