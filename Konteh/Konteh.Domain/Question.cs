using FluentResults;
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
        var canCreate = CanCreate(command);
        if (canCreate.IsFailed)
        {
            throw new InvalidOperationException();
        }

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
                    IsDeleted = x.IsDeleted
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
                    IsDeleted = x.IsDeleted
                }).ToList()
            },
            _ => throw new Exception("Unknown question type")
        };
    }

    public static Result CanCreate(CreateOrUpdateQuestionCommand command) =>
        command.Type switch
        {
            QuestionType.RadioButton => command.Answers.Count(x => x.IsCorrect && x.IsDeleted == false) == 1 ?
                    Result.Ok() :
                    Result.Fail("Must have exactly one correct answer"),
            QuestionType.CheckBox => command.Answers.Count(x => x.IsCorrect && x.IsDeleted == false) > 0
                    ? Result.Ok()
                    : Result.Fail("Must have at least one correct answer"),
            _ => throw new InvalidOperationException()
        };
}
