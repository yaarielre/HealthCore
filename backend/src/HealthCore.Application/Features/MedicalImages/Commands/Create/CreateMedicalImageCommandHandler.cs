using AutoMapper;
using MediatR;
using HealthCore.Application.Features.MedicalImages.DTOs;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.MedicalImages.Commands.Create;

public class CreateMedicalImageCommandHandler : IRequestHandler<CreateMedicalImageCommand, MedicalImageDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateMedicalImageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MedicalImageDto> Handle(CreateMedicalImageCommand request, CancellationToken cancellationToken)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(request.Dto.PatientId)
            ?? throw new KeyNotFoundException($"Paciente con Id '{request.Dto.PatientId}' no encontrado.");

        var image = new MedicalImage
        {
            Id = Guid.NewGuid(),
            PatientId = request.Dto.PatientId,
            MedicalConsultationId = request.Dto.MedicalConsultationId,
            OrderItemId = request.Dto.OrderItemId,
            ImageType = request.Dto.ImageType,
            BodyPart = request.Dto.BodyPart,
            FileName = request.Dto.FileName,
            FilePath = request.Dto.FilePath,
            ContentType = request.Dto.ContentType,
            FileSizeBytes = request.Dto.FileSizeBytes,
            Description = request.Dto.Description,
            Interpretation = request.Dto.Interpretation,
            InterpretedById = request.Dto.InterpretedById,
            TakenAt = request.Dto.TakenAt
        };

        await _unitOfWork.MedicalImages.AddAsync(image);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = _mapper.Map<MedicalImageDto>(image) with
        {
            PatientName = $"{patient.FirstName} {patient.LastName}"
        };

        if (image.InterpretedById.HasValue)
        {
            var interpretedBy = await _unitOfWork.Users.GetByIdAsync(image.InterpretedById.Value);
            if (interpretedBy is not null)
                dto = dto with { InterpretedByName = $"{interpretedBy.FirstName} {interpretedBy.LastName}" };
        }

        return dto;
    }
}
