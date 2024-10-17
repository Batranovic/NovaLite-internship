using Konteh.Domain.Enumerations;

namespace Konteh.Domain;

public class Question
{
    public long Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public QuestionCategory Category { get; set; }
    public QuestionType Type { get; set; }
    public List<Answer> Answers { get; set; } = [];
    public bool IsDeleted { get; set; } = false;
}
