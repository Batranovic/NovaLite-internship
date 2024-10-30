using FluentValidation;
using Konteh.Domain.Commands;

namespace Konteh.BackOfficeApi.Features.Questions.Validators;

public class CreateUpdateQuestionValidator : AbstractValidator<CreateOrUpdateQuestionCommand>
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
            .Must(command => command.ValidateCorrectAnswers())
            .WithMessage("Invalid number of correct answers.");

        RuleForEach(x => x.Answers).ChildRules(a =>
        {
            a.RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage("Answer text can't be empty");
        });

    }
}