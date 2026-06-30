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

        CreateMap<UpdatePrescriptionDto, PrescriptionEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PatientId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Patient, opt => opt.Ignore())
            .ForMember(dest => dest.Doctor, opt => opt.Ignore())
            .ForMember(dest => dest.MedicalConsultation, opt => opt.Ignore());
    }
}
