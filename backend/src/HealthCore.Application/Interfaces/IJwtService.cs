using HealthCore.Domain.Entities;

namespace HealthCore.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}