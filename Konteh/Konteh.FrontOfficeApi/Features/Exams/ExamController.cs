using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Konteh.FrontOfficeApi.Features.Exams
{
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
        public async Task<ActionResult<GenerateExam.Response>> GenerateExam([FromBody] GenerateExam.Command command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
