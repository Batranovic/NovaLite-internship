using Konteh.Domain;
using Konteh.Infrastructure.ExceptionHandlers.Exceptions;
using Konteh.Infrastructure.Repositories;
using MediatR;

namespace Konteh.FrontOfficeApi.Features.Exams;

public static class ExecuteExam
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
        public string Text { get; set; } = string.Empty;

        public class RequestHandler : IRequestHandler<Command>
        {
            private readonly IRepository<Exam> _examRepository;
            private readonly IRepository<ExamQuestion> _examQuestionRepository;

            public RequestHandler(IRepository<Exam> examRepository, IRepository<ExamQuestion> examQuestionRepository)
            {
                _examRepository = examRepository;
                _examQuestionRepository = examQuestionRepository;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var exam = await _examRepository.GetById(request.ExamId)
                            ?? throw new NotFoundException();

                exam.EndTime = DateTime.UtcNow;

                var selectedAnswers = request.ExamQuestions.ToDictionary(e => e.ExamQuestionId, e => e.SubmittedAnswers.Select(a => a.Id).ToHashSet());



                await _examRepository.SaveChanges();
            }
        }
    }
}