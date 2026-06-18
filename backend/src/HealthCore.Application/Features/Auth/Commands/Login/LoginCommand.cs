using HealthCore.Application.Features.Auth.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Auth.Commands.Login;
public record LoginCommand(LoginDto Dto) : IRequest<AuthResponseDto>;