using Konteh.Infrastructure.ExceptionHandlers.Exceptions;
using Konteh.Infrastructure.Repositories;
using MediatR;

namespace Konteh.BackOfficeApi.Features.Questions;

public static class DeleteQuestion
{
    public class Command : IRequest
    {
        public long Id { get; set; }
    }

    public class RequestHandler : IRequestHandler<Command>
    {
        private readonly IQuestionRepository _repository;

        public RequestHandler(IQuestionRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var question = await _repository.GetById(request.Id) ?? throw new NotFoundException();
            _repository.Delete(question);
            await _repository.SaveChanges();
        }
    }
}
