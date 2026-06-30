using HealthCore.Application.Features.MedicalHistoryItems.Commands.Create;
using HealthCore.Application.Features.MedicalHistoryItems.Commands.Update;
using HealthCore.Application.Features.MedicalHistoryItems.Commands.Delete;
using HealthCore.Application.Features.MedicalHistoryItems.DTOs;
using HealthCore.Application.Features.MedicalHistoryItems.Queries.GetAll;
using HealthCore.Application.Features.MedicalHistoryItems.Queries.GetById;
using HealthCore.Application.Features.MedicalHistoryItems.Queries.GetByPatient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCore.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MedicalHistoryItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MedicalHistoryItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllMedicalHistoryItemsQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetMedicalHistoryItemByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("patient/{patientId:guid}")]
    public async Task<IActionResult> GetByPatient(Guid patientId)
    {
        var result = await _mediator.Send(new GetMedicalHistoryItemsByPatientQuery(patientId));
        return Ok(result);
    }

    [Authorize(Roles = "Administrator,Doctor")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMedicalHistoryItemDto dto)
    {
        var result = await _mediator.Send(new CreateMedicalHistoryItemCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [Authorize(Roles = "Administrator,Doctor")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMedicalHistoryItemDto dto)
    {
        var result = await _mediator.Send(new UpdateMedicalHistoryItemCommand(id, dto));
        return Ok(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteMedicalHistoryItemCommand(id));
        return result ? NoContent() : NotFound();
    }
}
