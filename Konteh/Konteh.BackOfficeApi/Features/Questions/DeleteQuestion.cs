using Konteh.Infrastructure.Repositories;
using MediatR;

namespace Konteh.BackOfficeApi.Features.Questions
{
    public static class DeleteQuestion
    {
        public class Command : IRequest<bool>
        {
            public long Id { get; set; }
        }

        public class RequestHandler : IRequestHandler<Command, bool>
        {
            private readonly IQuestionRepository _repository;

            public RequestHandler(IQuestionRepository repository)
            {
                _repository = repository;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _repository.Delete(request.Id);
            }
        }
    }
}
