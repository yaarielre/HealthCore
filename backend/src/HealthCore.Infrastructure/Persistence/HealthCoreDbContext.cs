using HealthCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthCore.Infrastructure.Persistence;

public class HealthCoreDbContext : DbContext
{
    public HealthCoreDbContext(DbContextOptions<HealthCoreDbContext> options)
        : base(options)
    {
    }

    public DbSet<Patient> Patients => Set<Patient>();

    public DbSet<Doctor> Doctors => Set<Doctor>();

    public DbSet<User> Users => Set<User>();

    public DbSet<UserActivityLog> UserActivityLogs => Set<UserActivityLog>();

    public DbSet<PasswordResetToken> PasswordResetTokens => Set<PasswordResetToken>();

    public DbSet<Specialty> Specialties => Set<Specialty>();

    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HealthCoreDbContext).Assembly);

        modelBuilder.Entity<Patient>().HasQueryFilter(p => !p.IsDeleted);
    }
}