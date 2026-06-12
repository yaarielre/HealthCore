using AutoMapper;
using HealthCore.Application.Features.Users.DTOs;
using HealthCore.Application.Interfaces;
using HealthCore.Domain.Entities;
using HealthCore.Domain.enums;
using MediatR;

namespace HealthCore.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var emailExists = await _unitOfWork.Users.EmailExistsAsync(request.Dto.Email);
        if (emailExists)
            throw new InvalidOperationException($"El correo '{request.Dto.Email}' ya está registrado.");

        var user = _mapper.Map<User>(request.Dto);
        user.Id = Guid.NewGuid();
        user.Status = AccountStatus.Active;
        user.PasswordHash = string.Empty;

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserDto>(user);
    }
}