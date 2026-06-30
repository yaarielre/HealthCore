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
    public DbSet<SystemConfiguration> SystemConfigurations => Set<SystemConfiguration>();
    public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();
    public DbSet<MedicalHistoryItem> MedicalHistoryItems => Set<MedicalHistoryItem>();
    public DbSet<Immunization> Immunizations => Set<Immunization>();
    public DbSet<MedicalImage> MedicalImages => Set<MedicalImage>();
    public DbSet<OrderType> OrderTypes => Set<OrderType>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<InsuranceClaim> InsuranceClaims => Set<InsuranceClaim>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HealthCoreDbContext).Assembly);
    }
}