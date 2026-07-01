import { X, Save, AlertCircle } from "lucide-react"
import { CreateMedicalRecordFormData, BloodType, BloodTypeMap } from "@/types/medical-record"
import { Patient } from "@/types/patient"

interface Props {
  title: string
  formData: CreateMedicalRecordFormData
  setFormData: (data: CreateMedicalRecordFormData) => void
  onSubmit: (e: React.FormEvent) => void
  onClose: () => void
  isLoading: boolean
  patients: Patient[]
  isEditMode?: boolean
}

export function MedicalRecordForm({
  title,
  formData,
  setFormData,
  onSubmit,
  onClose,
  isLoading,
  patients,
  isEditMode = false
}: Props) {
  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-background/80 backdrop-blur-sm animate-in fade-in duration-200">
      <div className="w-full max-w-2xl rounded-2xl border border-border bg-card shadow-xl animate-in zoom-in-95 duration-200">
        <div className="flex items-center justify-between border-b border-border p-6">
          <h2 className="text-xl font-bold text-foreground">{title}</h2>
          <button
            onClick={onClose}
            className="rounded-full p-2 text-muted-foreground hover:bg-muted hover:text-foreground transition-colors"
          >
            <X className="size-5" />
          </button>
        </div>

        <form onSubmit={onSubmit} className="p-6">
          <div className="grid grid-cols-1 gap-6 sm:grid-cols-2">
            {!isEditMode && (
              <div className="space-y-2 sm:col-span-2">
                <label className="text-sm font-medium text-foreground">Paciente</label>
                <select
                  required
                  value={formData.patientId}
                  onChange={(e) => setFormData({ ...formData, patientId: e.target.value })}
                  className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm ring-offset-background placeholder:text-muted-foreground focus:outline-none focus:ring-2 focus:ring-ring"
                >
                  <option value="">Seleccione un paciente</option>
                  {patients.map((p) => (
                    <option key={p.id} value={p.id}>{p.firstName} {p.lastName}</option>
                  ))}
                </select>
              </div>
            )}

            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Número de Expediente</label>
              <input
                required
                disabled={isEditMode}
                value={formData.recordNumber}
                onChange={(e) => setFormData({ ...formData, recordNumber: e.target.value })}
                className="w-full rounded-xl border border-input bg-muted/50 px-4 py-2.5 text-sm ring-offset-background focus:outline-none focus:ring-2 focus:ring-ring"
                placeholder="Ej. REC-12345"
              />
            </div>

            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Tipo de Sangre</label>
              <select
                value={formData.bloodType}
                onChange={(e) => setFormData({ ...formData, bloodType: e.target.value ? Number(e.target.value) : "" })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm ring-offset-background focus:outline-none focus:ring-2 focus:ring-ring"
              >
                <option value="">No especificado</option>
                {Object.entries(BloodTypeMap).map(([key, value]) => (
                  <option key={key} value={key}>{value}</option>
                ))}
              </select>
            </div>

            <div className="space-y-2 sm:col-span-2">
              <label className="text-sm font-medium text-foreground flex items-center gap-2">
                <AlertCircle className="size-4 text-red-500" /> Alergias Globales
              </label>
              <input
                value={formData.allergies}
                onChange={(e) => setFormData({ ...formData, allergies: e.target.value })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm ring-offset-background placeholder:text-muted-foreground focus:outline-none focus:ring-2 focus:ring-ring focus:border-red-500"
                placeholder="Ej. Penicilina, Maní (Dejar en blanco si no hay)"
              />
            </div>

            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Nombre Contacto Emergencia</label>
              <input
                value={formData.emergencyContactName}
                onChange={(e) => setFormData({ ...formData, emergencyContactName: e.target.value })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm ring-offset-background placeholder:text-muted-foreground focus:outline-none focus:ring-2 focus:ring-ring"
                placeholder="Ej. María Pérez (Esposa)"
              />
            </div>

            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Teléfono de Emergencia</label>
              <input
                value={formData.emergencyContactPhone}
                onChange={(e) => setFormData({ ...formData, emergencyContactPhone: e.target.value })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm ring-offset-background placeholder:text-muted-foreground focus:outline-none focus:ring-2 focus:ring-ring"
                placeholder="Ej. +1 809-555-1234"
              />
            </div>

            <div className="space-y-2 sm:col-span-2">
              <label className="text-sm font-medium text-foreground">Notas Adicionales</label>
              <textarea
                value={formData.notes}
                onChange={(e) => setFormData({ ...formData, notes: e.target.value })}
                className="w-full min-h-[100px] rounded-xl border border-input bg-background px-4 py-3 text-sm ring-offset-background placeholder:text-muted-foreground focus:outline-none focus:ring-2 focus:ring-ring resize-none"
                placeholder="Notas generales del expediente..."
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
              <Save className="size-4" />
              {isLoading ? "Guardando..." : "Guardar Expediente"}
            </button>
          </div>
        </form>
      </div>
    </div>
  )
}
