using AutoMapper;
using HealthCore.Application.Features.Specialties.DTOs;
using HealthCore.Domain.Entities;

namespace HealthCore.Application.Features.Specialties;

public class SpecialtyMappingProfile : Profile
{
    public SpecialtyMappingProfile()
    {
        CreateMap<Specialty, SpecialtyDto>();
        CreateMap<CreateSpecialtyDto, Specialty>();
        CreateMap<UpdateSpecialtyDto, Specialty>();
    }
}