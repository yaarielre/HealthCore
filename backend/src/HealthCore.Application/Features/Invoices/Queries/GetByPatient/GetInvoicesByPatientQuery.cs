using HealthCore.Application.Features.Invoices.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Invoices.Queries.GetByPatient;

public record GetInvoicesByPatientQuery(Guid PatientId) : IRequest<IEnumerable<InvoiceDto>>;
