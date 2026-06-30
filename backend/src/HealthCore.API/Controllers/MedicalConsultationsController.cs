using HealthCore.Application.Features.MedicalConsultations.Commands.CreateMedicalConsultation;
using HealthCore.Application.Features.MedicalConsultations.Commands.UpdateMedicalConsultation;
using HealthCore.Application.Features.MedicalConsultations.Commands.DeleteMedicalConsultation;
using HealthCore.Application.Features.MedicalConsultations.DTOs;
using HealthCore.Application.Features.MedicalConsultations.Queries.GetAllMedicalConsultations;
using HealthCore.Application.Features.MedicalConsultations.Queries.GetMedicalConsultationById;
using HealthCore.Application.Features.MedicalConsultations.Queries.GetMedicalConsultationsByPatient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCore.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MedicalConsultationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MedicalConsultationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllMedicalConsultationsQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetMedicalConsultationByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("patient/{patientId:guid}")]
    public async Task<IActionResult> GetByPatient(Guid patientId)
    {
        var result = await _mediator.Send(new GetMedicalConsultationsByPatientQuery(patientId));
        return Ok(result);
    }

    [Authorize(Roles = "Administrator,Doctor")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMedicalConsultationDto dto)
    {
        var result = await _mediator.Send(new CreateMedicalConsultationCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [Authorize(Roles = "Administrator,Doctor")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMedicalConsultationDto dto)
    {
        var result = await _mediator.Send(new UpdateMedicalConsultationCommand(id, dto));
        return Ok(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteMedicalConsultationCommand(id));
        return result ? NoContent() : NotFound();
    }
}
