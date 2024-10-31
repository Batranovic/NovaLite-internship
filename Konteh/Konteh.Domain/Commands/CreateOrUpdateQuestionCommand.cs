using Konteh.Domain.Enumerations;
using MediatR;

namespace Konteh.Domain.Commands;
public class CreateOrUpdateQuestionCommand : IRequest<Unit>
{
    public long? Id { get; set; } = null;
    public string Text { get; set; } = string.Empty;
    public QuestionCategory Category { get; set; }
    public QuestionType Type { get; set; }
    public List<AnswerDto> Answers { get; set; } = [];

    public class AnswerDto
    {
        public long Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
