import { X, Save, Pill } from "lucide-react"
import { CreatePrescriptionFormData } from "@/types/prescription"
import { MedicalConsultation } from "@/types/medical-consultation"

interface Props {
  title: string
  formData: CreatePrescriptionFormData
  setFormData: (data: CreatePrescriptionFormData) => void
  onConsultationChange: (id: string) => void
  onSubmit: (e: React.FormEvent) => void
  onClose: () => void
  isLoading: boolean
  consultations: MedicalConsultation[]
  isEditMode?: boolean
}

export function PrescriptionForm({
  title,
  formData,
  setFormData,
  onConsultationChange,
  onSubmit,
  onClose,
  isLoading,
  consultations,
  isEditMode = false,
}: Props) {
  const selectedConsultation = consultations.find(
    (c) => c.id === formData.medicalConsultationId
  )

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-background/80 backdrop-blur-sm animate-in fade-in duration-200">
      <div className="w-full max-w-2xl rounded-2xl border border-border bg-card shadow-xl animate-in zoom-in-95 duration-200 max-h-[90vh] flex flex-col">
        <div className="flex items-center justify-between border-b border-border p-6">
          <div className="flex items-center gap-3">
            <span className="flex size-10 items-center justify-center rounded-xl bg-emerald-500/10 text-emerald-500">
              <Pill className="size-5" />
            </span>
            <h2 className="text-xl font-bold text-foreground">{title}</h2>
          </div>
          <button
            onClick={onClose}
            className="rounded-full p-2 text-muted-foreground hover:bg-muted hover:text-foreground transition-colors"
          >
            <X className="size-5" />
          </button>
        </div>

        <form onSubmit={onSubmit} className="p-6 overflow-y-auto space-y-5">
          {/* Consulta vinculada */}
          <div className="space-y-2">
            <label className="text-sm font-medium text-foreground">
              Consulta Médica Vinculada <span className="text-destructive">*</span>
            </label>
            <select
              required
              disabled={isEditMode}
              value={formData.medicalConsultationId}
              onChange={(e) => onConsultationChange(e.target.value)}
              className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring"
            >
              <option value="">Seleccione una consulta...</option>
              {consultations.map((c) => (
                <option key={c.id} value={c.id}>
                  {c.patientName} — {c.doctorName} ({new Date(c.createdAt).toLocaleDateString("es-DO")})
                </option>
              ))}
            </select>
          </div>

          {/* Info auto-rellenada */}
          {selectedConsultation && (
            <div className="rounded-xl bg-emerald-500/5 border border-emerald-500/20 p-4 grid grid-cols-2 gap-3 text-sm">
              <div>
                <p className="text-muted-foreground text-xs">Paciente</p>
                <p className="font-semibold text-foreground">{selectedConsultation.patientName}</p>
              </div>
              <div>
                <p className="text-muted-foreground text-xs">Médico Tratante</p>
                <p className="font-semibold text-foreground">{selectedConsultation.doctorName}</p>
              </div>
              {selectedConsultation.diagnosis && (
                <div className="col-span-2">
                  <p className="text-muted-foreground text-xs">Diagnóstico de la consulta</p>
                  <p className="font-medium text-foreground">{selectedConsultation.diagnosis}</p>
                </div>
              )}
            </div>
          )}

          <div className="grid grid-cols-1 gap-5 sm:grid-cols-2">
            {/* Medicamento */}
            <div className="space-y-2 sm:col-span-2">
              <label className="text-sm font-medium text-foreground">
                Medicamento <span className="text-destructive">*</span>
              </label>
              <input
                required
                value={formData.medication}
                onChange={(e) => setFormData({ ...formData, medication: e.target.value })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring"
                placeholder="Ej. Amoxicilina 500mg"
              />
            </div>

            {/* Dosis */}
            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">
                Dosis <span className="text-destructive">*</span>
              </label>
              <input
                required
                value={formData.dosage}
                onChange={(e) => setFormData({ ...formData, dosage: e.target.value })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring"
                placeholder="Ej. 1 cápsula"
              />
            </div>

            {/* Frecuencia */}
            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">
                Frecuencia <span className="text-destructive">*</span>
              </label>
              <input
                required
                value={formData.frequency}
                onChange={(e) => setFormData({ ...formData, frequency: e.target.value })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring"
                placeholder="Ej. Cada 8 horas"
              />
            </div>

            {/* Duración */}
            <div className="space-y-2 sm:col-span-2">
              <label className="text-sm font-medium text-foreground">
                Duración del Tratamiento <span className="text-destructive">*</span>
              </label>
              <input
                required
                value={formData.duration}
                onChange={(e) => setFormData({ ...formData, duration: e.target.value })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring"
                placeholder="Ej. 7 días"
              />
            </div>

            {/* Instrucciones */}
            <div className="space-y-2 sm:col-span-2">
              <label className="text-sm font-medium text-foreground">Instrucciones Adicionales</label>
              <textarea
                value={formData.instructions}
                onChange={(e) => setFormData({ ...formData, instructions: e.target.value })}
                className="w-full min-h-[80px] rounded-xl border border-input bg-background px-4 py-3 text-sm focus:outline-none focus:ring-2 focus:ring-ring resize-none"
                placeholder="Ej. Tomar con comida. Evitar el sol durante el tratamiento."
              />
            </div>
          </div>

          <div className="flex items-center justify-end gap-3 border-t border-border pt-5">
            <button
              type="button"
              onClick={onClose}
              disabled={isLoading}
              className="rounded-xl px-4 py-2.5 text-sm font-medium text-muted-foreground hover:bg-muted transition-colors disabled:opacity-50"
            >
              Cancelar
            </button>
            <button
              type="submit"
              disabled={isLoading}
              className="flex items-center gap-2 rounded-xl bg-emerald-600 px-6 py-2.5 text-sm font-medium text-white hover:bg-emerald-700 transition-colors shadow-sm disabled:opacity-50"
            >
              <Save className="size-4" />
              {isLoading ? "Guardando..." : "Guardar Receta"}
            </button>
          </div>
        </form>
      </div>
    </div>
  )
}
