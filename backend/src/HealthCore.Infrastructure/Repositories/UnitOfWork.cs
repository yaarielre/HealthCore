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
    public ISystemConfigurationRepository SystemConfigurations { get; }
    public IMedicalRecordRepository MedicalRecords { get; }
    public IMedicalHistoryItemRepository MedicalHistoryItems { get; }
    public IImmunizationRepository Immunizations { get; }
    public IMedicalImageRepository MedicalImages { get; }
    public IOrderRepository Orders { get; }
    public IOrderItemRepository OrderItems { get; }
    public IOrderTypeRepository OrderTypes { get; }
    public IInvoiceRepository Invoices { get; }
    public IInvoiceItemRepository InvoiceItems { get; }
    public IPaymentRepository Payments { get; }
    public IInsuranceClaimRepository InsuranceClaims { get; }

    public UnitOfWork(
        HealthCoreDbContext context,
        IPatientRepository patients,
        IUserRepository users,
        ISpecialtyRepository specialties,
        IDoctorRepository doctors,
        IAppointmentRepository appointments,
        IMedicalConsultationRepository medicalConsultations,
        IPrescriptionRepository prescriptions,
        ISystemConfigurationRepository systemConfigurations,
        IMedicalRecordRepository medicalRecords,
        IMedicalHistoryItemRepository medicalHistoryItems,
        IImmunizationRepository immunizations,
        IMedicalImageRepository medicalImages,
        IOrderRepository orders,
        IOrderItemRepository orderItems,
        IOrderTypeRepository orderTypes,
        IInvoiceRepository invoices,
        IInvoiceItemRepository invoiceItems,
        IPaymentRepository payments,
        IInsuranceClaimRepository insuranceClaims)
    {
        _context = context;

        Patients = patients;
        Users = users;
        Specialties = specialties;
        Doctors = doctors;
        Appointments = appointments;
        MedicalConsultations = medicalConsultations;
        Prescriptions = prescriptions;
        SystemConfigurations = systemConfigurations;
        MedicalRecords = medicalRecords;
        MedicalHistoryItems = medicalHistoryItems;
        Immunizations = immunizations;
        MedicalImages = medicalImages;
        Orders = orders;
        OrderItems = orderItems;
        OrderTypes = orderTypes;
        Invoices = invoices;
        InvoiceItems = invoiceItems;
        Payments = payments;
        InsuranceClaims = insuranceClaims;
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