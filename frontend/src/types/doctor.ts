export interface Doctor {
  id: string
  firstName: string
  lastName: string
  email?: string
  licenseNumber: string
  specialtyId: string
  specialtyName: string
  isActive: boolean
}
