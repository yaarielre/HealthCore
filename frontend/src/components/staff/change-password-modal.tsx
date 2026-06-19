"use client"

import { Loader2, Lock } from "lucide-react"
import { PasswordFormData } from "@/types/staff"

interface ChangePasswordModalProps {
  passwordData: PasswordFormData
  isSubmitLoading: boolean
  onSubmit: (e: React.FormEvent) => void
  onClose: () => void
  onChange: (data: PasswordFormData) => void
}

export function ChangePasswordModal({ passwordData, isSubmitLoading, onSubmit, onClose, onChange }: ChangePasswordModalProps) {
  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center p-4 bg-background/80 backdrop-blur-sm">
      <div className="w-full max-w-md rounded-2xl border border-border bg-card p-6 shadow-xl space-y-4">
        <div>
          <h3 className="text-lg font-bold text-foreground">Cambiar Contraseña</h3>
          <p className="text-xs text-muted-foreground">
            Nueva clave para <span className="font-semibold text-foreground">{passwordData.userName}</span>.
          </p>
        </div>

        <form onSubmit={onSubmit} className="space-y-4" autoComplete="off">
          <div className="space-y-1.5">
            <div className="relative">
              <Lock className="absolute left-3 top-1/2 size-4 -translate-y-1/2 text-muted-foreground" />
              <input
                type="password" required autoComplete="new-password"
                placeholder="Nueva contraseña..."
                value={passwordData.newPassword}
                onChange={(e) => onChange({ ...passwordData, newPassword: e.target.value })}
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
              {isSubmitLoading && <Loader2 className="size-4 animate-spin" />} Actualizar Clave
            </button>
          </div>
        </form>
      </div>
    </div>
  )
}
