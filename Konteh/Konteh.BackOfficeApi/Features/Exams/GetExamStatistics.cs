using Konteh.Domain;
using Konteh.Infrastructure.Repositories;
using MediatR;

namespace Konteh.BackOfficeApi.Features.Exams;

public static class GetExamStatistics
{
    public class StatisticsQuery : IRequest<ExamStatistics>;

    public class ExamStatistics
    {
        public double Over50Percent { get; set; }
        public double Under50Percent { get; set; }
    }

    public class RequestHandler : IRequestHandler<StatisticsQuery, ExamStatistics>
    {
        private readonly IRepository<Exam> _examRepository;
        public RequestHandler(IRepository<Exam> examRepository)
        {
            _examRepository = examRepository;
        }
        public async Task<ExamStatistics> Handle(StatisticsQuery request, CancellationToken cancellationToken)
        {
            var exams = await _examRepository.GetAll();
            int totalExams = exams.Count();

            if (totalExams == 0)
            {
                return new ExamStatistics { Over50Percent = 0, Under50Percent = 0 };
            }

            int examsOver50 = exams.Count(e =>
            {
                int totalQuestions = e.ExamQuestions.Count;
                int score = e.ExamQuestions.Count(eq => eq.IsCorrect());
                return (totalQuestions > 0) && score > (totalQuestions / 2.0);
            });

            double over50Percent = (double)examsOver50 / totalExams * 100;
            double under50Percent = 100 - over50Percent;

            return new ExamStatistics
            {
                Over50Percent = Math.Round(over50Percent, 2),
                Under50Percent = Math.Round(under50Percent, 2)
            };
        }
    }
}
