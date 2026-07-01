import { apiRequest } from "./api"
import { MedicalRecord, CreateMedicalRecordFormData, UpdateMedicalRecordFormData } from "@/types/medical-record"

export const medicalRecordService = {
  getAll: () => apiRequest<MedicalRecord[]>("api/MedicalRecords"),

  getById: (id: string) => apiRequest<MedicalRecord>(`api/MedicalRecords/${id}`),

  getByPatientId: (patientId: string) => apiRequest<MedicalRecord>(`api/MedicalRecords/patient/${patientId}`),

  create: (payload: Partial<CreateMedicalRecordFormData>) => 
    apiRequest<MedicalRecord>("api/MedicalRecords", {
      method: "POST",
      body: payload
    }),

  update: (id: string, payload: Partial<UpdateMedicalRecordFormData>) => 
    apiRequest<void>(`api/MedicalRecords/${id}`, {
      method: "PUT",
      body: payload
    }),

  delete: (id: string) => 
    apiRequest<void>(`api/MedicalRecords/${id}`, {
      method: "DELETE"
    })
}
