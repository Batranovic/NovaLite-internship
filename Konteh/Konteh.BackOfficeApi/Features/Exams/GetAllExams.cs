using Konteh.Domain;
using Konteh.Domain.Enumerations;
using Konteh.Infrastructure.Repositories;
using MediatR;

namespace Konteh.BackOfficeApi.Features.Exams;

public static class GetAllExams
{
    public class Query : IRequest<IEnumerable<Response>>;

    public class Response
    {
        public long Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Candidate Candidate { get; set; } = null!;
        public ExamStatus Status { get; set; }
        public double Score { get; set; } = 0;
    }

    public class RequestHandler : IRequestHandler<Query, IEnumerable<Response>>
    {
        private readonly IRepository<Exam> _repository;

        public RequestHandler(IRepository<Exam> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Response>> Handle(Query request, CancellationToken cancellationToken)
        {
            var exams = await _repository.GetAll();
            return exams.Select(e => new Response { Id = e.Id, StartTime = e.StartTime, EndTime = e.EndTime, Candidate = e.Candiate, Status = e.Status, Score = e.Score });
        }
    }

}
