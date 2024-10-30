using Konteh.Domain;
using Konteh.Domain.Enumerations;
using Konteh.Infrastructure.DTO;
using Konteh.Infrastructure.ExceptionHandlers.Exceptions;
using Konteh.Infrastructure.Repositories;
using MassTransit;
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
        private readonly IBus _bus;

        public RequestHandler(IBus bus, IRepository<Exam> examRepository)
        {
            _examRepository = examRepository;
            _bus = bus;
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
            await _bus.Publish(new GetExamDTO() { Id = exam.Id, Candidate = $"{exam.Candiate.Name} {exam.Candiate.Surname}", Score = 0, Status = ExamStatus.Completed });
        }
    }
}
