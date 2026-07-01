using MediatR;
using HealthCore.Application.Features.Auth.DTOs;
using HealthCore.Domain.Interfaces;
using HealthCore.Application.Settings;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Enums;
using Microsoft.Extensions.Options;
using HealthCore.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace HealthCore.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    private readonly JwtSettings _settings;
    private readonly ILogger<RegisterCommandHandler> _logger;

    public RegisterCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService, IOptions<JwtSettings> settings, ILogger<RegisterCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var emailExists = await _unitOfWork.Users.EmailExistsAsync(request.Dto.Email);
        if (emailExists)
            throw new InvalidOperationException($"El correo '{request.Dto.Email}' ya está registrado.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.Dto.FirstName,
            LastName = request.Dto.LastName,
            IdNumber = request.Dto.IdNumber,
            Email = request.Dto.Email,
            Phone = request.Dto.Phone,
            Role = request.Dto.Role,
            DoctorId = request.Dto.DoctorId,
            Status = AccountStatus.Active,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Dto.Password)
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Usuario registrado: {Email} ({Role})", user.Email, user.Role);

        var token = _jwtService.GenerateToken(user);

        return new AuthResponseDto(
            Token: token,
            Id: user.Id,
            FullName: $"{user.FirstName} {user.LastName}",
            Email: user.Email,
            Role: user.Role,
            ExpiresAt: DateTime.UtcNow.AddMinutes(_settings.ExpirationInMinutes)
        );
    }
}