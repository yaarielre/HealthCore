using MediatR;
using HealthCore.Application.Features.Orders.DTOs;

namespace HealthCore.Application.Features.Orders.Queries.GetByPatient;

public record GetOrdersByPatientQuery(Guid PatientId) : IRequest<IEnumerable<OrderDto>>;
