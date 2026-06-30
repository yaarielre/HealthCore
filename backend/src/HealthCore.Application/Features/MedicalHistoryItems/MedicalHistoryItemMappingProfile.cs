using AutoMapper;
using HealthCore.Application.Features.MedicalHistoryItems.DTOs;
using HealthCore.Domain.Entities;

namespace HealthCore.Application.Features.MedicalHistoryItems;

public class MedicalHistoryItemMappingProfile : Profile
{
    public MedicalHistoryItemMappingProfile()
    {
        CreateMap<MedicalHistoryItem, MedicalHistoryItemDto>()
            .ForCtorParam(nameof(MedicalHistoryItemDto.PatientName), opt => opt.MapFrom(src =>
                src.Patient != null ? $"{src.Patient.FirstName} {src.Patient.LastName}" : string.Empty))
            .ForCtorParam(nameof(MedicalHistoryItemDto.RecordedByName), opt => opt.MapFrom(src =>
                src.RecordedBy != null ? $"{src.RecordedBy.FirstName} {src.RecordedBy.LastName}" : string.Empty));

        CreateMap<CreateMedicalHistoryItemDto, MedicalHistoryItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Patient, opt => opt.Ignore())
            .ForMember(dest => dest.RecordedBy, opt => opt.Ignore());

        CreateMap<UpdateMedicalHistoryItemDto, MedicalHistoryItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PatientId, opt => opt.Ignore())
            .ForMember(dest => dest.RecordedById, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Patient, opt => opt.Ignore())
            .ForMember(dest => dest.RecordedBy, opt => opt.Ignore());
    }
}
