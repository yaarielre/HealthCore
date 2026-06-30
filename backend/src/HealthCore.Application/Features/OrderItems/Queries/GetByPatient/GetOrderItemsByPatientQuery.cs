using MediatR;
using HealthCore.Application.Features.OrderItems.DTOs;

namespace HealthCore.Application.Features.OrderItems.Queries.GetByPatient;

public record GetOrderItemsByPatientQuery(Guid PatientId) : IRequest<IEnumerable<OrderItemDto>>;
