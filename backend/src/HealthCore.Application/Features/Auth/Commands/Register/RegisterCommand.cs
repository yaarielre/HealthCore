using MediatR;
using HealthCore.Application.Features.Auth.DTOs;

namespace HealthCore.Application.Features.Auth.Commands.Register;

public record RegisterCommand(RegisterDto Dto) : IRequest<AuthResponseDto>;