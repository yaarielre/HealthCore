"use client"

import { Calendar, Clock, Edit2, Stethoscope, SwitchCamera, User2, CalendarX } from "lucide-react"
import { Appointment, AppointmentStatus, APPOINTMENT_STATUS_CONFIG } from "@/types/appointment"

interface AppointmentTableProps {
  appointments: Appointment[]
  isLoading: boolean
  onEdit: (appointment: Appointment) => void
  onChangeStatus: (appointment: Appointment) => void
}

function formatDate(dateStr: string): string {
  return new Date(dateStr).toLocaleDateString("es-DO", {
    weekday: "short",
    day: "2-digit",
    month: "short",
    year: "numeric",
  })
}

function formatTime(dateStr: string): string {
  return new Date(dateStr).toLocaleTimeString("es-DO", {
    hour: "2-digit",
    minute: "2-digit",
  })
}

function StatusBadge({ status }: { status: AppointmentStatus }) {
  const config = APPOINTMENT_STATUS_CONFIG[status]
  return (
    <span className={`inline-flex items-center rounded-full px-2.5 py-0.5 text-xs font-semibold ${config.className}`}>
      {config.label}
    </span>
  )
}

function AppointmentRow({
  appointment,
  onEdit,
  onChangeStatus,
}: {
  appointment: Appointment
  onEdit: (a: Appointment) => void
  onChangeStatus: (a: Appointment) => void
}) {
  const isTerminal =
    appointment.status === AppointmentStatus.Completed ||
    appointment.status === AppointmentStatus.Cancelled ||
    appointment.status === AppointmentStatus.NoShow

  return (
    <tr className="border-b border-border/50 hover:bg-accent/5 transition-colors">
      <td className="px-4 py-3.5">
        <div className="flex items-center gap-2.5">
          <span className="flex size-8 shrink-0 items-center justify-center rounded-full bg-accent/10 text-accent">
            <User2 className="size-4" />
          </span>
          <span className="text-sm font-semibold text-foreground">{appointment.patientName}</span>
        </div>
      </td>
      <td className="px-4 py-3.5">
        <div className="flex items-center gap-1.5 text-sm text-muted-foreground">
          <Stethoscope className="size-3.5 shrink-0" />
          {appointment.doctorName}
        </div>
      </td>
      <td className="px-4 py-3.5">
        <div className="flex flex-col gap-0.5">
          <div className="flex items-center gap-1.5 text-sm text-foreground font-medium">
            <Calendar className="size-3.5 shrink-0 text-muted-foreground" />
            {formatDate(appointment.appointmentDate)}
          </div>
          <div className="flex items-center gap-1.5 text-xs text-muted-foreground pl-5">
            <Clock className="size-3 shrink-0" />
            {formatTime(appointment.appointmentDate)}
          </div>
        </div>
      </td>
      <td className="px-4 py-3.5">
        <p className="text-sm text-foreground max-w-xs truncate">{appointment.reason}</p>
        {appointment.notes && (
          <p className="text-xs text-muted-foreground truncate max-w-xs mt-0.5">{appointment.notes}</p>
        )}
      </td>
      <td className="px-4 py-3.5">
        <StatusBadge status={appointment.status} />
      </td>
      <td className="px-4 py-3.5">
        <div className="flex items-center gap-1">
          {!isTerminal && (
            <>
              <button
                onClick={() => onEdit(appointment)}
                className="rounded-lg p-1.5 text-muted-foreground hover:bg-accent/10 hover:text-accent transition-colors"
                title="Editar cita"
              >
                <Edit2 className="size-4" />
              </button>
              <button
                onClick={() => onChangeStatus(appointment)}
                className="rounded-lg p-1.5 text-muted-foreground hover:bg-accent/10 hover:text-accent transition-colors"
                title="Cambiar estado"
              >
                <SwitchCamera className="size-4" />
              </button>
            </>
          )}
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
            <CalendarX className="size-7" />
          </span>
          <p className="mt-3 text-sm font-semibold text-foreground">Sin citas registradas</p>
          <p className="mt-1 text-xs text-muted-foreground">Programa la primera cita con el botón &quot;Nueva Cita&quot;.</p>
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

export function AppointmentTable({ appointments, isLoading, onEdit, onChangeStatus }: AppointmentTableProps) {
  return (
    <div className="overflow-hidden rounded-2xl border border-border bg-card shadow-sm">
      <table className="w-full text-left">
        <thead>
          <tr className="border-b border-border bg-accent/5">
            <th className="px-4 py-3 text-xs font-semibold uppercase tracking-wider text-muted-foreground">Paciente</th>
            <th className="px-4 py-3 text-xs font-semibold uppercase tracking-wider text-muted-foreground">Doctor</th>
            <th className="px-4 py-3 text-xs font-semibold uppercase tracking-wider text-muted-foreground">Fecha / Hora</th>
            <th className="px-4 py-3 text-xs font-semibold uppercase tracking-wider text-muted-foreground">Motivo</th>
            <th className="px-4 py-3 text-xs font-semibold uppercase tracking-wider text-muted-foreground">Estado</th>
            <th className="px-4 py-3 text-xs font-semibold uppercase tracking-wider text-muted-foreground">Acciones</th>
          </tr>
        </thead>
        <tbody>
          {isLoading ? (
            <LoadingState />
          ) : appointments.length === 0 ? (
            <EmptyState />
          ) : (
            appointments.map((a) => (
              <AppointmentRow key={a.id} appointment={a} onEdit={onEdit} onChangeStatus={onChangeStatus} />
            ))
          )}
        </tbody>
      </table>
    </div>
  )
}
