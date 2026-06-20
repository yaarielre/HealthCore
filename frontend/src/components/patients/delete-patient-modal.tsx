"use client"

import { AlertTriangle, Loader2, X } from "lucide-react"
import { Patient } from "@/types/patient"

interface DeletePatientModalProps {
  patient: Patient
  isLoading: boolean
  onClose: () => void
  onConfirm: () => void
}

export function DeletePatientModal({ patient, isLoading, onClose, onConfirm }: DeletePatientModalProps) {
  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-background/60 backdrop-blur-sm p-4">
      <div className="w-full max-w-md rounded-2xl border border-border bg-card shadow-xl shadow-black/10">
        <div className="flex items-center justify-between border-b border-border px-6 py-4">
          <h2 className="text-base font-bold text-foreground">Eliminar Paciente</h2>
          <button onClick={onClose} className="rounded-lg p-1.5 text-muted-foreground hover:bg-accent/10 transition-colors">
            <X className="size-5" />
          </button>
        </div>

        <div className="flex flex-col items-center gap-4 px-6 py-8 text-center">
          <span className="flex size-14 items-center justify-center rounded-full bg-destructive/10 text-destructive">
            <AlertTriangle className="size-7" />
          </span>
          <div>
            <p className="text-sm font-semibold text-foreground">
              ¿Eliminar a {patient.firstName} {patient.lastName}?
            </p>
            <p className="mt-1 text-xs text-muted-foreground">
              Esta acción es irreversible. Se eliminará permanentemente del sistema junto con sus datos.
            </p>
          </div>
        </div>

        <div className="flex justify-end gap-3 border-t border-border px-6 py-4">
          <button
            onClick={onClose}
            className="rounded-xl border border-border px-4 py-2 text-sm font-medium text-muted-foreground hover:bg-accent/5 transition-colors"
          >
            Cancelar
          </button>
          <button
            onClick={onConfirm}
            disabled={isLoading}
            className="flex items-center gap-2 rounded-xl bg-destructive px-4 py-2 text-sm font-semibold text-white hover:brightness-110 disabled:opacity-60 transition-all active:scale-95"
          >
            {isLoading && <Loader2 className="size-4 animate-spin" />}
            Sí, eliminar
          </button>
        </div>
      </div>
    </div>
  )
}
