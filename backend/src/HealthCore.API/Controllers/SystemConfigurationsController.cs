using HealthCore.Application.Features.SystemConfigurations.Commands.CreateSystemConfiguration;
using HealthCore.Application.Features.SystemConfigurations.Commands.ToggleSystemConfigurationStatus;
using HealthCore.Application.Features.SystemConfigurations.Commands.UpdateSystemConfiguration;
using HealthCore.Application.Features.SystemConfigurations.DTOs;
using HealthCore.Application.Features.SystemConfigurations.Queries.GetAllSystemConfigurations;
using HealthCore.Application.Features.SystemConfigurations.Queries.GetSystemConfigurationById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCore.API.Controllers;

[Authorize(Roles = "Administrator")]
[ApiController]
[Route("api/[controller]")]
public class SystemConfigurationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SystemConfigurationsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SystemConfigurationDto>>> GetAll()
        => Ok(await _mediator.Send(new GetAllSystemConfigurationsQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult<SystemConfigurationDto>> GetById(Guid id)
        => Ok(await _mediator.Send(new GetSystemConfigurationByIdQuery(id)));

    [HttpPost]
    public async Task<ActionResult<SystemConfigurationDto>> Create(CreateSystemConfigurationDto dto)
    {
        var result = await _mediator.Send(new CreateSystemConfigurationCommand(
            dto.Category, dto.ConfigKey, dto.ConfigValue, dto.Description, dto.IsEncrypted));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SystemConfigurationDto>> Update(Guid id, UpdateSystemConfigurationDto dto)
        => Ok(await _mediator.Send(new UpdateSystemConfigurationCommand(
            id, dto.Category, dto.ConfigKey, dto.ConfigValue, dto.Description, dto.IsEncrypted)));

    [HttpPatch("{id}/toggle-status")]
    public async Task<ActionResult<bool>> ToggleStatus(Guid id)
        => Ok(await _mediator.Send(new ToggleSystemConfigurationStatusCommand(id)));
}
