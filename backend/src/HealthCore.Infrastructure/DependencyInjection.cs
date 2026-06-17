using HealthCore.Application.Interfaces;
using HealthCore.Application.Settings;
using HealthCore.Infrastructure.Persistence;
using HealthCore.Infrastructure.Repositories;
using HealthCore.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthCore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<HealthCoreDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.Configure<JwtSettings>(options =>
        {
            options.SecretKey = configuration["JwtSettings:SecretKey"] ?? string.Empty;
            options.Issuer = configuration["JwtSettings:Issuer"] ?? string.Empty;
            options.Audience = configuration["JwtSettings:Audience"] ?? string.Empty;
            options.ExpirationInMinutes = int.TryParse(
                configuration["JwtSettings:ExpirationInMinutes"],
                out var expirationInMinutes)
                    ? expirationInMinutes
                    : 0;
        });
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}
