using HealthCore.Application.Features.Immunizations.Commands.Create;
using HealthCore.Application.Features.Immunizations.Commands.Update;
using HealthCore.Application.Features.Immunizations.Commands.Delete;
using HealthCore.Application.Features.Immunizations.DTOs;
using HealthCore.Application.Features.Immunizations.Queries.GetAll;
using HealthCore.Application.Features.Immunizations.Queries.GetById;
using HealthCore.Application.Features.Immunizations.Queries.GetByPatient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCore.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ImmunizationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ImmunizationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllImmunizationsQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetImmunizationByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("patient/{patientId:guid}")]
    public async Task<IActionResult> GetByPatient(Guid patientId)
    {
        var result = await _mediator.Send(new GetImmunizationsByPatientQuery(patientId));
        return Ok(result);
    }

    [Authorize(Roles = "Administrator,Doctor,Nurse")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateImmunizationDto dto)
    {
        var result = await _mediator.Send(new CreateImmunizationCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [Authorize(Roles = "Administrator,Doctor")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateImmunizationDto dto)
    {
        var result = await _mediator.Send(new UpdateImmunizationCommand(id, dto));
        return Ok(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteImmunizationCommand(id));
        return result ? NoContent() : NotFound();
    }
}
