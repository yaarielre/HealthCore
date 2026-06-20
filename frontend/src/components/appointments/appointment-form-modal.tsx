"use client"

import { Loader2, X } from "lucide-react"
import { AppointmentFormData } from "@/types/appointment"
import { Patient } from "@/types/patient"

interface AppointmentFormModalProps {
  mode: "create" | "edit"
  formData: AppointmentFormData
  patients: Patient[]
  isLoading: boolean
  onClose: () => void
  onSubmit: (e: React.FormEvent) => void
  onChange: (data: AppointmentFormData) => void
}

export function AppointmentFormModal({
  mode,
  formData,
  patients,
  isLoading,
  onClose,
  onSubmit,
  onChange,
}: AppointmentFormModalProps) {
  const isCreate = mode === "create"
  const title = isCreate ? "Nueva Cita Médica" : "Editar Cita"
  const buttonLabel = isCreate ? "Programar Cita" : "Guardar Cambios"

  function update(key: keyof AppointmentFormData, value: string) {
    onChange({ ...formData, [key]: value })
  }

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-background/60 backdrop-blur-sm p-4">
      <div className="w-full max-w-lg rounded-2xl border border-border bg-card shadow-xl shadow-black/10">
        <div className="flex items-center justify-between border-b border-border px-6 py-4">
          <h2 className="text-base font-bold text-foreground">{title}</h2>
          <button onClick={onClose} className="rounded-lg p-1.5 text-muted-foreground hover:bg-accent/10 transition-colors">
            <X className="size-5" />
          </button>
        </div>

        <form onSubmit={onSubmit} className="flex flex-col gap-4 p-6">
          {isCreate && (
            <div className="flex flex-col gap-1.5">
              <label className="text-xs font-semibold text-muted-foreground uppercase tracking-wide">Paciente</label>
              <select
                value={formData.patientId}
                onChange={(e) => update("patientId", e.target.value)}
                required
                className="rounded-xl border border-border bg-background px-3 py-2 text-sm text-foreground focus:outline-none focus:ring-2 focus:ring-accent/50"
              >
                <option value="">Selecciona un paciente...</option>
                {patients.map((p) => (
                  <option key={p.id} value={p.id}>
                    {p.firstName} {p.lastName} — {p.cedula}
                  </option>
                ))}
              </select>
            </div>
          )}

          {isCreate && (
            <div className="flex flex-col gap-1.5">
              <label className="text-xs font-semibold text-muted-foreground uppercase tracking-wide">Doctor ID</label>
              <input
                type="text"
                value={formData.doctorId}
                placeholder="ej. UUID del doctor asignado"
                onChange={(e) => update("doctorId", e.target.value)}
                required
                autoComplete="off"
                className="rounded-xl border border-border bg-background px-3 py-2 text-sm text-foreground placeholder:text-muted-foreground/60 focus:outline-none focus:ring-2 focus:ring-accent/50"
              />
            </div>
          )}

          <div className="flex flex-col gap-1.5">
            <label className="text-xs font-semibold text-muted-foreground uppercase tracking-wide">Fecha y Hora</label>
            <input
              type="datetime-local"
              value={formData.appointmentDate}
              onChange={(e) => update("appointmentDate", e.target.value)}
              required
              className="rounded-xl border border-border bg-background px-3 py-2 text-sm text-foreground focus:outline-none focus:ring-2 focus:ring-accent/50"
            />
          </div>

          <div className="flex flex-col gap-1.5">
            <label className="text-xs font-semibold text-muted-foreground uppercase tracking-wide">Motivo de la Consulta</label>
            <input
              type="text"
              value={formData.reason}
              placeholder="ej. Consulta general, Control mensual..."
              onChange={(e) => update("reason", e.target.value)}
              required
              autoComplete="off"
              className="rounded-xl border border-border bg-background px-3 py-2 text-sm text-foreground placeholder:text-muted-foreground/60 focus:outline-none focus:ring-2 focus:ring-accent/50"
            />
          </div>

          <div className="flex flex-col gap-1.5">
            <label className="text-xs font-semibold text-muted-foreground uppercase tracking-wide">Notas adicionales <span className="normal-case font-normal text-muted-foreground">(opcional)</span></label>
            <textarea
              value={formData.notes}
              placeholder="ej. Paciente alérgico a la penicilina..."
              rows={2}
              autoComplete="off"
              onChange={(e) => update("notes", e.target.value)}
              className="rounded-xl border border-border bg-background px-3 py-2 text-sm text-foreground placeholder:text-muted-foreground/60 focus:outline-none focus:ring-2 focus:ring-accent/50 resize-none"
            />
          </div>

          <div className="flex justify-end gap-3 pt-2">
            <button
              type="button"
              onClick={onClose}
              className="rounded-xl border border-border px-4 py-2 text-sm font-medium text-muted-foreground hover:bg-accent/5 transition-colors"
            >
              Cancelar
            </button>
            <button
              type="submit"
              disabled={isLoading}
              className="flex items-center gap-2 rounded-xl bg-accent px-4 py-2 text-sm font-semibold text-white hover:brightness-110 disabled:opacity-60 transition-all active:scale-95"
            >
              {isLoading && <Loader2 className="size-4 animate-spin" />}
              {buttonLabel}
            </button>
          </div>
        </form>
      </div>
    </div>
  )
}
