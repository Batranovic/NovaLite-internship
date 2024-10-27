using FluentValidation;
using Konteh.Domain;
using Konteh.Domain.Enumerations;
using Konteh.FrontOfficeApi.Features.Exams.RandomGenerator;
using Konteh.Infrastructure.ExceptionHandlers.Exceptions;
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
        public required string CandidateName { get; set; }
        public required string CandidateSurname { get; set; }
        public required string CandidateEmail { get; set; }
        public required string CandidateFaculty { get; set; }
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
        public QuestionType Type { get; set; }
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

    public class RequestHandler : IRequestHandler<Command, Response>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IRepository<Exam> _examRepository;
        private readonly IRandomGenerator _randomGenerator;
        private readonly IRepository<Candidate> _candidateRepository;
        public RequestHandler(IQuestionRepository questionRepository, IRepository<Exam> examRepository, IRandomGenerator randomGenerator, IRepository<Candidate> candidateRepository)
        {
            _questionRepository = questionRepository;
            _examRepository = examRepository;
            _randomGenerator = randomGenerator;
            _candidateRepository = candidateRepository;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var candidate = await HasCandidateTakenATestAsync(request);

            var questions = (await _questionRepository.GetAll())
                .Where(x => Categories.Contains(x.Category))
                .ToList();

            var randomQuestions = new List<ExamQuestion>();

            foreach (var category in Categories)
            {
                var questionsInCategory = questions.Where(q => q.Category == category).ToList();

                if (questionsInCategory.Count() < 2)
                {
                    throw new NotFoundException();
                }

                var selectedQuestions = questionsInCategory.OrderBy(q => _randomGenerator.Next())
                    .Take(2)
                    .Select(x => new ExamQuestion { Question = x });
                randomQuestions.AddRange(selectedQuestions);
            }

            var exam = new Exam
            {
                StartTime = DateTime.UtcNow,
                ExamQuestions = randomQuestions,
                Candiate = candidate
            };

            _examRepository.Create(exam);
            await _examRepository.SaveChanges();

            return new Response
            {
                Id = exam.Id,
                StartTime = exam.StartTime,
                ExamQuestions = exam.ExamQuestions.Select(q => new ExamQuestionDto
                {

                    Id = q.Id,
                    Question = new QuestionDto
                    {
                        Id = q.Question.Id,
                        Text = q.Question.Text,
                        Category = q.Question.Category,
                        Type = q.Question.Type,
                        Answers = q.Question.Answers
                        .Where(a => !a.IsDeleted)
                        .Select(a => new AnswerDto
                        {
                            Id = a.Id,
                            Text = a.Text
                        }).ToList()
                    }
                }).ToList(),

            };
        }

        private async Task<Candidate> HasCandidateTakenATestAsync(Command request)
        {
            var candidate = (await _candidateRepository.Search(x => x.Email == request.CandidateEmail)).FirstOrDefault();
            if (candidate == null)
            {
                candidate = new Candidate
                {
                    Name = request.CandidateName,
                    Surname = request.CandidateSurname,
                    Email = request.CandidateEmail,
                    Faculty = request.CandidateFaculty
                };
                _candidateRepository.Create(candidate);
                await _candidateRepository.SaveChanges();
            }

            var existingExam = await _examRepository.Search(e => e.Candiate.Email == candidate.Email);

            if (existingExam.Any())
            {
                throw new ValidationException("Candidate has already taken the exam.");
            }
            return candidate;
        }
    }
}