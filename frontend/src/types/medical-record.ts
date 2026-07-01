export enum BloodType {
  APositive = 1,
  ANegative = 2,
  BPositive = 3,
  BNegative = 4,
  ABPositive = 5,
  ABNegative = 6,
  OPositive = 7,
  ONegative = 8
}

export enum MedicalHistoryCategory {
  FamilyHistory = 1,
  PastSurgery = 2,
  ChronicDisease = 3,
  Medication = 4,
  Allergy = 5,
  Immunization = 6,
  Lifestyle = 7
}

export interface MedicalRecord {
  id: string
  patientId: string
  patientName: string
  recordNumber: string
  bloodType: BloodType | null
  allergies: string | null
  emergencyContactName: string | null
  emergencyContactPhone: string | null
  notes: string | null
  isActive: boolean
  createdAt: string
  updatedAt: string | null
}

export interface CreateMedicalRecordFormData {
  patientId: string
  recordNumber: string
  bloodType: BloodType | ""
  allergies: string
  emergencyContactName: string
  emergencyContactPhone: string
  notes: string
}

export interface UpdateMedicalRecordFormData {
  recordNumber: string
  bloodType: BloodType | ""
  allergies: string
  emergencyContactName: string
  emergencyContactPhone: string
  notes: string
  isActive: boolean
}

export interface MedicalHistoryItem {
  id: string
  patientId: string
  patientName: string
  category: MedicalHistoryCategory
  description: string
  details: string | null
  recordedDate: string | null
  severity: number | null
  isActive: boolean
  recordedById: string
  recordedByName: string
  createdAt: string
  updatedAt: string | null
}

export interface CreateMedicalHistoryItemFormData {
  patientId: string
  category: MedicalHistoryCategory | ""
  description: string
  details: string
  recordedDate: string
  severity: number | ""
  recordedById: string
}

export interface UpdateMedicalHistoryItemFormData {
  category: MedicalHistoryCategory | ""
  description: string
  details: string
  recordedDate: string
  severity: number | ""
  isActive: boolean
}

export const BloodTypeMap: Record<number, string> = {
  [BloodType.APositive]: "A+",
  [BloodType.ANegative]: "A-",
  [BloodType.BPositive]: "B+",
  [BloodType.BNegative]: "B-",
  [BloodType.ABPositive]: "AB+",
  [BloodType.ABNegative]: "AB-",
  [BloodType.OPositive]: "O+",
  [BloodType.ONegative]: "O-"
}

export const MedicalHistoryCategoryMap: Record<number, string> = {
  [MedicalHistoryCategory.FamilyHistory]: "Antecedentes Familiares",
  [MedicalHistoryCategory.PastSurgery]: "Cirugías Previas",
  [MedicalHistoryCategory.ChronicDisease]: "Enfermedades Crónicas",
  [MedicalHistoryCategory.Medication]: "Medicación",
  [MedicalHistoryCategory.Allergy]: "Alergias",
  [MedicalHistoryCategory.Immunization]: "Vacunas",
  [MedicalHistoryCategory.Lifestyle]: "Estilo de Vida"
}
