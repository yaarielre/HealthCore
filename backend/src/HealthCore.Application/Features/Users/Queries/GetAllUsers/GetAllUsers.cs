using HealthCore.Application.Features.Users.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Users.Queries.GetAllUsers;

public record GetAllUsersQuery : IRequest<IEnumerable<UserDto>>;
