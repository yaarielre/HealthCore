using AutoMapper;
using HealthCore.Application.Features.MedicalConsultations.DTOs;
using ConsultationEntity = HealthCore.Domain.Entities.MedicalConsultation;
using VitalSignEntity = HealthCore.Domain.Entities.VitalSign;

namespace HealthCore.Application.Features.MedicalConsultations;

public class MedicalConsultationMappingProfile : Profile
{
    public MedicalConsultationMappingProfile()
    {
        CreateMap<VitalSignEntity, VitalSignDto>();

        CreateMap<ConsultationEntity, MedicalConsultationDto>()
            .ForCtorParam(nameof(MedicalConsultationDto.PatientName), opt => opt.MapFrom(src =>
                src.Patient != null ? $"{src.Patient.FirstName} {src.Patient.LastName}" : string.Empty))
            .ForCtorParam(nameof(MedicalConsultationDto.DoctorName), opt => opt.MapFrom(src =>
                src.Doctor != null ? $"{src.Doctor.FirstName} {src.Doctor.LastName}" : string.Empty));
    }
}
