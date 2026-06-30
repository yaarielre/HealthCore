using MediatR;
using HealthCore.Application.Features.MedicalImages.DTOs;

namespace HealthCore.Application.Features.MedicalImages.Queries.GetByPatient;

public record GetMedicalImagesByPatientQuery(Guid PatientId) : IRequest<IEnumerable<MedicalImageDto>>;
