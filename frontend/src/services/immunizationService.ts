import { apiRequest } from "./api"
import { Immunization, CreateImmunizationFormData, UpdateImmunizationFormData } from "@/types/clinical"

export const immunizationService = {
  getAll: () => apiRequest<Immunization[]>("api/Immunizations"),
  getById: (id: string) => apiRequest<Immunization>(`api/Immunizations/${id}`),
  getByPatientId: (patientId: string) => apiRequest<Immunization[]>(`api/Immunizations/patient/${patientId}`),

  create: (data: CreateImmunizationFormData) =>
    apiRequest<Immunization>("api/Immunizations", {
      method: "POST",
      body: {
        PatientId: data.patientId,
        VaccineName: data.vaccineName,
        DoseNumber: Number(data.doseNumber),
        ApplicationDate: data.applicationDate,
        NextDoseDate: data.nextDoseDate || null,
        BatchNumber: data.batchNumber || null,
        AdministeredBy: data.administeredBy || null,
        Notes: data.notes || null,
      },
    }),

  update: (id: string, data: UpdateImmunizationFormData) =>
    apiRequest<Immunization>(`api/Immunizations/${id}`, {
      method: "PUT",
      body: {
        VaccineName: data.vaccineName,
        DoseNumber: Number(data.doseNumber),
        ApplicationDate: data.applicationDate,
        NextDoseDate: data.nextDoseDate || null,
        BatchNumber: data.batchNumber || null,
        AdministeredBy: data.administeredBy || null,
        Notes: data.notes || null,
      },
    }),

  delete: (id: string) =>
    apiRequest<void>(`api/Immunizations/${id}`, { method: "DELETE" }),
}
