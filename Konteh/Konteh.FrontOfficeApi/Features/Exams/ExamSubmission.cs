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
        public List<AnswerDto> SubmittedAnswers { get; set; } = [];
    }
    public class AnswerDto
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
            var exam = await _examRepository.GetById(request.ExamId)
                        ?? throw new InvalidOperationException($"No exam with id:{request.ExamId}.");
            exam.EndTime = DateTime.UtcNow;

            foreach (var questionDto in request.ExamQuestions)
            {
                var examQuestion = await _examQuestionRepository.GetById(questionDto.ExamQuestionId)
                                   ?? throw new InvalidOperationException($"No exam question with id:{questionDto.ExamQuestionId}.");

                examQuestion.SubmmitedAnswers.Clear();

                var submittedAnswers = new List<Answer>();

                foreach (var answerDto in questionDto.SubmittedAnswers)
                {
                    var answer = await _answerRepository.GetById(answerDto.Id)
                                 ?? throw new InvalidOperationException($"No answer with id:{answerDto.Id}.");
                    submittedAnswers.Add(answer);
                }

                examQuestion.SubmmitedAnswers.AddRange(submittedAnswers);
            }

            await _examQuestionRepository.SaveChanges();
            await _examRepository.SaveChanges();
        }
    }

}
