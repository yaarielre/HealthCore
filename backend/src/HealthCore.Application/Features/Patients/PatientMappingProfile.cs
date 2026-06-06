using AutoMapper;
using HealthCore.Application.Features.Patients.DTOs;
using HealthCore.Domain.Entities;

namespace HealthCore.Application.Features.Patients;

public class PatientMappingProfile : Profile
{
    public PatientMappingProfile()
    {
        CreateMap<Patient, PatientDto>();

        CreateMap<CreatePatientDto, Patient>();

        CreateMap<UpdatePatientDto, Patient>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Cedula, opt => opt.Ignore())
            .ForMember(dest => dest.BirthDate, opt => opt.Ignore());
    }
}