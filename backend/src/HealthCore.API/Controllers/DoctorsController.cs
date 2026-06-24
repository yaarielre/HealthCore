using HealthCore.Application.Features.Doctors.Commands.CreateDoctor;
using HealthCore.Application.Features.Doctors.Commands.ToggleDoctorStatus;
using HealthCore.Application.Features.Doctors.Commands.UpdateDoctor;
using HealthCore.Application.Features.Doctors.DTOs;
using HealthCore.Application.Features.Doctors.Queries.GetAllDoctors;
using HealthCore.Application.Features.Doctors.Queries.GetDoctorById;
using HealthCore.Application.Features.Doctors.Queries.GetDoctorsBySpecialty;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCore.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DoctorsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DoctorDto>>> GetAll()
        => Ok(await _mediator.Send(new GetAllDoctorsQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult<DoctorDto>> GetById(Guid id)
        => Ok(await _mediator.Send(new GetDoctorByIdQuery(id)));

    [HttpGet("by-specialty/{specialtyId}")]
    public async Task<ActionResult<IEnumerable<DoctorDto>>> GetBySpecialty(Guid specialtyId)
        => Ok(await _mediator.Send(new GetDoctorsBySpecialtyQuery(specialtyId)));

    [HttpPost]
    public async Task<ActionResult<DoctorDto>> Create(CreateDoctorDto dto)
    {
        var result = await _mediator.Send(new CreateDoctorCommand(
            dto.FirstName, dto.LastName, dto.LicenseNumber, dto.SpecialtyId));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DoctorDto>> Update(Guid id, UpdateDoctorDto dto)
        => Ok(await _mediator.Send(new UpdateDoctorCommand(
            id, dto.FirstName, dto.LastName, dto.LicenseNumber, dto.SpecialtyId)));

    [HttpPatch("{id}/toggle-status")]
    public async Task<ActionResult<bool>> ToggleStatus(Guid id)
        => Ok(await _mediator.Send(new ToggleDoctorStatusCommand(id)));
}