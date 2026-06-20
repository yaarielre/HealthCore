"use client"

import { Calendar, Edit2, MapPin, Phone, Trash2, Users } from "lucide-react"
import { Patient } from "@/types/patient"

interface PatientTableProps {
  patients: Patient[]
  isLoading: boolean
  onEdit: (patient: Patient) => void
  onDelete: (patient: Patient) => void
}

function calculateAge(birthDate: string): number {
  const today = new Date()
  const birth = new Date(birthDate)
  let age = today.getFullYear() - birth.getFullYear()
  const monthDiff = today.getMonth() - birth.getMonth()
  if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birth.getDate())) age--
  return age
}

function formatDate(dateStr: string): string {
  return new Date(dateStr).toLocaleDateString("es-DO", {
    day: "2-digit",
    month: "short",
    year: "numeric",
  })
}

function PatientRow({ patient, onEdit, onDelete }: { patient: Patient; onEdit: (p: Patient) => void; onDelete: (p: Patient) => void }) {
  const initials = `${patient.firstName[0]}${patient.lastName[0]}`.toUpperCase()
  const age = calculateAge(patient.birthDate)

  return (
    <tr className="border-b border-border/50 hover:bg-accent/5 transition-colors">
      <td className="px-4 py-3.5">
        <div className="flex items-center gap-3">
          <span className="flex size-9 shrink-0 items-center justify-center rounded-full bg-accent/10 text-accent text-sm font-bold">
            {initials}
          </span>
          <div>
            <p className="text-sm font-semibold text-foreground">{patient.firstName} {patient.lastName}</p>
            <p className="text-xs text-muted-foreground">{patient.cedula}</p>
          </div>
        </div>
      </td>
      <td className="px-4 py-3.5 text-sm text-muted-foreground">
        <div className="flex items-center gap-1.5">
          <Calendar className="size-3.5 shrink-0" />
          {formatDate(patient.birthDate)} <span className="text-xs font-medium text-foreground">({age} años)</span>
        </div>
      </td>
      <td className="px-4 py-3.5 text-sm text-muted-foreground">
        <div className="flex items-center gap-1.5">
          <Phone className="size-3.5 shrink-0" />
          {patient.phone}
        </div>
      </td>
      <td className="px-4 py-3.5 text-sm text-muted-foreground">
        <div className="flex items-center gap-1.5 max-w-xs">
          <MapPin className="size-3.5 shrink-0" />
          <span className="truncate">{patient.address}</span>
        </div>
      </td>
      <td className="px-4 py-3.5">
        <span className={`inline-flex items-center rounded-full px-2.5 py-0.5 text-xs font-semibold ${patient.isActive ? "bg-green-500/10 text-green-600" : "bg-red-500/10 text-red-600"}`}>
          {patient.isActive ? "Activo" : "Inactivo"}
        </span>
      </td>
      <td className="px-4 py-3.5">
        <div className="flex items-center gap-1">
          <button
            onClick={() => onEdit(patient)}
            className="rounded-lg p-1.5 text-muted-foreground hover:bg-accent/10 hover:text-accent transition-colors"
          >
            <Edit2 className="size-4" />
          </button>
          <button
            onClick={() => onDelete(patient)}
            className="rounded-lg p-1.5 text-muted-foreground hover:bg-destructive/10 hover:text-destructive transition-colors"
          >
            <Trash2 className="size-4" />
          </button>
        </div>
      </td>
    </tr>
  )
}

function EmptyState() {
  return (
    <tr>
      <td colSpan={6}>
        <div className="flex flex-col items-center justify-center py-16 text-center">
          <span className="flex size-14 items-center justify-center rounded-full bg-accent/10 text-accent">
            <Users className="size-7" />
          </span>
          <p className="mt-3 text-sm font-semibold text-foreground">Sin pacientes registrados</p>
          <p className="mt-1 text-xs text-muted-foreground">Registra el primer paciente con el botón &quot;Nuevo Paciente&quot;.</p>
        </div>
      </td>
    </tr>
  )
}

function LoadingState() {
  return (
    <>
      {Array.from({ length: 5 }).map((_, i) => (
        <tr key={i} className="border-b border-border/50">
          {Array.from({ length: 6 }).map((_, j) => (
            <td key={j} className="px-4 py-3.5">
              <div className="h-4 w-full animate-pulse rounded bg-accent/10" />
            </td>
          ))}
        </tr>
      ))}
    </>
  )
}

export function PatientTable({ patients, isLoading, onEdit, onDelete }: PatientTableProps) {
  return (
    <div className="overflow-hidden rounded-2xl border border-border bg-card shadow-sm">
      <table className="w-full text-left">
        <thead>
          <tr className="border-b border-border bg-accent/5">
            <th className="px-4 py-3 text-xs font-semibold uppercase tracking-wider text-muted-foreground">Paciente</th>
            <th className="px-4 py-3 text-xs font-semibold uppercase tracking-wider text-muted-foreground">Fecha de Nac.</th>
            <th className="px-4 py-3 text-xs font-semibold uppercase tracking-wider text-muted-foreground">Teléfono</th>
            <th className="px-4 py-3 text-xs font-semibold uppercase tracking-wider text-muted-foreground">Dirección</th>
            <th className="px-4 py-3 text-xs font-semibold uppercase tracking-wider text-muted-foreground">Estado</th>
            <th className="px-4 py-3 text-xs font-semibold uppercase tracking-wider text-muted-foreground">Acciones</th>
          </tr>
        </thead>
        <tbody>
          {isLoading ? (
            <LoadingState />
          ) : patients.length === 0 ? (
            <EmptyState />
          ) : (
            patients.map((patient) => (
              <PatientRow key={patient.id} patient={patient} onEdit={onEdit} onDelete={onDelete} />
            ))
          )}
        </tbody>
      </table>
    </div>
  )
}
