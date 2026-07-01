import { apiRequest, getApiBaseUrl } from "./api"
import { Prescription, CreatePrescriptionFormData, UpdatePrescriptionFormData } from "@/types/prescription"

export const prescriptionService = {
  getAll: () => apiRequest<Prescription[]>("api/Prescriptions"),

  getById: (id: string) => apiRequest<Prescription>(`api/Prescriptions/${id}`),

  getByPatientId: (patientId: string) =>
    apiRequest<Prescription[]>(`api/Prescriptions/patient/${patientId}`),

  create: (data: CreatePrescriptionFormData) =>
    apiRequest<Prescription>("api/Prescriptions", {
      method: "POST",
      body: {
        PatientId: data.patientId,
        DoctorId: data.doctorId,
        MedicalConsultationId: data.medicalConsultationId,
        Medication: data.medication,
        Dosage: data.dosage,
        Frequency: data.frequency,
        Duration: data.duration,
        Instructions: data.instructions || null,
      },
    }),

  update: (id: string, data: UpdatePrescriptionFormData) =>
    apiRequest<Prescription>(`api/Prescriptions/${id}`, {
      method: "PUT",
      body: {
        DoctorId: data.doctorId,
        MedicalConsultationId: data.medicalConsultationId,
        Medication: data.medication,
        Dosage: data.dosage,
        Frequency: data.frequency,
        Duration: data.duration,
        Instructions: data.instructions || null,
      },
    }),

  delete: (id: string) =>
    apiRequest<void>(`api/Prescriptions/${id}`, { method: "DELETE" }),

  downloadPdf: async (id: string, patientName: string): Promise<void> => {
    const token = typeof window !== "undefined" ? localStorage.getItem("healthcore_token") : null
    const baseUrl = getApiBaseUrl()
    const response = await fetch(`${baseUrl}/api/Prescriptions/${id}/pdf`, {
      headers: { Authorization: `Bearer ${token}` },
    })
    if (!response.ok) throw new Error("No se pudo generar el PDF")
    const blob = await response.blob()
    const url = URL.createObjectURL(blob)
    const a = document.createElement("a")
    a.href = url
    a.download = `receta_${patientName.replace(/\s+/g, "_")}.pdf`
    a.click()
    URL.revokeObjectURL(url)
  },
}
