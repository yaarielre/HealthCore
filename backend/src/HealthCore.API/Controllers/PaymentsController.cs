using HealthCore.Application.Features.Payments.Commands.Create;
using HealthCore.Application.Features.Payments.DTOs;
using HealthCore.Application.Features.Payments.Queries.GetByInvoice;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCore.API.Controllers;

[Authorize(Roles = "Administrator")]
[ApiController]
[Route("api/invoices/{invoiceId}/payments")]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaymentDto>>> GetByInvoice(Guid invoiceId)
        => Ok(await _mediator.Send(new GetPaymentsByInvoiceQuery(invoiceId)));

    [HttpPost]
    public async Task<ActionResult<PaymentDto>> Create(Guid invoiceId, CreatePaymentDto dto)
    {
        var result = await _mediator.Send(new CreatePaymentCommand(
            invoiceId, dto.Amount, dto.PaymentMethod, dto.ReferenceNumber, dto.ReceivedById, dto.Notes));
        return CreatedAtAction(nameof(GetByInvoice), new { invoiceId }, result);
    }
}
