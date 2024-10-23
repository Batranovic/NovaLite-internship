using Konteh.Domain;
using Konteh.Domain.Enumerations;
using Konteh.FrontOfficeApi.Features.Exams.RandomGenerator;
using Konteh.Infrastructure.Repositories;
using MediatR;

namespace Konteh.FrontOfficeApi.Features.Exams;

public static class GenerateExam
{
    private static readonly List<QuestionCategory> Categories =
    [
        QuestionCategory.General,
        QuestionCategory.OOP,
    ];
    public class Command : IRequest<Response>
    {
        public int QuestionPerCategory { get; set; }
    }
    public class AnswerDto
    {
        public long Id { get; set; }
        public string Text { get; set; } = string.Empty;
    }
    public class QuestionDto
    {
        public long Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public QuestionCategory Category { get; set; }
        public List<AnswerDto> Answers { get; set; } = [];
    }
    public class ExamQuestionDto
    {
        public long Id { get; set; }
        public required QuestionDto Question { get; set; }
    }
    public class Response
    {
        public long Id { get; set; }
        public DateTime StartTime { get; set; }
        public List<ExamQuestionDto> ExamQuestions { get; set; } = new List<ExamQuestionDto>();
    }

    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Exam> _examRepository;
        private readonly IRandomGenerator _randomGenerator;
        public Handler(IRepository<Question> questionRepository, IRepository<Exam> examRepository, IRandomGenerator randomGenerator)
        {
            _questionRepository = questionRepository;
            _examRepository = examRepository;
            _randomGenerator = randomGenerator;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var questions = await _questionRepository.Search(x => Categories.Contains(x.Category));
            var randomQuestions = new List<ExamQuestion>();

            foreach (var category in Categories)
            {
                var questionsInCategory = questions.Where(q => q.Category == category).ToList();

                if (questionsInCategory.Count() < request.QuestionPerCategory)
                {
                    throw new InvalidOperationException($"Not enough questions available in category '{category}'.");
                }

                var selectedQuestions = questionsInCategory.OrderBy(q => _randomGenerator.Next())
                    .Take(request.QuestionPerCategory)
                    .Select(x => new ExamQuestion { Question = x });
                randomQuestions.AddRange(selectedQuestions);
            }

            var examQuestionsDto = randomQuestions.Select(q => new ExamQuestionDto
            {
                Id = q.Id,
                Question = new QuestionDto
                {
                    Id = q.Question.Id,
                    Text = q.Question.Text,
                    Category = q.Question.Category,
                    Answers = q.Question.Answers.Select(a => new AnswerDto
                    {
                        Id = a.Id,
                        Text = a.Text
                    }).ToList()
                }
            }).ToList();

            var exam = new Exam
            {
                StartTime = DateTime.UtcNow,
                ExamQuestions = randomQuestions,
                Candiate = new Candidate { Email = "candidate@gmail.com", Faculty = "FTN", Name = "N", Surname = "B" }
            };

            _examRepository.Create(exam);
            await _examRepository.SaveChanges();

            return new Response
            {
                Id = exam.Id,
                StartTime = exam.StartTime,
                ExamQuestions = examQuestionsDto,
            };
        }
    }
}