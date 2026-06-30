using HealthCore.Application.Features.Invoices.Commands.Cancel;
using HealthCore.Application.Features.Invoices.Commands.Create;
using HealthCore.Application.Features.Invoices.Commands.Update;
using HealthCore.Application.Features.Invoices.DTOs;
using HealthCore.Application.Features.Invoices.Queries.GetAll;
using HealthCore.Application.Features.Invoices.Queries.GetById;
using HealthCore.Application.Features.Invoices.Queries.GetByPatient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCore.API.Controllers;

[Authorize(Roles = "Administrator")]
[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IMediator _mediator;

    public InvoicesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetAll()
        => Ok(await _mediator.Send(new GetAllInvoicesQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult<InvoiceDto>> GetById(Guid id)
        => Ok(await _mediator.Send(new GetInvoiceByIdQuery(id)));

    [HttpGet("by-patient/{patientId}")]
    public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetByPatient(Guid patientId)
        => Ok(await _mediator.Send(new GetInvoicesByPatientQuery(patientId)));

    [HttpPost]
    public async Task<ActionResult<InvoiceDto>> Create(CreateInvoiceDto dto)
    {
        var result = await _mediator.Send(new CreateInvoiceCommand(
            dto.PatientId,
            Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString()),
            dto.DueDate,
            dto.SubTotal,
            dto.TaxAmount,
            dto.DiscountAmount,
            dto.TotalAmount,
            dto.Notes,
            dto.Items));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<InvoiceDto>> Update(Guid id, UpdateInvoiceDto dto)
        => Ok(await _mediator.Send(new UpdateInvoiceCommand(
            id, dto.DueDate, dto.SubTotal, dto.TaxAmount, dto.DiscountAmount, dto.TotalAmount, dto.Notes)));

    [HttpPatch("{id}/cancel")]
    public async Task<ActionResult<bool>> Cancel(Guid id)
        => Ok(await _mediator.Send(new CancelInvoiceCommand(id)));
}
