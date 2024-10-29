using Konteh.Domain;
using Konteh.Domain.Enumerations;
using Konteh.Infrastructure.Repositories;
using MediatR;

namespace Konteh.BackOfficeApi.Features.Exams;

public static class GetAllExams
{
    public class Query : IRequest<IEnumerable<Response>>
    {
        public string? Candidate { get; set; } = string.Empty;

    }

    public class Response
    {
        public long Id { get; set; }
        public string Candidate { get; set; } = string.Empty!;
        public ExamStatus Status { get; set; }
        public int Score { get; set; }
        public int MaxScore { get; set; }
    }

    public class RequestHandler : IRequestHandler<Query, IEnumerable<Response>>
    {
        private readonly IRepository<Exam> _examRepository;

        public RequestHandler(IRepository<Exam> examRepository)
        {
            _examRepository = examRepository;
        }

        public async Task<IEnumerable<Response>> Handle(Query request, CancellationToken cancellationToken)
        {
            var exams = await _examRepository.Search(e =>
            string.IsNullOrEmpty(request.Candidate)
            || e.Candiate.Name.Contains(request.Candidate)
            || e.Candiate.Surname.Contains(request.Candidate));

            return exams.Select(e => new Response
            {
                Id = e.Id,
                Candidate = $"{e.Candiate.Name} {e.Candiate.Surname}",
                Status = e.Status,
                Score = e.ExamQuestions.Count(ea => ea.IsCorrect()),
                MaxScore = e.ExamQuestions.Count
            });
        }
    }
}