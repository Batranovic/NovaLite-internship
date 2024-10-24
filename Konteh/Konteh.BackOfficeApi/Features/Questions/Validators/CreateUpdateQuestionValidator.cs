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
            .Must(command => ValidateCorrectAnswers(command.Type, command.Answers, out var errorMessage))
            .WithMessage(command => GetErrorMessage(command.Type, command.Answers));

        RuleForEach(x => x.Answers).ChildRules(a =>
        {
            a.RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage("Answer text can't be empty");
        });

    }

    private static bool ValidateCorrectAnswers(QuestionType type, List<AnswerDto> answers, out string errorMessage)
    {
        var correctAnswers = answers.Count(a => a.IsCorrect);

        if (type == QuestionType.RadioButton && correctAnswers > 1)
        {
            errorMessage = "RadioButton question type can only have one correct answer.";
            return false;
        }

        if (correctAnswers < 1)
        {
            errorMessage = "Question must have at least one correct answer.";
            return false;
        }

        errorMessage = string.Empty;
        return true;
    }

    private static string GetErrorMessage(QuestionType type, List<AnswerDto> answers)
    {
        ValidateCorrectAnswers(type, answers, out var errorMessage);
        return errorMessage;
    }
}