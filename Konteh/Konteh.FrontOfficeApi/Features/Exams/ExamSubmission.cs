using Konteh.Domain;
using Konteh.Infrastructure.Repositories;
using MediatR;

namespace Konteh.FrontOfficeApi.Features.Exams;

public static class ExamSubmission
{
    public class Command : IRequest
    {
        public required long ExamId { get; set; }
        public List<ExamQuestionDto> ExamQuestions { get; set; } = [];
    }
    public class ExamQuestionDto
    {
        public long ExamQuestionId { get; set; }
        public List<SubmittedAnswerDto> SubmittedAnswers { get; set; } = [];
    }
    public class SubmittedAnswerDto
    {
        public long Id { get; set; }

    }
    public class Handler : IRequestHandler<Command>
    {
        private readonly IRepository<Exam> _examRepository;
        private readonly IRepository<ExamQuestion> _examQuestionRepository;
        private readonly IRepository<Answer> _answerRepository;

        public Handler(IRepository<Exam> examRepository, IRepository<ExamQuestion> examQuestionRepository, IRepository<Answer> answerRepository)
        {
            _examRepository = examRepository;
            _examQuestionRepository = examQuestionRepository;
            _answerRepository = answerRepository;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {

            var exam = await _examRepository.GetById(request.ExamId) ?? throw new InvalidOperationException($"No exam with id:{request.ExamId}.");
            exam.EndTime = DateTime.UtcNow;

            foreach (var question in request.ExamQuestions)
            {
                var submittedAnswers = new List<SubmittedAnswer>();

                foreach (var answerDto in question.SubmittedAnswers)
                {
                    var answer = await _answerRepository.GetById(answerDto.Id);
                    var submittedAnswer = new SubmittedAnswer
                    {
                        Answer = answer!
                    };
                    submittedAnswers.Add(submittedAnswer);
                }
                var examQuestion = await _examQuestionRepository.GetById(question.ExamQuestionId);
                examQuestion?.SubmmitedAnswers.AddRange(submittedAnswers);

                await _examQuestionRepository.SaveChanges();
            }

            await _examRepository.SaveChanges();

        }
    }
}
