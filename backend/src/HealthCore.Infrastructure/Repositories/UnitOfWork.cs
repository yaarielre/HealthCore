using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using HealthCore.Infrastructure.Persistence;
using HealthCore.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly HealthCoreDbContext _context;

    public IPatientRepository Patients { get; }
    public IUserRepository Users { get; }
    public ISpecialtyRepository Specialties { get; }
    public IDoctorRepository Doctors { get; }
    public IAppointmentRepository Appointments { get; }
    public IMedicalConsultationRepository MedicalConsultations { get; }
    public IPrescriptionRepository Prescriptions { get; }

    public UnitOfWork(
        HealthCoreDbContext context,
        IPatientRepository patients,
        IUserRepository users,
        ISpecialtyRepository specialties,
        IDoctorRepository doctors,
        IAppointmentRepository appointments,
        IMedicalConsultationRepository medicalConsultations,
        IPrescriptionRepository prescriptions)
    {
        _context = context;

        Patients = patients;
        Users = users;
        Specialties = specialties;
        Doctors = doctors;
        Appointments = appointments;
        MedicalConsultations = medicalConsultations;
        Prescriptions = prescriptions;
    }

    public async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}