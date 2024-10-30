using FluentValidation;
using Konteh.Domain;
using Konteh.Domain.Enumerations;
using Konteh.FrontOfficeApi.Features.Exams.RandomGenerator;
using Konteh.Infrastructure.DTO;
using Konteh.Infrastructure.ExceptionHandlers.Exceptions;
using Konteh.Infrastructure.Repositories;
using MassTransit;
using MediatR;

namespace Konteh.FrontOfficeApi.Features.Exams;

public static class GenerateExam
{
    private static readonly List<QuestionCategory> Categories =
    [
        QuestionCategory.General,
        QuestionCategory.OOP,
    ];
    public class Command : IRequest<long>
    {
        public required string CandidateName { get; set; }
        public required string CandidateSurname { get; set; }
        public required string CandidateEmail { get; set; }
        public required string CandidateFaculty { get; set; }
    }

    public class RequestHandler : IRequestHandler<Command, long>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IRepository<Exam> _examRepository;
        private readonly IRandomGenerator _randomGenerator;
        private readonly IRepository<Candidate> _candidateRepository;
        private readonly IBus _bus;

        public RequestHandler(IQuestionRepository questionRepository,
            IRepository<Exam> examRepository,
            IRandomGenerator randomGenerator,
            IRepository<Candidate> candidateRepository,
            IBus bus)
        {
            _questionRepository = questionRepository;
            _examRepository = examRepository;
            _randomGenerator = randomGenerator;
            _candidateRepository = candidateRepository;
            _bus = bus;
        }

        public async Task<long> Handle(Command request, CancellationToken cancellationToken)
        {
            var candidate = await HasCandidateTakenATestAsync(request);

            var questions = (await _questionRepository.GetAll())
                .Where(x => Categories.Contains(x.Category))
                .ToList();

            var randomQuestions = new List<ExamQuestion>();

            foreach (var category in Categories)
            {
                var questionsInCategory = questions.Where(q => q.Category == category).ToList();

                if (questionsInCategory.Count < 2)
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
                StartTime = DateTime.Now,
                ExamQuestions = randomQuestions,
                Candiate = candidate,
                Status = ExamStatus.InProgess
            };

            _examRepository.Create(exam);

            await _examRepository.SaveChanges();
            await _bus.Publish(new GetExamResponse() { Id = exam.Id, Candidate = $"{exam.Candiate.Name} {exam.Candiate.Surname}", Score = 0, Status = ExamStatus.InProgess });
            return exam.Id;
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