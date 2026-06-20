export enum AppointmentStatus {
  Scheduled = 1,
  Confirmed = 2,
  InProgress = 3,
  Completed = 4,
  Cancelled = 5,
  NoShow = 6,
}

export const APPOINTMENT_STATUS_CONFIG: Record<
  AppointmentStatus,
  { label: string; className: string }
> = {
  [AppointmentStatus.Scheduled]:  { label: "Programada",  className: "bg-blue-500/10 text-blue-600" },
  [AppointmentStatus.Confirmed]:  { label: "Confirmada",  className: "bg-green-500/10 text-green-600" },
  [AppointmentStatus.InProgress]: { label: "En Curso",    className: "bg-amber-500/10 text-amber-600" },
  [AppointmentStatus.Completed]:  { label: "Completada",  className: "bg-accent/10 text-accent" },
  [AppointmentStatus.Cancelled]:  { label: "Cancelada",   className: "bg-red-500/10 text-red-600" },
  [AppointmentStatus.NoShow]:     { label: "No Asistió",  className: "bg-zinc-500/10 text-zinc-600" },
}

export interface Appointment {
  id: string
  patientId: string
  patientName: string
  doctorId: string
  doctorName: string
  appointmentDate: string
  reason: string
  notes?: string | null
  status: AppointmentStatus
}

export interface CreateAppointmentDto {
  patientId: string
  doctorId: string
  appointmentDate: string
  reason: string
  notes?: string
}

export interface UpdateAppointmentDto {
  appointmentDate: string
  reason: string
  notes?: string
}

export interface AppointmentFormData {
  patientId: string
  doctorId: string
  appointmentDate: string
  reason: string
  notes: string
}
