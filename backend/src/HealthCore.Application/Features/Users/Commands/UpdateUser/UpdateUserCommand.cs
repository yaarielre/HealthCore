using HealthCore.Application.Features.Users.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Users.Commands.UpdateUser;
public record UpdateUserCommand(Guid Id, UpdateUserDto Dto) : IRequest<UserDto>;

