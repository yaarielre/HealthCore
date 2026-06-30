using AutoMapper;
using HealthCore.Application.Features.OrderTypes.DTOs;
using OrderTypeEntity = HealthCore.Domain.Entities.OrderType;

namespace HealthCore.Application.Features.OrderTypes;

public class OrderTypeMappingProfile : Profile
{
    public OrderTypeMappingProfile()
    {
        CreateMap<OrderTypeEntity, OrderTypeDto>();

        CreateMap<CreateOrderTypeDto, OrderTypeEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<UpdateOrderTypeDto, OrderTypeEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
    }
}
