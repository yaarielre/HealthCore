using AutoMapper;
using HealthCore.Application.Features.Invoices.DTOs;
using HealthCore.Application.Features.InvoiceItems.DTOs;
using HealthCore.Domain.Entities;

namespace HealthCore.Application.Features.Invoices;

public class InvoiceMappingProfile : Profile
{
    public InvoiceMappingProfile()
    {
        CreateMap<Invoice, InvoiceDto>()
            .ForMember(d => d.Items, opt => opt.MapFrom(s => s.InvoiceItems));

        CreateMap<InvoiceItem, InvoiceItemDto>();

        CreateMap<CreateInvoiceDto, Invoice>();
        CreateMap<CreateInvoiceItemDto, InvoiceItem>();
        CreateMap<UpdateInvoiceDto, Invoice>();
    }
}
