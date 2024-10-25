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
            private readonly IExamQuestionRepository _examQuestionRepository;

            public RequestHandler(IRepository<Exam> examRepository, IExamQuestionRepository examQuestionRepository)
            {
                _examRepository = examRepository;
                _examQuestionRepository = examQuestionRepository;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var exam = await _examRepository.GetById(request.ExamId)
                            ?? throw new NotFoundException();

                exam.EndTime = DateTime.UtcNow;

                var selectedAnswersIds = request.ExamQuestions.ToDictionary(e => e.ExamQuestionId, e => e.SubmittedAnswers.Select(a => a.Id).ToHashSet());

                var examQuestions = _examQuestionRepository.GetByIds(request.ExamQuestions.Select(e => e.ExamQuestionId).ToList());

                /* foreach(var examQuestion in examQuestions)
                 {
                     var answersIds = selectedAnswersIds[examQuestion.Id];

                 }*/

                await _examRepository.SaveChanges();
            }
        }

    }
}