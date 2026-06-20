"use client"

import { Loader2, X } from "lucide-react"
import { Appointment, AppointmentStatus, APPOINTMENT_STATUS_CONFIG } from "@/types/appointment"

interface ChangeStatusModalProps {
  appointment: Appointment
  isLoading: boolean
  onClose: () => void
  onConfirm: (status: AppointmentStatus) => void
}

const TRANSITION_MAP: Record<AppointmentStatus, AppointmentStatus[]> = {
  [AppointmentStatus.Scheduled]:  [AppointmentStatus.Confirmed, AppointmentStatus.Cancelled, AppointmentStatus.NoShow],
  [AppointmentStatus.Confirmed]:  [AppointmentStatus.InProgress, AppointmentStatus.Cancelled, AppointmentStatus.NoShow],
  [AppointmentStatus.InProgress]: [AppointmentStatus.Completed, AppointmentStatus.Cancelled],
  [AppointmentStatus.Completed]:  [],
  [AppointmentStatus.Cancelled]:  [],
  [AppointmentStatus.NoShow]:     [],
}

export function ChangeStatusModal({ appointment, isLoading, onClose, onConfirm }: ChangeStatusModalProps) {
  const availableStatuses = TRANSITION_MAP[appointment.status]

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-background/60 backdrop-blur-sm p-4">
      <div className="w-full max-w-sm rounded-2xl border border-border bg-card shadow-xl shadow-black/10">
        <div className="flex items-center justify-between border-b border-border px-6 py-4">
          <h2 className="text-base font-bold text-foreground">Cambiar Estado</h2>
          <button onClick={onClose} className="rounded-lg p-1.5 text-muted-foreground hover:bg-accent/10 transition-colors">
            <X className="size-5" />
          </button>
        </div>

        <div className="flex flex-col gap-3 p-6">
          <p className="text-sm text-muted-foreground">
            Cita de <span className="font-semibold text-foreground">{appointment.patientName}</span>
          </p>

          {availableStatuses.length === 0 ? (
            <p className="text-sm text-center text-muted-foreground py-4">Esta cita ya no puede cambiar de estado.</p>
          ) : (
            <div className="flex flex-col gap-2">
              {availableStatuses.map((status) => {
                const config = APPOINTMENT_STATUS_CONFIG[status]
                return (
                  <button
                    key={status}
                    disabled={isLoading}
                    onClick={() => onConfirm(status)}
                    className={`flex items-center justify-between rounded-xl px-4 py-3 text-sm font-semibold transition-all hover:scale-[1.01] active:scale-100 disabled:opacity-60 ${config.className} border border-transparent hover:border-current/20`}
                  >
                    {config.label}
                    {isLoading && <Loader2 className="size-4 animate-spin" />}
                  </button>
                )
              })}
            </div>
          )}

          <button
            onClick={onClose}
            className="rounded-xl border border-border px-4 py-2 text-sm font-medium text-muted-foreground hover:bg-accent/5 transition-colors mt-1"
          >
            Cancelar
          </button>
        </div>
      </div>
    </div>
  )
}
