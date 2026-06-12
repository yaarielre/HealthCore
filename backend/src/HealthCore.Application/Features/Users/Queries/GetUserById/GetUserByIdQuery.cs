using MediatR;
using HealthCore.Application.Features.Users.DTOs;

namespace HealthCore.Application.Features.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;