using AutoMapper;
using HealthCore.Application.Features.Users.DTOs;
using HealthCore.Domain.Entities;

namespace HealthCore.Application.Features.Users;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>();
    }
}