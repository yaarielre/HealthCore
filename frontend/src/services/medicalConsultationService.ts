import { apiRequest } from "./api"
import { 
  MedicalConsultation, 
  CreateMedicalConsultationFormData, 
  UpdateMedicalConsultationFormData 
} from "@/types/medical-consultation"

export const medicalConsultationService = {
  getAll: () => apiRequest<MedicalConsultation[]>("api/MedicalConsultations"),
  
  getById: (id: string) => apiRequest<MedicalConsultation>(`api/MedicalConsultations/${id}`),
  
  getByPatientId: (patientId: string) => apiRequest<MedicalConsultation[]>(`api/MedicalConsultations/patient/${patientId}`),
  
  create: (data: CreateMedicalConsultationFormData) => {
    const payload = {
      ...data,
      vitalSigns: {
        bloodPressure: data.vitalSigns.bloodPressure || null,
        temperature: data.vitalSigns.temperature === "" ? null : data.vitalSigns.temperature,
        weight: data.vitalSigns.weight === "" ? null : data.vitalSigns.weight,
        height: data.vitalSigns.height === "" ? null : data.vitalSigns.height,
        heartRate: data.vitalSigns.heartRate === "" ? null : data.vitalSigns.heartRate,
        oxygenSaturation: data.vitalSigns.oxygenSaturation === "" ? null : data.vitalSigns.oxygenSaturation,
      }
    }
    
    return apiRequest<MedicalConsultation>("api/MedicalConsultations", {
      method: "POST",
      body: payload
    })
  },
    
  update: (id: string, data: UpdateMedicalConsultationFormData) => {
    const payload = {
      ...data,
      vitalSigns: {
        bloodPressure: data.vitalSigns.bloodPressure || null,
        temperature: data.vitalSigns.temperature === "" ? null : data.vitalSigns.temperature,
        weight: data.vitalSigns.weight === "" ? null : data.vitalSigns.weight,
        height: data.vitalSigns.height === "" ? null : data.vitalSigns.height,
        heartRate: data.vitalSigns.heartRate === "" ? null : data.vitalSigns.heartRate,
        oxygenSaturation: data.vitalSigns.oxygenSaturation === "" ? null : data.vitalSigns.oxygenSaturation,
      }
    }
    
    return apiRequest<MedicalConsultation>(`api/MedicalConsultations/${id}`, {
      method: "PUT",
      body: payload
    })
  },
    
  delete: (id: string) =>
    apiRequest<void>(`api/MedicalConsultations/${id}`, {
      method: "DELETE"
    })
}
