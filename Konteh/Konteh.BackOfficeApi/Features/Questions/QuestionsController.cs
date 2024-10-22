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
    public async Task<ActionResult<SearchQuestions.Response>> Paginate([FromQuery] SearchQuestions.Query request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpDelete("{questionId:long}")]
    public async Task<ActionResult<bool>> DeleteById(long questionId)
    {
        await _mediator.Send(new DeleteQuestion.Command { Id = questionId });
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllQuestions.Response>>> GetAll()
    {
        var response = await _mediator.Send(new GetAllQuestions.Query());
        return Ok(response);
    }
}
