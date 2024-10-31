using FluentValidation;
using Konteh.Domain;
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


        RuleFor(x => x).Must(BeCreateable).WithMessage($"{{{nameof(CreateOrUpdateQuestionCommand.Answers)}}}");

        RuleForEach(x => x.Answers).ChildRules(a =>
        {
            a.RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage("Answer text can't be empty");
        });

    }

    private bool BeCreateable(CreateOrUpdateQuestionCommand dto, CreateOrUpdateQuestionCommand command, ValidationContext<CreateOrUpdateQuestionCommand> context)
    {
        var result = Question.CanCreate(command);
        if (result.IsSuccess)
        {
            return true;
        }

        context.MessageFormatter.AppendArgument(nameof(dto.Answers), string.Join(',', result.Errors.Select(x => x.Message)));
        return false;
    }
}