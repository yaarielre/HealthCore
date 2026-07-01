import { X, Loader2 } from "lucide-react"
import { CreateMedicalRecordFormData, BloodType } from "@/types/medical-record"
import { Patient } from "@/types/patient"

interface Props {
  isOpen: boolean
  onClose: () => void
  onSubmit: (e: React.FormEvent) => Promise<void>
  formData: CreateMedicalRecordFormData
  setFormData: (data: CreateMedicalRecordFormData) => void
  isLoading: boolean
  isEdit: boolean
  patients: Patient[]
}

const bloodTypeOptions = [
  { value: BloodType.APositive, label: "A+" },
  { value: BloodType.ANegative, label: "A-" },
  { value: BloodType.BPositive, label: "B+" },
  { value: BloodType.BNegative, label: "B-" },
  { value: BloodType.ABPositive, label: "AB+" },
  { value: BloodType.ABNegative, label: "AB-" },
  { value: BloodType.OPositive, label: "O+" },
  { value: BloodType.ONegative, label: "O-" }
]

export function MedicalRecordFormModal({
  isOpen,
  onClose,
  onSubmit,
  formData,
  setFormData,
  isLoading,
  isEdit,
  patients
}: Props) {
  if (!isOpen) return null

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-background/80 backdrop-blur-sm">
      <div className="w-full max-w-2xl rounded-2xl border border-border bg-card p-6 shadow-2xl">
        <div className="flex items-center justify-between mb-6">
          <h2 className="text-xl font-bold text-foreground">
            {isEdit ? "Editar Historia Clínica" : "Nueva Historia Clínica"}
          </h2>
          <button
            onClick={onClose}
            className="rounded-full p-2 text-muted-foreground hover:bg-accent/10 transition-colors"
            type="button"
          >
            <X className="size-5" />
          </button>
        </div>

        <form onSubmit={onSubmit} className="space-y-4">
          <div className="grid gap-4 md:grid-cols-2">
            {!isEdit && (
              <div className="space-y-2 md:col-span-2">
                <label className="text-sm font-medium text-foreground">Paciente</label>
                <select
                  required
                  value={formData.patientId}
                  onChange={(e) => setFormData({ ...formData, patientId: e.target.value })}
                  className="w-full rounded-xl border border-border bg-background px-4 py-2.5 text-sm outline-none focus:border-accent"
                >
                  <option value="">Seleccione un paciente</option>
                  {patients.map(p => (
                    <option key={p.id} value={p.id}>{p.firstName} {p.lastName}</option>
                  ))}
                </select>
              </div>
            )}

            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">N° Expediente</label>
              <input
                required
                type="text"
                value={formData.recordNumber}
                onChange={(e) => setFormData({ ...formData, recordNumber: e.target.value })}
                className="w-full rounded-xl border border-border bg-background px-4 py-2.5 text-sm outline-none focus:border-accent"
                placeholder="Ej. EXP-2023-001"
              />
            </div>

            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Tipo de Sangre</label>
              <select
                value={formData.bloodType?.toString() || ""}
                onChange={(e) => setFormData({ 
                  ...formData, 
                  bloodType: e.target.value ? parseInt(e.target.value) as BloodType : "" 
                })}
                className="w-full rounded-xl border border-border bg-background px-4 py-2.5 text-sm outline-none focus:border-accent"
              >
                <option value="">Desconocido / No especificado</option>
                {bloodTypeOptions.map(opt => (
                  <option key={opt.value} value={opt.value}>{opt.label}</option>
                ))}
              </select>
            </div>

            <div className="space-y-2 md:col-span-2">
              <label className="text-sm font-medium text-foreground">Alergias conocidas</label>
              <input
                type="text"
                value={formData.allergies}
                onChange={(e) => setFormData({ ...formData, allergies: e.target.value })}
                className="w-full rounded-xl border border-border bg-background px-4 py-2.5 text-sm outline-none focus:border-accent"
                placeholder="Ej. Penicilina, Nueces (dejar vacío si no aplica)"
              />
            </div>

            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Contacto de Emergencia</label>
              <input
                type="text"
                value={formData.emergencyContactName}
                onChange={(e) => setFormData({ ...formData, emergencyContactName: e.target.value })}
                className="w-full rounded-xl border border-border bg-background px-4 py-2.5 text-sm outline-none focus:border-accent"
                placeholder="Nombre del familiar"
              />
            </div>

            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Teléfono de Emergencia</label>
              <input
                type="text"
                value={formData.emergencyContactPhone}
                onChange={(e) => setFormData({ ...formData, emergencyContactPhone: e.target.value })}
                className="w-full rounded-xl border border-border bg-background px-4 py-2.5 text-sm outline-none focus:border-accent"
                placeholder="Teléfono"
              />
            </div>
            
            <div className="space-y-2 md:col-span-2">
              <label className="text-sm font-medium text-foreground">Notas Adicionales</label>
              <textarea
                value={formData.notes}
                onChange={(e) => setFormData({ ...formData, notes: e.target.value })}
                className="w-full rounded-xl border border-border bg-background px-4 py-2.5 text-sm outline-none focus:border-accent resize-none h-24"
                placeholder="Antecedentes importantes, información relevante..."
              />
            </div>
          </div>

          <div className="flex justify-end gap-3 pt-6 border-t border-border">
            <button
              type="button"
              onClick={onClose}
              disabled={isLoading}
              className="rounded-xl px-5 py-2.5 text-sm font-medium text-foreground hover:bg-accent/10 transition-colors"
            >
              Cancelar
            </button>
            <button
              type="submit"
              disabled={isLoading}
              className="flex items-center gap-2 rounded-xl bg-accent px-5 py-2.5 text-sm font-medium text-white hover:bg-accent/90 transition-colors disabled:opacity-70"
            >
              {isLoading && <Loader2 className="size-4 animate-spin" />}
              {isEdit ? "Actualizar" : "Registrar"}
            </button>
          </div>
        </form>
      </div>
    </div>
  )
}
