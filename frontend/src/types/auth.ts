export enum UserRole {
  Administrator = 1,
  Manager = 2,
  Doctor = 3,
  Nurse = 4,
  Receptionist = 5,
  Cashier = 6,
  Laboratory = 7,
  Pharmacy = 8
}

export interface LoginDto {
  email: string
  password: string
}

export interface RegisterDto {
  firstName: string
  lastName: string
  idNumber: string
  email: string
  password: string
  phone: string
  role: UserRole
  doctorId?: string | null
}

export interface AuthResponseDto {
  token: string
  id: string
  fullName: string
  email: string
  role: UserRole
  expiresAt: string
}

export interface AuthUser {
  id: string
  fullName: string
  email: string
  role: UserRole
}
