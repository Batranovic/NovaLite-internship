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

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<SearchQuestions.Response>>> Paginate(
    [FromQuery] int page,
    [FromQuery] int pageSize,
    [FromQuery] string? questionText)
    {
        var response = await _mediator.Send(new SearchQuestions.Query() { Page = page, PageSize = pageSize, QuestionText = questionText });
        return Ok(response);
    }

    [HttpDelete]
    public async Task<ActionResult<DeleteQuestion.Response>> DeleteById(
    [FromQuery] long questionId)
    {
        var response = await _mediator.Send(new DeleteQuestion.Query() { Id = questionId });
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
