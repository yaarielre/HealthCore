using HealthCore.Application.Features.MedicalImages.Commands.Create;
using HealthCore.Application.Features.MedicalImages.Commands.Update;
using HealthCore.Application.Features.MedicalImages.Commands.Delete;
using HealthCore.Application.Features.MedicalImages.DTOs;
using HealthCore.Application.Features.MedicalImages.Queries.GetAll;
using HealthCore.Application.Features.MedicalImages.Queries.GetById;
using HealthCore.Application.Features.MedicalImages.Queries.GetByPatient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCore.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MedicalImagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MedicalImagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllMedicalImagesQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetMedicalImageByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("patient/{patientId:guid}")]
    public async Task<IActionResult> GetByPatient(Guid patientId)
    {
        var result = await _mediator.Send(new GetMedicalImagesByPatientQuery(patientId));
        return Ok(result);
    }

    [Authorize(Roles = "Administrator,Doctor")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMedicalImageDto dto)
    {
        var result = await _mediator.Send(new CreateMedicalImageCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [Authorize(Roles = "Administrator,Doctor")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMedicalImageDto dto)
    {
        var result = await _mediator.Send(new UpdateMedicalImageCommand(id, dto));
        return Ok(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteMedicalImageCommand(id));
        return result ? NoContent() : NotFound();
    }
}
