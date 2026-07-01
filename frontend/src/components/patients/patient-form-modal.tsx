"use client"

import { Loader2, X } from "lucide-react"
import { PatientFormData } from "@/types/patient"

interface PatientFormModalProps {
  mode: "create" | "edit"
  formData: PatientFormData
  isLoading: boolean
  onClose: () => void
  onSubmit: (e: React.FormEvent) => void
  onChange: (data: PatientFormData) => void
}

interface FieldProps {
  id: string
  label: string
  value: string
  type?: string
  placeholder: string
  disabled?: boolean
  onChange: (value: string) => void
}

function Field({ id, label, value, type = "text", placeholder, disabled = false, onChange }: FieldProps) {
  return (
    <div className="flex flex-col gap-1.5">
      <label htmlFor={id} className="text-xs font-semibold text-muted-foreground uppercase tracking-wide">{label}</label>
      <input id={id} type={type} value={value} placeholder={placeholder} disabled={disabled} autoComplete="off" onChange={(e) => onChange(e.target.value)} className="rounded-xl border border-border bg-background px-3 py-2 text-sm text-foreground placeholder:text-muted-foreground/60 focus:outline-none focus:ring-2 focus:ring-accent/50 disabled:opacity-50" />
    </div>
  )
}

export function PatientFormModal({ mode, formData, isLoading, onClose, onSubmit, onChange }: PatientFormModalProps) {
  const isCreate = mode === "create"
  const title = isCreate ? "Registrar Nuevo Paciente" : "Editar Paciente"
  const buttonLabel = isCreate ? "Registrar Paciente" : "Guardar Cambios"

  function update(key: keyof PatientFormData, value: string) {
    onChange({ ...formData, [key]: value })
  }

  return (
    <div role="dialog" aria-modal="true" aria-labelledby="patient-form-title" className="fixed inset-0 z-50 flex items-center justify-center bg-background/60 backdrop-blur-sm p-4">
      <div className="w-full max-w-lg rounded-2xl border border-border bg-card shadow-xl shadow-black/10">
        <div className="flex items-center justify-between border-b border-border px-6 py-4">
          <h2 id="patient-form-title" className="text-base font-bold text-foreground">{title}</h2>
          <button onClick={onClose} className="rounded-lg p-1.5 text-muted-foreground hover:bg-accent/10 transition-colors">
            <X className="size-5" />
          </button>
        </div>

        <form onSubmit={onSubmit} className="flex flex-col gap-4 p-6">
          <div className="grid grid-cols-2 gap-4">
            <Field id="firstName" label="Nombre" value={formData.firstName} placeholder="ej. María" onChange={(v) => update("firstName", v)} />
            <Field id="lastName" label="Apellido" value={formData.lastName} placeholder="ej. García" onChange={(v) => update("lastName", v)} />
          </div>

          <Field
            id="cedula"
            label="Cédula"
            value={formData.cedula}
            placeholder="ej. 001-0000000-0"
            disabled={!isCreate}
            onChange={(v) => update("cedula", v)}
          />

          <Field
            id="birthDate"
            label="Fecha de Nacimiento"
            type="date"
            value={formData.birthDate}
            placeholder=""
            disabled={!isCreate}
            onChange={(v) => update("birthDate", v)}
          />

          <Field id="phone" label="Teléfono" value={formData.phone} placeholder="ej. 809-000-0000" onChange={(v) => update("phone", v)} />

          <div className="flex flex-col gap-1.5">
            <label className="text-xs font-semibold text-muted-foreground uppercase tracking-wide">Dirección</label>
            <textarea
              value={formData.address}
              placeholder="ej. Calle Las Flores #12, Santo Domingo"
              rows={2}
              autoComplete="off"
              onChange={(e) => update("address", e.target.value)}
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
