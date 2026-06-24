using AutoMapper;
using HealthCore.Application.Features.Doctors.DTOs;
using HealthCore.Domain.Entities;

namespace HealthCore.Application.Features.Doctors;

public class DoctorMappingProfile : Profile
{
    public DoctorMappingProfile()
    {
        CreateMap<Doctor, DoctorDto>()
            .ForMember(dest => dest.SpecialtyName, opt => opt.MapFrom(src => src.Specialty.Name));

        CreateMap<CreateDoctorDto, Doctor>();
        CreateMap<UpdateDoctorDto, Doctor>();
    }
}