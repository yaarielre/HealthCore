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
    public DbSet<MedicalConsultation> MedicalConsultations => Set<MedicalConsultation>();
    public DbSet<VitalSign> VitalSigns => Set<VitalSign>();
    public DbSet<Prescription> Prescriptions => Set<Prescription>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HealthCoreDbContext).Assembly);
    }
}