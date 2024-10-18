using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Konteh.BackOfficeApi.Features.Questions;

[ApiController]
[Route("questions")]
[Authorize]
public class QuestionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public QuestionsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("paginate")]
    public async Task<ActionResult<IEnumerable<PaginateQuestions.Response>>> Paginate(
    [FromQuery] int page = 1,
    [FromQuery] float pageSize = 10,
    [FromQuery] string? questionText = null)
    {
        var response = await _mediator.Send(new PaginateQuestions.Query(page, pageSize, questionText));
        return Ok(response);
    }

    [HttpDelete]
    public async Task<ActionResult<DeleteQuestion.Response>> DeleteById(
    [FromQuery] long questionId)
    {
        var response = await _mediator.Send(new DeleteQuestion.Query(questionId));
        if (response.Success)
        {
            return Ok(response);
        }
        else
        {
            return NotFound();
        }
    }
}
