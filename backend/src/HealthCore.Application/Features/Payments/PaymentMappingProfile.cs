using AutoMapper;
using HealthCore.Application.Features.Payments.DTOs;
using HealthCore.Domain.Entities;

namespace HealthCore.Application.Features.Payments;

public class PaymentMappingProfile : Profile
{
    public PaymentMappingProfile()
    {
        CreateMap<Payment, PaymentDto>();
        CreateMap<CreatePaymentDto, Payment>();
    }
}
