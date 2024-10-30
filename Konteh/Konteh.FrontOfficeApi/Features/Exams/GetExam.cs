using Konteh.Domain;
using Konteh.Domain.Enumerations;
using Konteh.FrontOfficeApi.Options;
using Konteh.Infrastructure.ExceptionHandlers.Exceptions;
using Konteh.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Options;

namespace Konteh.FrontOfficeApi.Features.Exams;

public static class GetExam
{
    public class Query : IRequest<Response>
    {
        public long ExamId { get; set; }
    }

    public class Response
    {
        public long Id { get; set; }
        public IEnumerable<ExamQuestionItem> Questions { get; set; } = [];
        public DateTime MaxEndDateTime { get; set; }
    }

    public class ExamQuestionItem
    {
        public long Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public QuestionType Type { get; set; }
        public IEnumerable<AnswerItem> Answers { get; set; } = [];
    }

    public class AnswerItem
    {
        public long Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsSelected { get; set; }
    }

    public class RequestHandler : IRequestHandler<Query, Response>
    {
        private readonly IRepository<Exam> _examRepository;
        private readonly ExamOptions _options;
        public RequestHandler(IRepository<Exam> examRepository, IOptions<ExamOptions> options)
        {
            _examRepository = examRepository;
            _options = options.Value;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var exam = await _examRepository.GetById(request.ExamId) ?? throw new NotFoundException();

            return new Response
            {
                Id = exam.Id,
                Questions = exam.ExamQuestions.Select(examQuestion => new ExamQuestionItem
                {
                    Id = examQuestion.Id,
                    Type = examQuestion.Question.Type,
                    Text = examQuestion.Question.Text,
                    Answers = examQuestion.Question.Answers.Select(answer => new AnswerItem
                    {
                        Id = answer.Id,
                        Text = answer.Text,
                        IsSelected = examQuestion.SubmittedAnswers.Contains(answer)
                    })
                }),
                MaxEndDateTime = exam.StartTime.AddSeconds(_options.MaxDurationInSeconds)
            };
        }
    }
}
