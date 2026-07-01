import { X, Save, Plus } from "lucide-react"
import { CreateMedicalHistoryItemFormData, MedicalHistoryCategory, MedicalHistoryCategoryMap } from "@/types/medical-record"

interface Props {
  formData: CreateMedicalHistoryItemFormData
  setFormData: (data: CreateMedicalHistoryItemFormData) => void
  onSubmit: (e: React.FormEvent) => void
  onClose: () => void
  isLoading: boolean
}

export function MedicalHistoryItemForm({
  formData,
  setFormData,
  onSubmit,
  onClose,
  isLoading
}: Props) {
  return (
    <div className="fixed inset-0 z-[60] flex items-center justify-center bg-background/80 backdrop-blur-sm animate-in fade-in duration-200">
      <div className="w-full max-w-lg rounded-2xl border border-border bg-card shadow-xl animate-in zoom-in-95 duration-200">
        <div className="flex items-center justify-between border-b border-border p-6">
          <h2 className="text-xl font-bold text-foreground">Añadir Antecedente</h2>
          <button
            onClick={onClose}
            className="rounded-full p-2 text-muted-foreground hover:bg-muted hover:text-foreground transition-colors"
          >
            <X className="size-5" />
          </button>
        </div>

        <form onSubmit={onSubmit} className="p-6">
          <div className="space-y-6">
            
            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Categoría</label>
              <select
                required
                value={formData.category}
                onChange={(e) => setFormData({ ...formData, category: e.target.value ? Number(e.target.value) : "" })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm ring-offset-background focus:outline-none focus:ring-2 focus:ring-ring"
              >
                <option value="">Seleccione una categoría</option>
                {Object.entries(MedicalHistoryCategoryMap).map(([key, value]) => (
                  <option key={key} value={key}>{value}</option>
                ))}
              </select>
            </div>

            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Descripción Principal</label>
              <input
                required
                value={formData.description}
                onChange={(e) => setFormData({ ...formData, description: e.target.value })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm ring-offset-background placeholder:text-muted-foreground focus:outline-none focus:ring-2 focus:ring-ring"
                placeholder="Ej. Apendicectomía, Hipertensión, Penicilina..."
              />
            </div>

            <div className="grid grid-cols-2 gap-4">
              <div className="space-y-2">
                <label className="text-sm font-medium text-foreground">Fecha del Suceso</label>
                <input
                  type="date"
                  value={formData.recordedDate}
                  onChange={(e) => setFormData({ ...formData, recordedDate: e.target.value })}
                  className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm ring-offset-background focus:outline-none focus:ring-2 focus:ring-ring"
                />
              </div>

              <div className="space-y-2">
                <label className="text-sm font-medium text-foreground">Severidad (1 al 10)</label>
                <input
                  type="number"
                  min="1"
                  max="10"
                  value={formData.severity}
                  onChange={(e) => setFormData({ ...formData, severity: e.target.value ? Number(e.target.value) : "" })}
                  className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm ring-offset-background placeholder:text-muted-foreground focus:outline-none focus:ring-2 focus:ring-ring"
                  placeholder="Opcional"
                />
              </div>
            </div>

            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Detalles Adicionales</label>
              <textarea
                value={formData.details}
                onChange={(e) => setFormData({ ...formData, details: e.target.value })}
                className="w-full min-h-[100px] rounded-xl border border-input bg-background px-4 py-3 text-sm ring-offset-background placeholder:text-muted-foreground focus:outline-none focus:ring-2 focus:ring-ring resize-none"
                placeholder="Explicación detallada, complicaciones, notas..."
              />
            </div>

          </div>

          <div className="mt-8 flex items-center justify-end gap-3 border-t border-border pt-6">
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
              className="flex items-center gap-2 rounded-xl bg-primary px-6 py-2.5 text-sm font-medium text-primary-foreground hover:bg-primary/90 transition-colors shadow-sm disabled:opacity-50"
            >
              <Plus className="size-4" />
              {isLoading ? "Guardando..." : "Registrar"}
            </button>
          </div>
        </form>
      </div>
    </div>
  )
}
