using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Konteh.BackOfficeApi.Features.Exams;

[ApiController]
[Route("exams")]
public class ExamsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExamsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<GetAllExams.Response>> GetAllExams(GetAllExams.Query request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}
