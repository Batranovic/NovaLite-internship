using Konteh.Domain.Commands;
using Konteh.Domain.Enumerations;

namespace Konteh.Domain;

public abstract class Question
{
    public long Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public QuestionCategory Category { get; set; }
    public abstract QuestionType Type { get; set; }
    public List<Answer> Answers { get; set; } = [];
    public bool IsDeleted { get; set; } = false;

    internal abstract bool IsCorrect(IEnumerable<long> submittedAnswerIds);

    public static Question Create(CreateOrUpdateQuestionCommand command)
    {
        return command.Type switch
        {
            QuestionType.RadioButton => new RadioButtonQuestion
            {
                Text = command.Text,
                Category = command.Category,
                Answers = command.Answers.Select(x => new Answer
                {
                    Text = x.Text,
                    IsCorrect = x.IsCorrect,
                    IsDeleted = false
                }).ToList()
            },
            QuestionType.CheckBox => new CheckBoxQuestion
            {
                Text = command.Text,
                Category = command.Category,
                Answers = command.Answers.Select(x => new Answer
                {
                    Text = x.Text,
                    IsCorrect = x.IsCorrect,
                    IsDeleted = false
                }).ToList()
            },
            _ => throw new Exception("Unknown question type")
        };
    }
}
