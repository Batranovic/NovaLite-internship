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

    [HttpGet("{id}")]
    public async Task<ActionResult<GetQuestionById.Response>> GetQuestionById(long id)
    {
        var response = await _mediator.Send(new GetQuestionById.Query { Id = id });
        return Ok(response);
    }

    [HttpPut("/createOrUpdate")]
    public async Task<ActionResult<CreateUpdateQuestion.Response>> CreateOrUpdateQuestion(CreateUpdateQuestion.Command command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("{questionId:long}")]
    public async Task<ActionResult<bool>> DeleteById(long questionId)
    {
        await _mediator.Send(new DeleteQuestion.Command { Id = questionId });
        return Ok();
    }
}
