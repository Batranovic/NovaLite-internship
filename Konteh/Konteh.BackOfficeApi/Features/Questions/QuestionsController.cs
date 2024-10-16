using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Konteh.BackOfficeApi.Features.Questions;

[ApiController]
[Route("questions")]
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

    [HttpGet("page-count")]
    public async Task<ActionResult<QuestionPageCount.Response>> GetPageCount(
    [FromQuery] float pageSize,
    [FromQuery] string? questionText = null)
    {
        var response = await _mediator.Send(new QuestionPageCount.Query(pageSize, questionText));
        return Ok(response);
    }


}
