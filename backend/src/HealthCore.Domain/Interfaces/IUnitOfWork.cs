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
    ISystemConfigurationRepository SystemConfigurations { get; }
    IMedicalRecordRepository MedicalRecords { get; }
    IMedicalHistoryItemRepository MedicalHistoryItems { get; }
    IImmunizationRepository Immunizations { get; }
    IMedicalImageRepository MedicalImages { get; }
    IOrderRepository Orders { get; }
    IOrderItemRepository OrderItems { get; }
    IOrderTypeRepository OrderTypes { get; }
    IInvoiceRepository Invoices { get; }
    IInvoiceItemRepository InvoiceItems { get; }
    IPaymentRepository Payments { get; }
    IInsuranceClaimRepository InsuranceClaims { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}