using FluentValidation;
using Konteh.Domain.Enumerations;
using static Konteh.BackOfficeApi.Features.Questions.CreateUpdateQuestion;

namespace Konteh.BackOfficeApi.Features.Questions.Validators;

public class CreateUpdateQuestionValidator : AbstractValidator<Command>
{
    public CreateUpdateQuestionValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage("Question text can't be empty.");

        RuleFor(x => x.Category)
            .IsInEnum()
            .WithMessage("Question cateqory must be valid value.");

        RuleFor(x => x.Type)
            .IsInEnum()
            .WithMessage("Question type must be valid value.");

        RuleFor(x => x)
            .Must(command => HasCorrectAnswer(command.Type, command.Answers))
            .WithMessage("RadioButton question type can only have one correct answer");

        RuleFor(x => x)
            .Must(command => HasCorrectAnswer(command.Type, command.Answers))
            .WithMessage("Question must have at least one correct answer");

        RuleForEach(x => x.Answers).ChildRules(a =>
        {
            a.RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage("Answer text can't be empty");
        });

    }

    private static bool HasCorrectAnswer(QuestionType type, List<AnswerDto> answers)
    {
        if (type == QuestionType.RadioButton)
        {
            return type != QuestionType.RadioButton || answers.Count(a => a.IsCorrect) <= 1;
        }
        return answers.Any(a => a.IsCorrect);
    }
}
