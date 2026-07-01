export interface Prescription {
  id: string
  patientId: string
  patientName: string
  doctorId: string
  doctorName: string
  medicalConsultationId: string
  medication: string
  dosage: string
  frequency: string
  duration: string
  instructions?: string | null
  createdAt: string
}

export interface CreatePrescriptionFormData {
  patientId: string
  doctorId: string
  medicalConsultationId: string
  medication: string
  dosage: string
  frequency: string
  duration: string
  instructions: string
}

export interface UpdatePrescriptionFormData {
  doctorId: string
  medicalConsultationId: string
  medication: string
  dosage: string
  frequency: string
  duration: string
  instructions: string
}
