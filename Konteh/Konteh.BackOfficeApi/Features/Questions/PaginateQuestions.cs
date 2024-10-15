using Konteh.Domain.Enumerations;
using Konteh.Domain;
using Konteh.Infrastructure.Repositories;
using MediatR;

namespace Konteh.BackOfficeApi.Features.Questions
{
    public class PaginateQuestions
    {
        public class Query : IRequest<IEnumerable<Response>>
        {
            public Query(int page, float pageSize)
            {
                Page = page;
                PageSize = pageSize;
            }

            public int Page { get; set; }
            public float PageSize { get; set; }
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
                var items = await _repository.PaginateItems(request.Page, request.PageSize);

                return items.Select(q => new Response { Id = q.Id, Category = q.Category, Text = q.Text});
            }
        }
    }
}
