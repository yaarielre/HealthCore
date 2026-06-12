
using HealthCore.Application.Features.Users.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand(CreateUserDto Dto): IRequest<UserDto>;
