using Konteh.Domain;
using Konteh.Infrastructure.DTO;
using Konteh.Infrastructure.Repositories;
using MediatR;

namespace Konteh.BackOfficeApi.Features.Exams;

public static class GetAllExams
{
    public class Query : IRequest<IEnumerable<GetExamResponse>>
    {
        public string? Candidate { get; set; } = string.Empty;
    }


    public class RequestHandler : IRequestHandler<Query, IEnumerable<GetExamResponse>>
    {
        private readonly IRepository<Exam> _examRepository;

        public RequestHandler(IRepository<Exam> examRepository)
        {
            _examRepository = examRepository;
        }

        public async Task<IEnumerable<GetExamResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var exams = await _examRepository.Search(e =>
            string.IsNullOrEmpty(request.Candidate)
            || e.Candiate.Name.Contains(request.Candidate)
            || e.Candiate.Surname.Contains(request.Candidate));

            return exams.Select(e => new GetExamResponse
            {
                Id = e.Id,
                Candidate = $"{e.Candiate.Name} {e.Candiate.Surname}",
                Status = e.Status,
                Score = $"{e.ExamQuestions.Count(ea => ea.IsCorrect())} / {e.ExamQuestions.Count}",
            });
        }
    }
}