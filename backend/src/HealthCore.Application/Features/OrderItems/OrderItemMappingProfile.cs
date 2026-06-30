using AutoMapper;
using HealthCore.Application.Features.OrderItems.DTOs;
using OrderItemEntity = HealthCore.Domain.Entities.OrderItem;

namespace HealthCore.Application.Features.OrderItems;

public class OrderItemMappingProfile : Profile
{
    public OrderItemMappingProfile()
    {
        CreateMap<OrderItemEntity, OrderItemDto>()
            .ForCtorParam(nameof(OrderItemDto.OrderNumber), opt => opt.MapFrom(src =>
                src.Order != null ? src.Order.OrderNumber : string.Empty))
            .ForCtorParam(nameof(OrderItemDto.TotalPrice), opt => opt.MapFrom(src =>
                src.UnitPrice.HasValue ? src.UnitPrice * src.Quantity : null));

        CreateMap<CreateOrderItemDto, OrderItemEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.OrderId, opt => opt.Ignore())
            .ForMember(dest => dest.Results, opt => opt.Ignore())
            .ForMember(dest => dest.ResultUrl, opt => opt.Ignore())
            .ForMember(dest => dest.ResultedAt, opt => opt.Ignore())
            .ForMember(dest => dest.ResultedBy, opt => opt.Ignore())
            .ForMember(dest => dest.IsCompleted, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.ResultedByUser, opt => opt.Ignore());

        CreateMap<UpdateOrderItemDto, OrderItemEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.OrderId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.ResultedByUser, opt => opt.Ignore());
    }
}
