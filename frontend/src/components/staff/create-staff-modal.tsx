"use client"

import { Briefcase, Loader2, Lock, Mail } from "lucide-react"
import { STAFF_ROLES, StaffFormData } from "@/hooks/useStaffManagement"
import { UserRole } from "@/services/authService"

interface CreateStaffModalProps {
  formData: StaffFormData
  isSubmitLoading: boolean
  onSubmit: (e: React.FormEvent) => void
  onClose: () => void
  onChange: (data: StaffFormData) => void
}

export function CreateStaffModal({ formData, isSubmitLoading, onSubmit, onClose, onChange }: CreateStaffModalProps) {
  function field(key: keyof StaffFormData, value: string) {
    onChange({ ...formData, [key]: value })
  }

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center p-4 bg-background/80 backdrop-blur-sm">
      <div className="w-full max-w-lg rounded-2xl border border-border bg-card p-6 shadow-xl space-y-4">
        <div>
          <h3 className="text-lg font-bold text-foreground">Registrar Personal Clínico</h3>
          <p className="text-xs text-muted-foreground">Completa los campos para crear la cuenta de acceso.</p>
        </div>

        <form onSubmit={onSubmit} className="space-y-4" autoComplete="off">
          <input type="text" name="prevent_autofill" style={{ display: "none" }} />
          <input type="password" name="prevent_autofill_pwd" style={{ display: "none" }} />

          <div className="space-y-1.5">
            <label className="text-xs font-semibold text-muted-foreground">Cargo / Rol</label>
            <div className="relative">
              <Briefcase className="absolute left-3 top-1/2 size-4 -translate-y-1/2 text-muted-foreground pointer-events-none" />
              <select
                required
                value={formData.role}
                onChange={(e) => onChange({ ...formData, role: Number(e.target.value) as UserRole })}
                className="w-full appearance-none rounded-xl border border-border bg-background py-2 pl-10 pr-4 text-sm text-foreground focus:outline-none focus:ring-2 focus:ring-accent/50 focus:border-accent"
              >
                {STAFF_ROLES.map(r => (
                  <option key={r.value} value={r.value}>{r.label}</option>
                ))}
              </select>
            </div>
          </div>

          <div className="grid grid-cols-2 gap-4">
            <div className="space-y-1.5">
              <label className="text-xs font-semibold text-muted-foreground">Nombre</label>
              <input
                type="text" required autoComplete="off"
                placeholder="ej. Carlos"
                value={formData.firstName}
                onChange={(e) => field("firstName", e.target.value)}
                className="w-full rounded-xl border border-border bg-background px-3 py-2 text-sm text-foreground focus:outline-none focus:ring-2 focus:ring-accent/50 focus:border-accent"
              />
            </div>
            <div className="space-y-1.5">
              <label className="text-xs font-semibold text-muted-foreground">Apellido</label>
              <input
                type="text" required autoComplete="off"
                placeholder="ej. Mendoza"
                value={formData.lastName}
                onChange={(e) => field("lastName", e.target.value)}
                className="w-full rounded-xl border border-border bg-background px-3 py-2 text-sm text-foreground focus:outline-none focus:ring-2 focus:ring-accent/50 focus:border-accent"
              />
            </div>
          </div>

          <div className="grid grid-cols-2 gap-4">
            <div className="space-y-1.5">
              <label className="text-xs font-semibold text-muted-foreground">Cédula / Identificación</label>
              <input
                type="text" required autoComplete="off"
                placeholder="ej. 001-1234567-8"
                value={formData.idNumber}
                onChange={(e) => field("idNumber", e.target.value)}
                className="w-full rounded-xl border border-border bg-background px-3 py-2 text-sm text-foreground focus:outline-none focus:ring-2 focus:ring-accent/50 focus:border-accent"
              />
            </div>
            <div className="space-y-1.5">
              <label className="text-xs font-semibold text-muted-foreground">Teléfono</label>
              <input
                type="text" required autoComplete="off"
                placeholder="ej. 809-555-1234"
                value={formData.phone}
                onChange={(e) => field("phone", e.target.value)}
                className="w-full rounded-xl border border-border bg-background px-3 py-2 text-sm text-foreground focus:outline-none focus:ring-2 focus:ring-accent/50 focus:border-accent"
              />
            </div>
          </div>

          <div className="space-y-1.5">
            <label className="text-xs font-semibold text-muted-foreground">Correo Electrónico</label>
            <div className="relative">
              <Mail className="absolute left-3 top-1/2 size-4 -translate-y-1/2 text-muted-foreground pointer-events-none" />
              <input
                type="text" required autoComplete="off" inputMode="email"
                placeholder="ej. carlos.mendoza@healthcore.com"
                value={formData.email}
                onChange={(e) => field("email", e.target.value)}
                className="w-full rounded-xl border border-border bg-background py-2 pl-10 pr-4 text-sm text-foreground focus:outline-none focus:ring-2 focus:ring-accent/50 focus:border-accent"
              />
            </div>
          </div>

          <div className="space-y-1.5">
            <label className="text-xs font-semibold text-muted-foreground">Contraseña Inicial</label>
            <div className="relative">
              <Lock className="absolute left-3 top-1/2 size-4 -translate-y-1/2 text-muted-foreground pointer-events-none" />
              <input
                type="password" required autoComplete="new-password"
                placeholder="ej. Clinica@2024!"
                value={formData.password}
                onChange={(e) => field("password", e.target.value)}
                className="w-full rounded-xl border border-border bg-background py-2 pl-10 pr-4 text-sm text-foreground focus:outline-none focus:ring-2 focus:ring-accent/50 focus:border-accent"
              />
            </div>
          </div>

          <div className="flex justify-end gap-2 pt-2 border-t border-border">
            <button
              type="button" onClick={onClose}
              className="rounded-xl border border-border bg-card px-4 py-2 text-sm font-semibold text-foreground hover:bg-accent/10 transition-colors"
            >
              Cancelar
            </button>
            <button
              type="submit" disabled={isSubmitLoading}
              className="flex items-center gap-2 rounded-xl bg-accent text-white px-4 py-2 text-sm font-semibold hover:bg-accent/90 disabled:opacity-50 transition-colors"
            >
              {isSubmitLoading && <Loader2 className="size-4 animate-spin" />} Guardar
            </button>
          </div>
        </form>
      </div>
    </div>
  )
}
