import { AlertTriangle, Loader2 } from "lucide-react"

interface Props {
  isOpen: boolean
  onClose: () => void
  onConfirm: () => Promise<void>
  patientName: string
  date: string
  isLoading: boolean
}

export function MedicalConsultationDeleteModal({
  isOpen,
  onClose,
  onConfirm,
  patientName,
  date,
  isLoading
}: Props) {
  if (!isOpen) return null

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-background/80 backdrop-blur-sm">
      <div className="w-full max-w-md rounded-2xl border border-border bg-card p-6 shadow-2xl text-center">
        <span className="mx-auto flex size-12 items-center justify-center rounded-full bg-destructive/10 text-destructive mb-4">
          <AlertTriangle className="size-6" />
        </span>
        
        <h2 className="text-lg font-bold text-foreground">¿Eliminar Registro de Consulta?</h2>
        <p className="mt-2 text-sm text-muted-foreground">
          Estás a punto de eliminar la consulta del paciente <strong>{patientName}</strong> realizada el <strong>{new Date(date).toLocaleDateString()}</strong>. Esta acción no se puede deshacer y los datos clínicos registrados se perderán.
        </p>

        <div className="mt-6 flex justify-end gap-3">
          <button
            onClick={onClose}
            disabled={isLoading}
            className="flex-1 rounded-xl px-4 py-2.5 text-sm font-medium text-foreground hover:bg-accent/10 transition-colors"
          >
            Cancelar
          </button>
          <button
            onClick={onConfirm}
            disabled={isLoading}
            className="flex flex-1 items-center justify-center gap-2 rounded-xl bg-destructive px-4 py-2.5 text-sm font-medium text-white hover:bg-destructive/90 transition-colors disabled:opacity-70"
          >
            {isLoading && <Loader2 className="size-4 animate-spin" />}
            Sí, Eliminar
          </button>
        </div>
      </div>
    </div>
  )
}
