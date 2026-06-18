using MediatR;
using HealthCore.Application.Features.Auth.DTOs;
using HealthCore.Application.Interfaces;
using HealthCore.Application.Settings;
using HealthCore.Domain.Enums;
using Microsoft.Extensions.Options;

namespace HealthCore.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    private readonly JwtSettings _settings;

    public LoginCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService, IOptions<JwtSettings> settings)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _settings = settings.Value;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(request.Dto.Email)
            ?? throw new UnauthorizedAccessException("Credenciales incorrectas.");

        if (user.Status != AccountStatus.Active)
            throw new UnauthorizedAccessException("La cuenta no está activa.");

        var passwordValid = BCrypt.Net.BCrypt.Verify(request.Dto.Password, user.PasswordHash);
        if (!passwordValid)
            throw new UnauthorizedAccessException("Credenciales incorrectas.");

        var token = _jwtService.GenerateToken(user);

        return new AuthResponseDto(
            Token: token,
            FullName: $"{user.FirstName} {user.LastName}",
            Email: user.Email,
            Role: user.Role,
            ExpiresAt: DateTime.UtcNow.AddMinutes(_settings.ExpirationInMinutes)
        );
    }
}
