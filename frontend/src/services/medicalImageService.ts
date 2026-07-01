import { apiRequest } from "./api"
import { MedicalImage, CreateMedicalImageFormData } from "@/types/clinical"

export const medicalImageService = {
  getAll: () => apiRequest<MedicalImage[]>("api/MedicalImages"),
  getById: (id: string) => apiRequest<MedicalImage>(`api/MedicalImages/${id}`),
  getByPatientId: (patientId: string) => apiRequest<MedicalImage[]>(`api/MedicalImages/patient/${patientId}`),

  create: (data: CreateMedicalImageFormData) =>
    apiRequest<MedicalImage>("api/MedicalImages", {
      method: "POST",
      body: {
        PatientId: data.patientId,
        MedicalConsultationId: data.medicalConsultationId || null,
        ImageType: data.imageType,
        BodyPart: data.bodyPart || null,
        FileName: data.fileName,
        FilePath: data.filePath,
        ContentType: data.contentType,
        FileSizeBytes: Number(data.fileSizeBytes) || 0,
        Description: data.description || null,
        Interpretation: data.interpretation || null,
        InterpretedById: null,
        TakenAt: data.takenAt || null,
      },
    }),

  update: (id: string, data: Partial<CreateMedicalImageFormData> & { interpretation?: string }) =>
    apiRequest<MedicalImage>(`api/MedicalImages/${id}`, {
      method: "PUT",
      body: {
        ImageType: data.imageType,
        BodyPart: data.bodyPart || null,
        FileName: data.fileName,
        FilePath: data.filePath,
        ContentType: data.contentType,
        FileSizeBytes: Number(data.fileSizeBytes) || 0,
        Description: data.description || null,
        Interpretation: data.interpretation || null,
        InterpretedById: null,
        TakenAt: data.takenAt || null,
      },
    }),

  delete: (id: string) =>
    apiRequest<void>(`api/MedicalImages/${id}`, { method: "DELETE" }),
}
