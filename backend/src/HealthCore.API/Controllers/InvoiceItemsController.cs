using HealthCore.Application.Features.InvoiceItems.Commands.Create;
using HealthCore.Application.Features.InvoiceItems.Commands.Delete;
using HealthCore.Application.Features.InvoiceItems.Commands.Update;
using HealthCore.Application.Features.InvoiceItems.DTOs;
using HealthCore.Application.Features.InvoiceItems.Queries.GetByInvoice;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCore.API.Controllers;

[Authorize(Roles = "Administrator")]
[ApiController]
[Route("api/invoices/{invoiceId}/items")]
public class InvoiceItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public InvoiceItemsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InvoiceItemDto>>> GetByInvoice(Guid invoiceId)
        => Ok(await _mediator.Send(new GetInvoiceItemsByInvoiceQuery(invoiceId)));

    [HttpPost]
    public async Task<ActionResult<InvoiceItemDto>> Create(Guid invoiceId, CreateInvoiceItemDto dto)
    {
        var result = await _mediator.Send(new CreateInvoiceItemCommand(
            invoiceId, dto.Description, dto.Quantity, dto.UnitPrice, dto.DiscountPercent, dto.TotalPrice, dto.OrderItemId));
        return CreatedAtAction(nameof(GetByInvoice), new { invoiceId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<InvoiceItemDto>> Update(Guid id, UpdateInvoiceItemDto dto)
        => Ok(await _mediator.Send(new UpdateInvoiceItemCommand(
            id, dto.Description, dto.Quantity, dto.UnitPrice, dto.DiscountPercent, dto.TotalPrice)));

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(Guid id)
        => Ok(await _mediator.Send(new DeleteInvoiceItemCommand(id)));
}
