using Konteh.Domain;
using Konteh.Infrastructure.Repositories;
using MediatR;

namespace Konteh.BackOfficeApi.Features.Questions
{
    public static class DeleteQuestion
    {
        public class Query : IRequest<Response>
        {
            public long Id { get; set; }
        }

        public class Response
        {
            public bool Success { get; set; }
        }

        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly IQuestionRepository _repository;

            public RequestHandler(IQuestionRepository repository)
            {
                _repository = repository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                bool success = await _repository.Delete(new Question { Id = request.Id });
                return new Response() { Success = success };
            }
        }

    }
}
