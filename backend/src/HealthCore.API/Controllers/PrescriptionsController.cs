using HealthCore.Application.Features.Prescriptions.Commands.CreatePrescription;
using HealthCore.Application.Features.Prescriptions.Commands.UpdatePrescription;
using HealthCore.Application.Features.Prescriptions.Commands.DeletePrescription;
using HealthCore.Application.Features.Prescriptions.DTOs;
using HealthCore.Application.Features.Prescriptions.Queries.GetAllPrescriptions;
using HealthCore.Application.Features.Prescriptions.Queries.GetPrescriptionById;
using HealthCore.Application.Features.Prescriptions.Queries.GetPrescriptionsByPatient;
using HealthCore.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCore.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IPdfService _pdfService;

    public PrescriptionsController(IMediator mediator, IPdfService pdfService)
    {
        _mediator = mediator;
        _pdfService = pdfService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllPrescriptionsQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetPrescriptionByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("patient/{patientId:guid}")]
    public async Task<IActionResult> GetByPatient(Guid patientId)
    {
        var result = await _mediator.Send(new GetPrescriptionsByPatientQuery(patientId));
        return Ok(result);
    }

    [Authorize(Roles = "Administrator,Doctor")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePrescriptionDto dto)
    {
        var result = await _mediator.Send(new CreatePrescriptionCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [Authorize(Roles = "Administrator,Doctor")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePrescriptionDto dto)
    {
        var result = await _mediator.Send(new UpdatePrescriptionCommand(id, dto));
        return Ok(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeletePrescriptionCommand(id));
        return result ? NoContent() : NotFound();
    }

    [Authorize(Roles = "Administrator,Doctor")]
    [HttpGet("{id:guid}/pdf")]
    public async Task<IActionResult> GeneratePdf(Guid id)
    {
        var prescription = await _mediator.Send(new GetPrescriptionByIdQuery(id));
        if (prescription is null)
            return NotFound();

        var pdf = _pdfService.GeneratePrescriptionPdf(
            prescription.PatientName,
            prescription.DoctorName,
            prescription.Medication,
            prescription.Dosage,
            prescription.Frequency,
            prescription.Duration,
            prescription.Instructions,
            prescription.CreatedAt);

        return File(pdf, "application/pdf", $"receta_{prescription.Id}.pdf");
    }
}
