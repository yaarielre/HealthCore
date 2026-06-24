using MediatR;
using HealthCore.Application.Features.Auth.DTOs;
using HealthCore.Domain.Interfaces;
using HealthCore.Application.Settings;
using HealthCore.Domain.Enums;
using Microsoft.Extensions.Options;
using HealthCore.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace HealthCore.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    private readonly JwtSettings _settings;
    private readonly ILogger<LoginCommandHandler> _logger;

    public LoginCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService, IOptions<JwtSettings> settings, ILogger<LoginCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(request.Dto.Email);

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Dto.Password, user.PasswordHash))
        {
            _logger.LogWarning("Intento de login fallido para {Email}", request.Dto.Email);
            throw new UnauthorizedAccessException("Credenciales incorrectas.");
        }

        if (user.Status != AccountStatus.Active)
        {
            _logger.LogWarning("Login bloqueado para {Email}: cuenta {Status}", request.Dto.Email, user.Status);
            throw new UnauthorizedAccessException("La cuenta no está activa.");
        }

        var token = _jwtService.GenerateToken(user);
        _logger.LogInformation("Login exitoso: {Email} ({Role})", user.Email, user.Role);

        return new AuthResponseDto(
            Token: token,
            FullName: $"{user.FirstName} {user.LastName}",
            Email: user.Email,
            Role: user.Role,
            ExpiresAt: DateTime.UtcNow.AddMinutes(_settings.ExpirationInMinutes)
        );
    }
}
