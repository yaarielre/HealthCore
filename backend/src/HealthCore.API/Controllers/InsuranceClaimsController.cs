using HealthCore.Application.Features.InsuranceClaims.Commands.Approve;
using HealthCore.Application.Features.InsuranceClaims.Commands.Create;
using HealthCore.Application.Features.InsuranceClaims.Commands.Deny;
using HealthCore.Application.Features.InsuranceClaims.Commands.Update;
using HealthCore.Application.Features.InsuranceClaims.DTOs;
using HealthCore.Application.Features.InsuranceClaims.Queries.GetAll;
using HealthCore.Application.Features.InsuranceClaims.Queries.GetById;
using HealthCore.Application.Features.InsuranceClaims.Queries.GetByPatient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCore.API.Controllers;

[Authorize(Roles = "Administrator")]
[ApiController]
[Route("api/[controller]")]
public class InsuranceClaimsController : ControllerBase
{
    private readonly IMediator _mediator;

    public InsuranceClaimsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InsuranceClaimDto>>> GetAll()
        => Ok(await _mediator.Send(new GetAllInsuranceClaimsQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult<InsuranceClaimDto>> GetById(Guid id)
        => Ok(await _mediator.Send(new GetInsuranceClaimByIdQuery(id)));

    [HttpGet("by-patient/{patientId}")]
    public async Task<ActionResult<IEnumerable<InsuranceClaimDto>>> GetByPatient(Guid patientId)
        => Ok(await _mediator.Send(new GetInsuranceClaimsByPatientQuery(patientId)));

    [HttpPost]
    public async Task<ActionResult<InsuranceClaimDto>> Create(CreateInsuranceClaimDto dto)
    {
        var result = await _mediator.Send(new CreateInsuranceClaimCommand(
            dto.InvoiceId, dto.PatientId, dto.InsuranceCompany, dto.PolicyNumber, dto.ClaimAmount, dto.Notes));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<InsuranceClaimDto>> Update(Guid id, UpdateInsuranceClaimDto dto)
        => Ok(await _mediator.Send(new UpdateInsuranceClaimCommand(
            id, dto.InsuranceCompany, dto.PolicyNumber, dto.ClaimAmount, dto.Notes)));

    [HttpPatch("{id}/approve")]
    public async Task<ActionResult<InsuranceClaimDto>> Approve(Guid id, [FromBody] decimal approvedAmount)
        => Ok(await _mediator.Send(new ApproveInsuranceClaimCommand(id, approvedAmount)));

    [HttpPatch("{id}/deny")]
    public async Task<ActionResult<InsuranceClaimDto>> Deny(Guid id, [FromBody] string? notes)
        => Ok(await _mediator.Send(new DenyInsuranceClaimCommand(id, notes)));
}
