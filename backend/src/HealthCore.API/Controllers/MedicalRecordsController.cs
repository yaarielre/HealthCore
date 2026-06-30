using HealthCore.Application.Features.MedicalRecords.Commands.CreateMedicalRecord;
using HealthCore.Application.Features.MedicalRecords.Commands.UpdateMedicalRecord;
using HealthCore.Application.Features.MedicalRecords.Commands.DeleteMedicalRecord;
using HealthCore.Application.Features.MedicalRecords.DTOs;
using HealthCore.Application.Features.MedicalRecords.Queries.GetAllMedicalRecords;
using HealthCore.Application.Features.MedicalRecords.Queries.GetMedicalRecordById;
using HealthCore.Application.Features.MedicalRecords.Queries.GetMedicalRecordByPatient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCore.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MedicalRecordsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MedicalRecordsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllMedicalRecordsQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetMedicalRecordByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("patient/{patientId:guid}")]
    public async Task<IActionResult> GetByPatient(Guid patientId)
    {
        var result = await _mediator.Send(new GetMedicalRecordByPatientQuery(patientId));
        return result is null ? NotFound() : Ok(result);
    }

    [Authorize(Roles = "Administrator,Doctor")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMedicalRecordDto dto)
    {
        var result = await _mediator.Send(new CreateMedicalRecordCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [Authorize(Roles = "Administrator,Doctor")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMedicalRecordDto dto)
    {
        var result = await _mediator.Send(new UpdateMedicalRecordCommand(id, dto));
        return Ok(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteMedicalRecordCommand(id));
        return result ? NoContent() : NotFound();
    }
}
