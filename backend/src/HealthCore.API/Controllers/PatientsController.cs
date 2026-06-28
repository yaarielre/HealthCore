using HealthCore.Application.Features.Patients.Commands.CreatePatient;
using HealthCore.Application.Features.Patients.Commands.DeletePatient;
using HealthCore.Application.Features.Patients.Commands.RestorePatient;
using HealthCore.Application.Features.Patients.Commands.UpdatePatient;
using HealthCore.Application.Features.Patients.DTOs;
using HealthCore.Application.Features.Patients.Queries.GetAllPatients;
using HealthCore.Application.Features.Patients.Queries.GetPatientAppointments;
using HealthCore.Application.Features.Patients.Queries.GetPatientByIdNumber;
using HealthCore.Application.Features.Patients.Queries.GetPatientById;
using HealthCore.Application.Features.Patients.Queries.GetPatientMedicalHistory;
using HealthCore.Application.Features.Patients.Queries.SearchPatients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCore.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PatientsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllPatientsQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetPatientByIdQuery(id));
        return Ok(result);
    }

    [Authorize(Roles = "Administrator,Receptionist")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePatientDto dto)
    {
        var result = await _mediator.Send(new CreatePatientCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [Authorize(Roles = "Administrator,Receptionist")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePatientDto dto)
    {
        var result = await _mediator.Send(new UpdatePatientCommand(id, dto));
        return Ok(result);
    }

    [Authorize(Roles = "Administrator,Receptionist")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeletePatientCommand(id));
        return Ok(result);
    }

    [HttpGet("search/{term}")]
    public async Task<IActionResult> Search(string term)
    {
        var result = await _mediator.Send(new SearchPatientsQuery(term));
        return Ok(result);
    }

    [HttpGet("id-number/{idNumber}")]
    public async Task<IActionResult> GetByIdNumber(string idNumber)
    {
        var result = await _mediator.Send(new GetPatientByIdNumberQuery(idNumber));
        return Ok(result);
    }

    [HttpGet("{id:guid}/appointments")]
    public async Task<IActionResult> GetAppointments(Guid id)
    {
        var result = await _mediator.Send(new GetPatientAppointmentsQuery(id));
        return Ok(result);
    }

    [HttpGet("{id:guid}/medical-history")]
    public async Task<IActionResult> GetMedicalHistory(Guid id)
    {
        var result = await _mediator.Send(new GetPatientMedicalHistoryQuery(id));
        return Ok(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPatch("{id:guid}/restore")]
    public async Task<IActionResult> Restore(Guid id)
    {
        var result = await _mediator.Send(new RestorePatientCommand(id));
        return Ok(result);
    }
}