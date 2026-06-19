import { UserRole } from "./auth"

export interface StaffMember {
  id: string
  firstName: string
  lastName: string
  idNumber: string
  email: string
  phone: string
  role: UserRole
  status: number
  doctorId: string | null
}

export interface StaffFormData {
  firstName: string
  lastName: string
  idNumber: string
  email: string
  password?: string
  phone: string
  role: UserRole
}

export interface PasswordFormData {
  userId: string
  userName: string
  newPassword: string
}
