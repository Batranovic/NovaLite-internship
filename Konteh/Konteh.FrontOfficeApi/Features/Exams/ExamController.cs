using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Konteh.FrontOfficeApi.Features.Exams;

[ApiController]
[Route("exams")]
public class ExamController : Controller
{
    private readonly IMediator _mediator;

    public ExamController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // TODO: Remove this when notification using features are implemented
    [HttpPost("notify")]
    public async Task<ActionResult> Notify(SubmitExam.Command command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GenerateExam.Response>> GenerateExam(GenerateExam.Command command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult> ExecuteExam(ExecuteExam.Command command)
    {
        await _mediator.Send(command);
        return Ok();
    }

}
