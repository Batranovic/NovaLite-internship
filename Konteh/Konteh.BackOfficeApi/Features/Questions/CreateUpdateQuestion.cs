using Konteh.Domain;
using Konteh.Domain.Commands;
using Konteh.Infrastructure.ExceptionHandlers.Exceptions;
using Konteh.Infrastructure.Repositories;
using MediatR;

namespace Konteh.BackOfficeApi.Features.Questions;

public static class CreateUpdateQuestion
{
    public class RequestHandler : IRequestHandler<CreateOrUpdateQuestionCommand, Unit>
    {
        private readonly IRepository<Question> _repository;

        public RequestHandler(IRepository<Question> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateOrUpdateQuestionCommand request, CancellationToken cancellationToken)
        {
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

        private async Task UpdateQuestion(CreateOrUpdateQuestionCommand request)
        {
            var existingQuestion = await _repository.GetById(request.Id!.Value) ?? throw new NotFoundException();
            existingQuestion.Text = request.Text;
            existingQuestion.Category = request.Category;
            //TODO: Think about a way to do this
            //existingQuestion.Type = request.Type;
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
                        IsCorrect = answer.IsCorrect
                    });
                }
            }
        }

        private void CreateQuestion(CreateOrUpdateQuestionCommand request)
        {
            var question = Question.Create(request);

            _repository.Create(question);
        }
    }
}
