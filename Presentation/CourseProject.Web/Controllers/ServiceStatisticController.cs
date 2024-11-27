﻿using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using CourseProject.Application.Dtos;
using CourseProject.Application.Requests.Queries;
using CourseProject.Application.Requests.Commands;

namespace CourseProject.Web.Controllers;

[Route("api/serviceStatistics")]
[ApiController]
public class ServiceStatisticController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServiceStatisticController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var serviceStatistics = await _mediator.Send(new GetServiceStatisticsQuery());

        return Ok(serviceStatistics);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var serviceStatistic = await _mediator.Send(new GetServiceStatisticByIdQuery(id));

        if (serviceStatistic is null)
        {
            return NotFound($"ServiceStatistic with id {id} is not found.");
        }
        
        return Ok(serviceStatistic);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ServiceStatisticForCreationDto? serviceStatistic)
    {
        if (serviceStatistic is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateServiceStatisticCommand(serviceStatistic));

        return CreatedAtAction(nameof(Create), serviceStatistic);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ServiceStatisticForUpdateDto? serviceStatistic)
    {
        if (serviceStatistic is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateServiceStatisticCommand(serviceStatistic));

        if (!isEntityFound)
        {
            return NotFound($"ServiceStatistic with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteServiceStatisticCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"ServiceStatistic with id {id} is not found.");
        }

        return NoContent();
    }
}
