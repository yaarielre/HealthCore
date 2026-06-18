using AutoMapper;
using HealthCore.Application.Features.Appointments.DTOs;
using AppointmentEntity = HealthCore.Domain.Entities.Appointment;


namespace HealthCore.Application.Features.Appointments;

public class AppointmentMappingProfile : Profile
{
    public AppointmentMappingProfile()
    {
        CreateMap<AppointmentEntity, AppointmentDto>()
            .ForCtorParam(nameof(AppointmentDto.PatientName), opt => opt.MapFrom(src =>
                src.Patient != null ? $"{src.Patient.FirstName} {src.Patient.LastName}" : string.Empty))
            .ForCtorParam(nameof(AppointmentDto.DoctorName), opt => opt.MapFrom(src =>
                src.Doctor != null ? $"{src.Doctor.FirstName} {src.Doctor.LastName}" : string.Empty));

        CreateMap<CreateAppointmentDto, AppointmentEntity>();
        CreateMap<UpdateAppointmentDto, AppointmentEntity>();
    }
}
