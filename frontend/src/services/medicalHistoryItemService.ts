import { apiRequest } from "./api"
import { MedicalHistoryItem, CreateMedicalHistoryItemFormData, UpdateMedicalHistoryItemFormData } from "@/types/medical-record"

export const medicalHistoryItemService = {
  getAll: () => apiRequest<MedicalHistoryItem[]>("api/MedicalHistoryItems"),

  getById: (id: string) => apiRequest<MedicalHistoryItem>(`api/MedicalHistoryItems/${id}`),

  getByPatientId: (patientId: string) => apiRequest<MedicalHistoryItem[]>(`api/MedicalHistoryItems/patient/${patientId}`),

  create: (payload: Partial<CreateMedicalHistoryItemFormData>) => 
    apiRequest<MedicalHistoryItem>("api/MedicalHistoryItems", {
      method: "POST",
      body: payload
    }),

  update: (id: string, payload: Partial<UpdateMedicalHistoryItemFormData>) => 
    apiRequest<void>(`api/MedicalHistoryItems/${id}`, {
      method: "PUT",
      body: payload
    }),

  delete: (id: string) => 
    apiRequest<void>(`api/MedicalHistoryItems/${id}`, {
      method: "DELETE"
    })
}
