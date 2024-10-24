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
            .Must(command => ValidateCorrectAnswers(command.Type, command.Answers))
            .WithMessage(command =>
            {
                if (command.Type == QuestionType.RadioButton)
                {
                    return "RadioButton question must have one correct answer.";
                }

                return "Question must have at least one correct answer.";
            });

        RuleForEach(x => x.Answers).ChildRules(a =>
        {
            a.RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage("Answer text can't be empty");
        });

    }

    private static bool ValidateCorrectAnswers(QuestionType type, List<AnswerDto> answers)
    {
        var correctAnswers = answers.Count(a => a.IsCorrect);

        if ((type == QuestionType.RadioButton && correctAnswers > 1) || correctAnswers < 1)
        {
            return false;
        }

        return true;
    }
}