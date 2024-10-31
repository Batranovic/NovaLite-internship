using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Konteh.Infrastructure.Filters;
public class EmailClaimRequiredAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var emailClaim = context.HttpContext.Request.Headers["email"].ToString();

        if (string.IsNullOrEmpty(emailClaim))
        {
            context.Result = new UnauthorizedObjectResult("Email claim is missing in the request headers.");
        }
    }
}
