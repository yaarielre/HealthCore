using AutoMapper;
using HealthCore.Application.Features.MedicalRecords.DTOs;
using HealthCore.Domain.Entities;

namespace HealthCore.Application.Features.MedicalRecords;

public class MedicalRecordMappingProfile : Profile
{
    public MedicalRecordMappingProfile()
    {
        CreateMap<MedicalRecord, MedicalRecordDto>()
            .ForCtorParam(nameof(MedicalRecordDto.PatientName), opt => opt.MapFrom(src =>
                src.Patient != null ? $"{src.Patient.FirstName} {src.Patient.LastName}" : string.Empty));

        CreateMap<CreateMedicalRecordDto, MedicalRecord>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Patient, opt => opt.Ignore());

        CreateMap<UpdateMedicalRecordDto, MedicalRecord>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PatientId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Patient, opt => opt.Ignore());
    }
}
