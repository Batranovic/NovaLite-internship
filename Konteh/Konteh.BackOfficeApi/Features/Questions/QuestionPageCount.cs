using Konteh.Domain.Enumerations;
using Konteh.Domain;
using Konteh.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Konteh.BackOfficeApi.Features.Questions
{
    public class QuestionPageCount
    {
        public class Query : IRequest<Response>
        {
            public Query(float pageSize, string? questionText)
            {
                PageSize = pageSize;
                QuestionText = questionText;

            }
            
            public string? QuestionText { get; set; }
            public float PageSize { get; set; }
        }

        public class Response
        {
            public int PageCount { get; set; }
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
                var pageCount = await _repository.GetPageCount(request.PageSize, prepareFilter(request));

                return new Response { PageCount = pageCount };
            }

            private Expression<Func<Question, bool>>? prepareFilter(Query request)
            {
                if (request.QuestionText == null)
                {
                    return null;
                }
                return q => q.Text.Contains(request.QuestionText);
            }
        }
    }
}
