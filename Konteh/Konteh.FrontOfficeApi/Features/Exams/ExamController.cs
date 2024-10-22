﻿using MediatR;
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
        public async Task<ActionResult<GenerateExam.Response>> GenerateExam()
        {
            var response = await _mediator.Send(new GenerateExam.Command());
            return Ok(response);
        }

        [HttpPost("notify")]
        public async Task<ActionResult> Notify(SubmitExam.Command command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
