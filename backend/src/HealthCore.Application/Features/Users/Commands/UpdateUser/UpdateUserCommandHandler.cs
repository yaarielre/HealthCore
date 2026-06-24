using AutoMapper;
using MediatR;
using HealthCore.Application.Features.Users.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Usuario con Id '{request.Id}' no encontrado.");

        user.FirstName = request.Dto.FirstName;
        user.LastName = request.Dto.LastName;
        user.Phone = request.Dto.Phone;
        user.Role = request.Dto.Role;
        user.DoctorId = request.Dto.DoctorId;
        user.UpdateAt = DateTime.UtcNow;

        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserDto>(user);
    }
}