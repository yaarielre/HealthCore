using HealthCore.Application.Features.OrderTypes.Commands.Create;
using HealthCore.Application.Features.OrderTypes.Commands.Update;
using HealthCore.Application.Features.OrderTypes.Commands.Delete;
using HealthCore.Application.Features.OrderTypes.DTOs;
using HealthCore.Application.Features.OrderTypes.Queries.GetAll;
using HealthCore.Application.Features.OrderTypes.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCore.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrderTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllOrderTypesQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetOrderTypeByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderTypeDto dto)
    {
        var result = await _mediator.Send(new CreateOrderTypeCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOrderTypeDto dto)
    {
        var result = await _mediator.Send(new UpdateOrderTypeCommand(id, dto));
        return Ok(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteOrderTypeCommand(id));
        return result ? NoContent() : NotFound();
    }
}
