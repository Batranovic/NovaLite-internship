using Konteh.FrontOfficeApi.Features.Exams.RandomGenerator;
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

    [HttpPost]
    public async Task<ActionResult<GenerateExam.Response>> GenerateExam(GenerateExam.Command command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult> ExamSubmission(ExamSubmission.Command command)
    {
        await _mediator.Send(command);
        return Ok();
    }


}
