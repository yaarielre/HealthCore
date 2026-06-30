using HealthCore.Application.Features.OrderItems.Commands.Create;
using HealthCore.Application.Features.OrderItems.Commands.Update;
using HealthCore.Application.Features.OrderItems.Commands.Delete;
using HealthCore.Application.Features.OrderItems.DTOs;
using HealthCore.Application.Features.OrderItems.Queries.GetAll;
using HealthCore.Application.Features.OrderItems.Queries.GetById;
using HealthCore.Application.Features.OrderItems.Queries.GetByOrder;
using HealthCore.Application.Features.OrderItems.Queries.GetByPatient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCore.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrderItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllOrderItemsQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetOrderItemByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("order/{orderId:guid}")]
    public async Task<IActionResult> GetByOrder(Guid orderId)
    {
        var result = await _mediator.Send(new GetOrderItemsByOrderQuery(orderId));
        return Ok(result);
    }

    [HttpGet("patient/{patientId:guid}")]
    public async Task<IActionResult> GetByPatient(Guid patientId)
    {
        var result = await _mediator.Send(new GetOrderItemsByPatientQuery(patientId));
        return Ok(result);
    }

    [Authorize(Roles = "Administrator,Doctor")]
    [HttpPost("order/{orderId:guid}")]
    public async Task<IActionResult> Create(Guid orderId, [FromBody] CreateOrderItemDto dto)
    {
        var result = await _mediator.Send(new CreateOrderItemCommand(orderId, dto));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [Authorize(Roles = "Administrator,Doctor")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOrderItemDto dto)
    {
        var result = await _mediator.Send(new UpdateOrderItemCommand(id, dto));
        return Ok(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteOrderItemCommand(id));
        return result ? NoContent() : NotFound();
    }
}
