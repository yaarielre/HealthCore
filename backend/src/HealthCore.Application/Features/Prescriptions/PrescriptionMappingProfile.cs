using AutoMapper;
using HealthCore.Application.Features.Prescriptions.DTOs;
using PrescriptionEntity = HealthCore.Domain.Entities.Prescription;

namespace HealthCore.Application.Features.Prescriptions;

public class PrescriptionMappingProfile : Profile
{
    public PrescriptionMappingProfile()
    {
        CreateMap<PrescriptionEntity, PrescriptionDto>()
            .ForCtorParam(nameof(PrescriptionDto.PatientName), opt => opt.MapFrom(src =>
                src.Patient != null ? $"{src.Patient.FirstName} {src.Patient.LastName}" : string.Empty))
            .ForCtorParam(nameof(PrescriptionDto.DoctorName), opt => opt.MapFrom(src =>
                src.Doctor != null ? $"{src.Doctor.FirstName} {src.Doctor.LastName}" : string.Empty));
    }
}
