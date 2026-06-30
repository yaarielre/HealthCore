using AutoMapper;
using HealthCore.Application.Features.MedicalImages.DTOs;
using HealthCore.Domain.Entities;

namespace HealthCore.Application.Features.MedicalImages;

public class MedicalImageMappingProfile : Profile
{
    public MedicalImageMappingProfile()
    {
        CreateMap<MedicalImage, MedicalImageDto>()
            .ForCtorParam(nameof(MedicalImageDto.PatientName), opt => opt.MapFrom(src =>
                src.Patient != null ? $"{src.Patient.FirstName} {src.Patient.LastName}" : string.Empty))
            .ForCtorParam(nameof(MedicalImageDto.InterpretedByName), opt => opt.MapFrom(src =>
                src.InterpretedBy != null ? $"{src.InterpretedBy.FirstName} {src.InterpretedBy.LastName}" : null));

        CreateMap<CreateMedicalImageDto, MedicalImage>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Patient, opt => opt.Ignore())
            .ForMember(dest => dest.MedicalConsultation, opt => opt.Ignore())
            .ForMember(dest => dest.OrderItem, opt => opt.Ignore())
            .ForMember(dest => dest.InterpretedBy, opt => opt.Ignore());

        CreateMap<UpdateMedicalImageDto, MedicalImage>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PatientId, opt => opt.Ignore())
            .ForMember(dest => dest.MedicalConsultationId, opt => opt.Ignore())
            .ForMember(dest => dest.OrderItemId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Patient, opt => opt.Ignore())
            .ForMember(dest => dest.MedicalConsultation, opt => opt.Ignore())
            .ForMember(dest => dest.OrderItem, opt => opt.Ignore())
            .ForMember(dest => dest.InterpretedBy, opt => opt.Ignore());
    }
}
