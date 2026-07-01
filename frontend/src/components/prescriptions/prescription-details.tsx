import { X, Download, Loader2, Pill, User, UserCog, Clock, Hash } from "lucide-react"
import { Prescription } from "@/types/prescription"

interface Props {
  prescription: Prescription
  onClose: () => void
  onDownloadPdf: (p: Prescription) => void
  isPdfLoading: string | null
}

export function PrescriptionDetails({ prescription, onClose, onDownloadPdf, isPdfLoading }: Props) {
  const date = new Date(prescription.createdAt)

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-background/80 backdrop-blur-sm animate-in fade-in duration-200">
      <div className="w-full max-w-lg rounded-2xl border border-border bg-card shadow-xl animate-in zoom-in-95 duration-200">
        {/* Header */}
        <div className="flex items-center justify-between border-b border-border p-6">
          <div className="flex items-center gap-3">
            <span className="flex size-10 items-center justify-center rounded-xl bg-emerald-500/10 text-emerald-500">
              <Pill className="size-5" />
            </span>
            <div>
              <h2 className="text-lg font-bold text-foreground">Detalle de Receta</h2>
              <p className="text-xs text-muted-foreground">
                {date.toLocaleDateString("es-DO", { day: "2-digit", month: "long", year: "numeric" })}
              </p>
            </div>
          </div>
          <button
            onClick={onClose}
            className="rounded-full p-2 text-muted-foreground hover:bg-muted hover:text-foreground transition-colors"
          >
            <X className="size-5" />
          </button>
        </div>

        {/* Prescription Card – styled like a physical recipe */}
        <div className="p-6 space-y-5">
          {/* Clinic header */}
          <div className="rounded-xl bg-gradient-to-br from-emerald-500/10 to-teal-500/5 border border-emerald-500/20 p-4">
            <div className="flex items-center gap-2 mb-1">
              <span className="text-emerald-600 font-bold text-lg tracking-tight">HealthCore</span>
              <span className="text-xs text-muted-foreground">· Sistema de Gestión Médica</span>
            </div>
            <div className="h-px bg-emerald-500/20 my-2" />
            <div className="grid grid-cols-2 gap-3 text-sm">
              <div className="flex items-center gap-2">
                <User className="size-3.5 text-muted-foreground shrink-0" />
                <div>
                  <p className="text-[10px] text-muted-foreground uppercase tracking-wider">Paciente</p>
                  <p className="font-semibold text-foreground">{prescription.patientName}</p>
                </div>
              </div>
              <div className="flex items-center gap-2">
                <UserCog className="size-3.5 text-muted-foreground shrink-0" />
                <div>
                  <p className="text-[10px] text-muted-foreground uppercase tracking-wider">Médico</p>
                  <p className="font-semibold text-foreground">{prescription.doctorName}</p>
                </div>
              </div>
              <div className="flex items-center gap-2">
                <Clock className="size-3.5 text-muted-foreground shrink-0" />
                <div>
                  <p className="text-[10px] text-muted-foreground uppercase tracking-wider">Fecha</p>
                  <p className="font-medium text-foreground">
                    {date.toLocaleDateString("es-DO", { day: "2-digit", month: "short", year: "numeric" })}
                  </p>
                </div>
              </div>
              <div className="flex items-center gap-2">
                <Hash className="size-3.5 text-muted-foreground shrink-0" />
                <div>
                  <p className="text-[10px] text-muted-foreground uppercase tracking-wider">ID Receta</p>
                  <p className="font-mono text-xs text-foreground">{prescription.id.slice(0, 8).toUpperCase()}</p>
                </div>
              </div>
            </div>
          </div>

          {/* Rx block */}
          <div className="rounded-xl border border-border bg-muted/20 p-5 space-y-3">
            <div className="flex items-center gap-2 mb-1">
              <span className="font-bold text-2xl italic text-emerald-600">℞</span>
              <span className="text-xs font-semibold uppercase tracking-widest text-muted-foreground">Prescripción</span>
            </div>

            <p className="text-lg font-bold text-foreground">{prescription.medication}</p>

            <div className="grid grid-cols-3 gap-3 text-sm">
              <div>
                <p className="text-[10px] text-muted-foreground uppercase tracking-wider">Dosis</p>
                <p className="font-semibold text-foreground">{prescription.dosage}</p>
              </div>
              <div>
                <p className="text-[10px] text-muted-foreground uppercase tracking-wider">Frecuencia</p>
                <p className="font-semibold text-foreground">{prescription.frequency}</p>
              </div>
              <div>
                <p className="text-[10px] text-muted-foreground uppercase tracking-wider">Duración</p>
                <span className="inline-flex items-center rounded-full bg-emerald-500/10 px-2 py-0.5 text-xs font-medium text-emerald-600">
                  {prescription.duration}
                </span>
              </div>
            </div>

            {prescription.instructions && (
              <div className="pt-2 border-t border-border">
                <p className="text-[10px] text-muted-foreground uppercase tracking-wider mb-1">Instrucciones</p>
                <p className="text-sm text-foreground italic">{prescription.instructions}</p>
              </div>
            )}
          </div>

          {/* Signature line */}
          <div className="text-center pt-2">
            <div className="h-px w-40 bg-border mx-auto mb-1" />
            <p className="text-xs font-semibold text-foreground">{prescription.doctorName}</p>
            <p className="text-[10px] text-muted-foreground">Médico Tratante · HealthCore</p>
          </div>
        </div>

        {/* Footer actions */}
        <div className="border-t border-border px-6 py-4 flex items-center justify-end gap-3">
          <button
            onClick={onClose}
            className="rounded-xl px-4 py-2 text-sm font-medium text-muted-foreground hover:bg-muted transition-colors"
          >
            Cerrar
          </button>
          <button
            onClick={() => onDownloadPdf(prescription)}
            disabled={isPdfLoading === prescription.id}
            className="flex items-center gap-2 rounded-xl bg-emerald-600 px-5 py-2 text-sm font-medium text-white hover:bg-emerald-700 transition-colors shadow-sm disabled:opacity-50"
          >
            {isPdfLoading === prescription.id ? (
              <Loader2 className="size-4 animate-spin" />
            ) : (
              <Download className="size-4" />
            )}
            {isPdfLoading === prescription.id ? "Generando PDF..." : "Descargar Receta PDF"}
          </button>
        </div>
      </div>
    </div>
  )
}
