using AutoMapper;
using MediatR;
using HealthCore.Application.Features.Users.DTOs;
using HealthCore.Application.Interfaces;

namespace HealthCore.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Usuario con Id '{request.Id}' no encontrado.");

        return _mapper.Map<UserDto>(user);
    }
}