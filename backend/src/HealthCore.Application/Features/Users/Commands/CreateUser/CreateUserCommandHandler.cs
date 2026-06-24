using AutoMapper;
using HealthCore.Application.Features.Users.DTOs;
using HealthCore.Domain.Interfaces;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HealthCore.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateUserCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var emailExists = await _unitOfWork.Users.EmailExistsAsync(request.Dto.Email);
        if (emailExists)
            throw new InvalidOperationException($"El correo '{request.Dto.Email}' ya está registrado.");

        var user = _mapper.Map<User>(request.Dto);
        user.Id = Guid.NewGuid();
        user.Status = AccountStatus.Active;
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Dto.Password);

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Usuario creado: {Email} ({Role}) por admin", user.Email, user.Role);
        return _mapper.Map<UserDto>(user);
    }
}
