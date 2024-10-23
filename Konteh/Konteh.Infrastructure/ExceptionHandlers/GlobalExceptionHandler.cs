using FluentValidation;
using Konteh.Infrastructure.ExceptionHandlers.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Konteh.Infrastructure.ExceptionHandlers;
public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        switch (exception)
        {
            case ValidationException validationException:
                {
                    var problemDetails = new ValidationProblemDetails
                    {
                        Errors = new Dictionary<string, string[]>(),
                        Status = StatusCodes.Status400BadRequest,

                    };
                    foreach (var error in validationException.Errors)
                    {
                        if (problemDetails.Errors.ContainsKey(error.PropertyName))
                        {
                            var errorMessages = problemDetails.Errors[error.PropertyName].ToList();
                            errorMessages.Add(error.ErrorMessage);
                            problemDetails.Errors[error.PropertyName] = [.. errorMessages];
                        }
                        else
                        {
                            problemDetails.Errors[error.PropertyName] = [error.ErrorMessage];
                        }
                    }
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
                    return true;
                }
            case NotFoundException:
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return true;
            default:
                return false;
        }
    }
}
