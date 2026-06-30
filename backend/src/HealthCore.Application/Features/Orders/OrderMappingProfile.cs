using AutoMapper;
using HealthCore.Application.Features.Orders.DTOs;
using OrderEntity = HealthCore.Domain.Entities.Order;

namespace HealthCore.Application.Features.Orders;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<OrderEntity, OrderDto>()
            .ForCtorParam(nameof(OrderDto.PatientName), opt => opt.MapFrom(src =>
                src.Patient != null ? $"{src.Patient.FirstName} {src.Patient.LastName}" : string.Empty))
            .ForCtorParam(nameof(OrderDto.DoctorName), opt => opt.MapFrom(src =>
                src.Doctor != null ? $"{src.Doctor.FirstName} {src.Doctor.LastName}" : string.Empty))
            .ForCtorParam(nameof(OrderDto.OrderTypeName), opt => opt.MapFrom(src =>
                src.OrderType != null ? src.OrderType.Name : string.Empty))
            .ForCtorParam(nameof(OrderDto.ItemCount), opt => opt.MapFrom(src =>
                src.OrderItems != null ? src.OrderItems.Count : 0));

        CreateMap<CreateOrderDto, OrderEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.OrderNumber, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => Domain.Enums.OrderStatus.Pending))
            .ForMember(dest => dest.OrderedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.CompletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CancelledAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Patient, opt => opt.Ignore())
            .ForMember(dest => dest.Doctor, opt => opt.Ignore())
            .ForMember(dest => dest.MedicalConsultation, opt => opt.Ignore())
            .ForMember(dest => dest.OrderType, opt => opt.Ignore())
            .ForMember(dest => dest.OrderItems, opt => opt.Ignore());

        CreateMap<UpdateOrderDto, OrderEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.OrderNumber, opt => opt.Ignore())
            .ForMember(dest => dest.PatientId, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorId, opt => opt.Ignore())
            .ForMember(dest => dest.MedicalConsultationId, opt => opt.Ignore())
            .ForMember(dest => dest.OrderedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Patient, opt => opt.Ignore())
            .ForMember(dest => dest.Doctor, opt => opt.Ignore())
            .ForMember(dest => dest.MedicalConsultation, opt => opt.Ignore())
            .ForMember(dest => dest.OrderType, opt => opt.Ignore())
            .ForMember(dest => dest.OrderItems, opt => opt.Ignore());
    }
}
