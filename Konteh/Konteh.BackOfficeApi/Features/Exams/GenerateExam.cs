using Konteh.Domain;
using Konteh.Domain.Enumerations;
using Konteh.Infrastructure.Repositories;
using MediatR;

namespace Konteh.BackOfficeApi.Features.Exams
{
    public static class GenerateExam
    {
        private static readonly List<QuestionCategory> Categories =
        [
            QuestionCategory.OOP,
            QuestionCategory.General
        ];

        public class Command : IRequest<Response>
        {
            public int QuestionPerCategpry { get; set; } = 3;
        }

        public class Response
        {
            public long Id { get; set; }
            public DateTime StartTime { get; set; }
            public List<ExamQuestion> ExamQuestions { get; set; } = new List<ExamQuestion>();
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IRepository<Question> _questionRepository;
            private readonly IRepository<Exam> _examRepository;

            public Handler(IRepository<Question> questionRepository, IRepository<Exam> examRepository)
            {
                _questionRepository = questionRepository;
                _examRepository = examRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var questions = await _questionRepository.Search(x => Categories.Contains(x.Category));
                var randomQuestions = new List<ExamQuestion>();
                var r = new Random();

                foreach (QuestionCategory category in Categories)
                {
                    var questionsInCategory = questions.Where(q => q.Category == category).ToList();
                    var selectedQuestions = questionsInCategory.OrderBy(q => r.Next())
                        .Take(request.QuestionPerCategpry)
                        .Select(x => new ExamQuestion { Question = x });
                    randomQuestions.AddRange(selectedQuestions);
                }

                var exam = new Exam
                {
                    StartTime = DateTime.UtcNow,
                    ExamQuestions = randomQuestions,
                    Candiate = new Candidate { Email = "candidate@gmail.com", Faculty ="FTN", Name="N", Surname="B"}
                };

                _examRepository.Create(exam);
                await _examRepository.SaveChanges();

                return new Response
                {
                    Id = exam.Id,
                    StartTime = exam.StartTime,
                    ExamQuestions = randomQuestions,

                };
            }
        }

    }
}
