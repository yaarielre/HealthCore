import { clsx, type ClassValue } from 'clsx'
import { twMerge } from 'tailwind-merge'
import { UserRole } from "@/types/auth"

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

const ROLE_LABELS: Record<number, string> = {
  [UserRole.Administrator]: "Administrador",
  [UserRole.Manager]: "Administrador Clínico",
  [UserRole.Doctor]: "Médico / Doctor",
  [UserRole.Nurse]: "Enfermero/a",
  [UserRole.Receptionist]: "Recepcionista",
  [UserRole.Cashier]: "Cajero/a",
  [UserRole.Laboratory]: "Laboratorio",
  [UserRole.Pharmacy]: "Farmacia",
}

export function getRoleLabel(role?: number): string {
  return role ? ROLE_LABELS[role] || "Personal" : "Personal"
}

export function getErrorMessage(err: unknown, fallback: string): string {
  return err instanceof Error ? err.message : fallback
}
