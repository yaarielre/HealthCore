using HealthCore.Application.Features.Specialties.Commands.CreateSpecialty;
using HealthCore.Application.Features.Specialties.Commands.ToggleSpecialtyStatus;
using HealthCore.Application.Features.Specialties.Commands.UpdateSpecialty;
using HealthCore.Application.Features.Specialties.DTOs;
using HealthCore.Application.Features.Specialties.Queries.GetAllSpecialties;
using HealthCore.Application.Features.Specialties.Queries.GetSpecialtyById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCore.API.Controllers;

[Authorize(Roles = "Administrator")]
[ApiController]
[Route("api/[controller]")]
public class SpecialtiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SpecialtiesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SpecialtyDto>>> GetAll()
        => Ok(await _mediator.Send(new GetAllSpecialtiesQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult<SpecialtyDto>> GetById(Guid id)
        => Ok(await _mediator.Send(new GetSpecialtyByIdQuery(id)));

    [HttpPost]
    public async Task<ActionResult<SpecialtyDto>> Create(CreateSpecialtyDto dto)
    {
        var result = await _mediator.Send(new CreateSpecialtyCommand(dto.Name, dto.Description));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SpecialtyDto>> Update(Guid id, UpdateSpecialtyDto dto)
        => Ok(await _mediator.Send(new UpdateSpecialtyCommand(id, dto.Name, dto.Description)));

    [HttpPatch("{id}/toggle-status")]
    public async Task<ActionResult<bool>> ToggleStatus(Guid id)
        => Ok(await _mediator.Send(new ToggleSpecialtyStatusCommand(id)));
}