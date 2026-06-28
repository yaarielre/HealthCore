namespace HealthCore.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IPatientRepository Patients { get; }
    IUserRepository Users { get; }
    IAppointmentRepository Appointments { get; }
    ISpecialtyRepository Specialties { get; }
    IDoctorRepository Doctors { get; }
    IMedicalConsultationRepository MedicalConsultations { get; }
    IPrescriptionRepository Prescriptions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}