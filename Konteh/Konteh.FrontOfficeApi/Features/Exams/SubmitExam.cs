using Konteh.Domain;
using Konteh.Infrastructure.ExceptionHandlers.Exceptions;
using Konteh.Infrastructure.Repositories;
using MediatR;

namespace Konteh.FrontOfficeApi.Features.Exams;

public static class SubmitExam
{
    public class Command : IRequest
    {
        public long ExamId { get; set; }
        public List<ExamQuestionDto> ExamQuestions { get; set; } = [];
    }
    public class ExamQuestionDto
    {
        public long ExamQuestionId { get; set; }
        public List<long> SubmittedAnswers { get; set; } = [];
    }

    public class RequestHandler : IRequestHandler<Command>
    {
        private readonly IRepository<Exam> _examRepository;

        public RequestHandler(IRepository<Exam> examRepository)
        {
            _examRepository = examRepository;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {

            var exam = await _examRepository.GetById(request.ExamId)
                        ?? throw new NotFoundException();

            if (exam.EndTime != null)
            {
                throw new Exception("Exam has already been completed");
            }
            exam.EndTime = DateTime.UtcNow;

            var examQuestions = exam.ExamQuestions;

            foreach (var examQuestion in examQuestions)
            {
                var answersIds = request.ExamQuestions.Single(x => x.ExamQuestionId == examQuestion.Id).SubmittedAnswers;
                examQuestion.SubmittedAnswers = examQuestion.Question.Answers.Where(a => answersIds.Contains(a.Id)).ToList();
            }

            await _examRepository.SaveChanges();
        }
    }
}
