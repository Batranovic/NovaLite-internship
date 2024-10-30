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
        private readonly IRepository<Exam> _repository;

        public RequestHandler(IRepository<Exam> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetExamResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var searchTerms = request.Candidate?.Trim().Split(' ') ?? [];
            if (searchTerms.Length == 2)
            {
                var firstName = searchTerms[0];
                var lastName = searchTerms[1];
                var searchedExams = await _repository.Search(e => string.IsNullOrEmpty(request.Candidate) || (e.Candiate.Name.Contains(firstName) && e.Candiate.Surname.Contains(lastName)));
                return searchedExams.Select(e => new GetExamResponse { Id = e.Id, Candidate = $"{e.Candiate.Name} {e.Candiate.Surname}", Status = e.Status, Score = CalculateScore(e) });
            }
            var exams = await _repository.Search(e => string.IsNullOrEmpty(request.Candidate) || e.Candiate.Name.Contains(request.Candidate) || e.Candiate.Surname.Contains(request.Candidate));
            return exams.Select(e => new GetExamResponse { Id = e.Id, Candidate = $"{e.Candiate.Name} {e.Candiate.Surname}", Status = e.Status, Score = CalculateScore(e) });
        }

        private double CalculateScore(Exam exam)
        {
            return 0 / exam.ExamQuestions.Count;
        }
    }

}
