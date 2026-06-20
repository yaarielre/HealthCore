using HealthCore.Application.Features.Users.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Users.Queries.GetAllLogs;

public record GetAllLogsQuery() : IRequest<IEnumerable<LogDto>>;
