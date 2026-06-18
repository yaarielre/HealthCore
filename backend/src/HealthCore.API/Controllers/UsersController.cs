using HealthCore.Application.Features.Users.Commands.ChangeUserStatus;
using HealthCore.Application.Features.Users.Commands.CreateUser;
using HealthCore.Application.Features.Users.Commands.UpdateUser;
using HealthCore.Application.Features.Users.Commands.ChangePassword;
using HealthCore.Application.Features.Users.DTOs;
using HealthCore.Application.Features.Users.Queries.GetAllUsers;
using HealthCore.Application.Features.Users.Queries.GetUserById;
using HealthCore.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCore.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllUsersQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        var result = await _mediator.Send(new CreateUserCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto dto)
    {
        var result = await _mediator.Send(new UpdateUserCommand(id, dto));
        return Ok(result);
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] AccountStatus newStatus)
    {
        var result = await _mediator.Send(new ChangeUserStatusCommand(id, newStatus));
        return Ok(result);
    }

    [HttpPatch("{id:guid}/password")]
    public async Task<IActionResult> ChangePassword(Guid id, [FromBody] ChangePasswordDto dto)
    {
        await _mediator.Send(new ChangePasswordCommand(id, dto.NewPassword));
        return Ok();
    }
}
