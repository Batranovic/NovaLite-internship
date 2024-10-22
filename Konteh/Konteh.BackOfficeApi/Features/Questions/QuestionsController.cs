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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllQuestions.Response>>> GetAll()
    {
        var response = await _mediator.Send(new GetAllQuestions.Query());
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

}
