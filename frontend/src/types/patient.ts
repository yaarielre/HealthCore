export interface Patient {
  id: string
  firstName: string
  lastName: string
  cedula: string
  birthDate: string
  phone: string
  address: string
  isActive: boolean
  createdAt: string
}

export interface CreatePatientDto {
  firstName: string
  lastName: string
  cedula: string
  birthDate: string
  phone: string
  address: string
}

export interface UpdatePatientDto {
  firstName: string
  lastName: string
  phone: string
  address: string
}

export interface PatientFormData {
  firstName: string
  lastName: string
  cedula: string
  birthDate: string
  phone: string
  address: string
}
