using FluentValidation;
using MediatR;

namespace Konteh.Infrastructure.PipelineBehaviours;
public class ValidationBehaviour<TRequst, TResponse> : IPipelineBehavior<TRequst, TResponse>
    where TRequst : IRequest
{
    private readonly IEnumerable<IValidator<TRequst>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequst>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequst request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequst>(request);

        var validationFailures = await Task.WhenAll(
            _validators.Select(validator => validator.ValidateAsync(context)));

        var errors = validationFailures
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .ToList();

        if (errors.Count != 0)
        {
            throw new ValidationException(errors);
        }

        return await next();

    }
}
