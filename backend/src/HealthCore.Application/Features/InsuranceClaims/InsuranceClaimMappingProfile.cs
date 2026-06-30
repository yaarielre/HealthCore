using AutoMapper;
using HealthCore.Application.Features.InsuranceClaims.DTOs;
using HealthCore.Domain.Entities;

namespace HealthCore.Application.Features.InsuranceClaims;

public class InsuranceClaimMappingProfile : Profile
{
    public InsuranceClaimMappingProfile()
    {
        CreateMap<InsuranceClaim, InsuranceClaimDto>();
        CreateMap<CreateInsuranceClaimDto, InsuranceClaim>();
        CreateMap<UpdateInsuranceClaimDto, InsuranceClaim>();
    }
}
