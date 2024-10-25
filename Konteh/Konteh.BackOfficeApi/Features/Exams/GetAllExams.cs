using Konteh.Domain;
using Konteh.Domain.Enumerations;
using Konteh.Infrastructure.Repositories;
using MediatR;

namespace Konteh.BackOfficeApi.Features.Exams;

public static class GetAllExams
{
    public class Query : IRequest<Response>;

    public class Response
    {
        public IEnumerable<ResponseItem> Items { get; set; } = [];
    }
    public class ResponseItem
    {
        public long Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Candidate { get; set; } = string.Empty!;
        public ExamStatus Status { get; set; }
        public double Score { get; set; }
    }

    public class RequestHandler : IRequestHandler<Query, Response>
    {
        private readonly IRepository<Exam> _repository;

        public RequestHandler(IRepository<Exam> repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var exams = await _repository.GetAll();
            return new Response
            {
                Items = exams.Select(e => new ResponseItem
                {
                    Id = e.Id,
                    StartTime = e.StartTime,
                    EndTime = e.EndTime,
                    Candidate = $"{e.Candiate.Name} {e.Candiate.Surname}, {e.Candiate.Faculty}",
                    Status = e.Status,
                    Score = CalculateScore(e)
                })
            };
        }

        private double CalculateScore(Exam exam)
        {
            return 0 / exam.ExamQuestions.Count;
        }
    }

}
