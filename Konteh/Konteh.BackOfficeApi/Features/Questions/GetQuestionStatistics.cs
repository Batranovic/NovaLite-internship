using Konteh.Domain;
using Konteh.Infrastructure.Repositories;
using MediatR;

namespace Konteh.BackOfficeApi.Features.Questions;

public static class GetQuestionStatistics
{
    public class QuestionStatisticsQuery : IRequest<QuestionStatistics>
    {
        public long QuestionId { get; set; }
    }

    public class QuestionStatistics
    {
        public double CorrectAnswers { get; set; }
        public double WrongAnswers { get; set; }
    }

    public class RequestHandler : IRequestHandler<QuestionStatisticsQuery, QuestionStatistics>
    {
        private readonly IRepository<Exam> _examRepository;
        private readonly IQuestionRepository _questionRepository;

        public RequestHandler(IRepository<Exam> examRepository, IQuestionRepository questionRepository)
        {
            _examRepository = examRepository;
            _questionRepository = questionRepository;
        }

        public async Task<QuestionStatistics> Handle(QuestionStatisticsQuery request, CancellationToken cancellationToken)
        {

            var question = await _questionRepository.GetById(request.QuestionId);

            var exams = await _examRepository.GetAll();

            var examQuestions = exams
                .SelectMany(exam => exam.ExamQuestions)
                .Where(eq => eq.Question.Id == question?.Id)
                .ToList();

            int totalAttempts = examQuestions.Count;

            int correctAttempts = examQuestions.Count(eq =>
            {
                return eq.IsCorrect();
            });


            double answeredCorrect = (double)correctAttempts / totalAttempts * 100;
            double asnweredWrong = 100 - answeredCorrect;

            return new QuestionStatistics
            {
                CorrectAnswers = Math.Round(answeredCorrect, 2),
                WrongAnswers = Math.Round(asnweredWrong, 2)
            };

        }
    }
}
