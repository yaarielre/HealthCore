using AutoMapper;
using HealthCore.Application.Features.Patients.DTOs;
using HealthCore.Domain.Entities;

namespace HealthCore.Application.Features.Patients;

public class PatientMappingProfile : Profile
{
    public PatientMappingProfile()
    {
        CreateMap<Patient, PatientDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.UtcNow.Year - src.BirthDate.Year - (DateTime.UtcNow.DayOfYear < src.BirthDate.DayOfYear ? 1 : 0)))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));

        CreateMap<Patient, PatientSearchDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

        CreateMap<Patient, PatientMedicalHistoryDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.Appointments, opt => opt.MapFrom(src => src.Appointments))
            .ForMember(dest => dest.Consultations, opt => opt.MapFrom(src => src.MedicalConsultations))
            .ForMember(dest => dest.Prescriptions, opt => opt.MapFrom(src => src.Prescriptions));

        CreateMap<Appointment, MedicalHistoryAppointmentDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => $"{src.Doctor.FirstName} {src.Doctor.LastName}"));

        CreateMap<MedicalConsultation, MedicalHistoryConsultationDto>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => $"{src.Doctor.FirstName} {src.Doctor.LastName}"));

        CreateMap<Prescription, MedicalHistoryPrescriptionDto>()
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => $"{src.Doctor.FirstName} {src.Doctor.LastName}"));

        CreateMap<CreatePatientDto, Patient>();

        CreateMap<UpdatePatientDto, Patient>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IdNumber, opt => opt.Ignore())
            .ForMember(dest => dest.BirthDate, opt => opt.Ignore());
    }
}