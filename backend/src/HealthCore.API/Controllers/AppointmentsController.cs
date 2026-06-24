using HealthCore.Application.Features.Appointments.Commands.CreateAppointment;
using HealthCore.Application.Features.Appointments.Commands.UpdateAppointment;
using HealthCore.Application.Features.Appointments.Queries.GetAppointmentsByDate;
using HealthCore.Application.Features.Appointments.Queries.GetAppointmentsByDoctor;
using HealthCore.Application.Features.Appointments.Commands.ChangeAppointmentStatus;
using HealthCore.Application.Features.Appointments.DTOs;
using HealthCore.Application.Features.Appointments.Queries.GetAllAppointments;
using HealthCore.Application.Features.Appointments.Queries.GetAppointmentById;
using HealthCore.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCore.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AppointmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllAppointmentsQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetAppointmentByIdQuery(id));
        return Ok(result);
    }

    [HttpGet("doctor/{doctorId:guid}")]
    public async Task<IActionResult> GetByDoctor(Guid doctorId)
    {
        var result = await _mediator.Send(new GetAppointmentsByDoctorQuery(doctorId));
        return Ok(result);
    }

    [HttpGet("date/{date}")]
    public async Task<IActionResult> GetByDate(DateTime date)
    {
        var result = await _mediator.Send(new GetAppointmentsByDateQuery(date));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentDto dto)
    {
        var result = await _mediator.Send(new CreateAppointmentCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAppointmentDto dto)
    {
        var result = await _mediator.Send(new UpdateAppointmentCommand(id, dto));
        return Ok(result);
    }

    [Authorize(Roles = "Administrator,Receptionist,Doctor")]
    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] AppointmentStatus newStatus)
    {
        var result = await _mediator.Send(new ChangeAppointmentStatusCommand(id, newStatus));
        return Ok(result);
    }
}