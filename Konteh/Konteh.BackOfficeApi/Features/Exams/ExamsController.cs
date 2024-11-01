﻿using Konteh.Infrastructure.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Konteh.BackOfficeApi.Features.Exams.GetAllExams;
using static Konteh.BackOfficeApi.Features.Exams.GetExamStatistics;

namespace Konteh.BackOfficeApi.Features.Exams;

[ApiController]
[Route("exams")]
public class ExamsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExamsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetExamResponse>>> GetAllExams([FromQuery] Query request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("statistics")]
    public async Task<ActionResult<ExamStatistics>> GetExamStatistics()
    {
        var response = await _mediator.Send(new StatisticsQuery());
        return Ok(response);
    }

}
