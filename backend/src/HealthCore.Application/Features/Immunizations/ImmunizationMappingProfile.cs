using AutoMapper;
using HealthCore.Application.Features.Immunizations.DTOs;
using HealthCore.Domain.Entities;

namespace HealthCore.Application.Features.Immunizations;

public class ImmunizationMappingProfile : Profile
{
    public ImmunizationMappingProfile()
    {
        CreateMap<Immunization, ImmunizationDto>()
            .ForCtorParam(nameof(ImmunizationDto.PatientName), opt => opt.MapFrom(src =>
                src.Patient != null ? $"{src.Patient.FirstName} {src.Patient.LastName}" : string.Empty));

        CreateMap<CreateImmunizationDto, Immunization>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Patient, opt => opt.Ignore());

        CreateMap<UpdateImmunizationDto, Immunization>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PatientId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Patient, opt => opt.Ignore());
    }
}
