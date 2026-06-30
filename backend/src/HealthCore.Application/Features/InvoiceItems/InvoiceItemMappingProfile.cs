using AutoMapper;
using HealthCore.Application.Features.InvoiceItems.DTOs;
using HealthCore.Domain.Entities;

namespace HealthCore.Application.Features.InvoiceItems;

public class InvoiceItemMappingProfile : Profile
{
    public InvoiceItemMappingProfile()
    {
        CreateMap<InvoiceItem, InvoiceItemDto>();
        CreateMap<CreateInvoiceItemDto, InvoiceItem>();
        CreateMap<UpdateInvoiceItemDto, InvoiceItem>();
    }
}
