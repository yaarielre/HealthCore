export interface VitalSign {
  id?: string
  bloodPressure: string | null
  temperature: number | null
  weight: number | null
  height: number | null
  heartRate: number | null
  oxygenSaturation: number | null
}

export interface CreateVitalSignFormData {
  bloodPressure: string
  temperature: number | ""
  weight: number | ""
  height: number | ""
  heartRate: number | ""
  oxygenSaturation: number | ""
}

export interface MedicalConsultation {
  id: string
  patientId: string
  patientName: string
  doctorId: string
  doctorName: string
  appointmentId: string | null
  reasonForVisit: string
  symptoms: string | null
  diagnosis: string | null
  treatment: string | null
  observations: string | null
  internalNotes: string | null
  vitalSigns: VitalSign[]
  createdAt: string
}

export interface CreateMedicalConsultationFormData {
  patientId: string
  doctorId: string
  appointmentId: string | null
  reasonForVisit: string
  symptoms: string
  diagnosis: string
  treatment: string
  observations: string
  internalNotes: string
  vitalSigns: CreateVitalSignFormData
}

export interface UpdateMedicalConsultationFormData {
  doctorId: string
  appointmentId: string | null
  reasonForVisit: string
  symptoms: string
  diagnosis: string
  treatment: string
  observations: string
  internalNotes: string
  vitalSigns: CreateVitalSignFormData
}
