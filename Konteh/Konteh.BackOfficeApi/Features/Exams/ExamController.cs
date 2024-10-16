using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Konteh.BackOfficeApi.Features.Exams
{
    [ApiController]
    [Route("exams")]
    public class ExamController : Controller
    {
        private readonly IMediator _mediator;

        public ExamController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<GenerateExam.Response>> GenerateExam()
        {
            var response = await _mediator.Send(new GenerateExam.Command());
            return Ok(response);
        }
    }
}
