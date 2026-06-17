using MediatR;
using HealthCore.Application.Features.Auth.DTOs;
using HealthCore.Application.Interfaces;
using HealthCore.Application.Settings;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Enums;
using Microsoft.Extensions.Options;

namespace HealthCore.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    private readonly JwtSettings _settings;

    public RegisterCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService, IOptions<JwtSettings> settings)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _settings = settings.Value;
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