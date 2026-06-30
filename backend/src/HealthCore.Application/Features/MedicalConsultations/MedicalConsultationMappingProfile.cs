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

        CreateMap<UpdateMedicalConsultationDto, ConsultationEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PatientId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Patient, opt => opt.Ignore())
            .ForMember(dest => dest.Doctor, opt => opt.Ignore())
            .ForMember(dest => dest.Appointment, opt => opt.Ignore())
            .ForMember(dest => dest.VitalSigns, opt => opt.Ignore())
            .ForMember(dest => dest.Prescriptions, opt => opt.Ignore());
    }
}
