import { ArrowLeft, Loader2, HeartPulse, Activity } from "lucide-react"
import { CreateMedicalConsultationFormData } from "@/types/medical-consultation"
import { Patient } from "@/types/patient"
import { Doctor } from "@/types/doctor"
import { Appointment } from "@/types/appointment"
import { UserRole } from "@/types/auth"

interface Props {
  onClose: () => void
  onSubmit: (e: React.FormEvent) => Promise<void>
  formData: CreateMedicalConsultationFormData
  setFormData: (data: CreateMedicalConsultationFormData) => void
  isLoading: boolean
  isEdit: boolean
  patients: Patient[]
  doctors: Doctor[]
  appointments: Appointment[]
  userRole?: UserRole
}

export function MedicalConsultationForm({
  onClose,
  onSubmit,
  formData,
  setFormData,
  isLoading,
  isEdit,
  patients,
  doctors,
  appointments,
  userRole
}: Props) {
  return (
    <div className="space-y-6 animate-in fade-in slide-in-from-bottom-4 duration-500">
      <div className="flex items-center gap-4">
        <button
          onClick={onClose}
          className="rounded-full p-2 text-muted-foreground hover:bg-accent/10 hover:text-foreground transition-colors"
          type="button"
          title="Volver"
        >
          <ArrowLeft className="size-5" />
        </button>
        <div>
          <h2 className="text-2xl font-bold text-foreground">
            {isEdit ? "Detalles de la Consulta" : "Registrar Nueva Consulta"}
          </h2>
          <p className="text-sm text-muted-foreground mt-1">
            {isEdit ? "Edita la información clínica de esta atención médica." : "Completa los datos para registrar una nueva atención médica."}
          </p>
        </div>
      </div>

      <div className="rounded-2xl border border-border bg-card shadow-sm p-6 lg:p-8">
        <form onSubmit={onSubmit} className="space-y-8">
          <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
            <div className="space-y-2 lg:col-span-2">
              <label className="text-sm font-medium text-foreground">Paciente <span className="text-destructive">*</span></label>
              <select
                required
                disabled={isEdit}
                value={formData.patientId}
                onChange={(e) => setFormData({ ...formData, patientId: e.target.value })}
                className="w-full rounded-xl border border-border bg-background px-4 py-3 text-sm outline-none focus:border-accent disabled:opacity-70 disabled:bg-accent/5 transition-colors"
              >
                <option value="">Seleccione un paciente</option>
                {patients.map(p => (
                  <option key={p.id} value={p.id}>{p.firstName} {p.lastName}</option>
                ))}
              </select>
            </div>

            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Médico Tratante <span className="text-destructive">*</span></label>
              <select
                required
                disabled={userRole === UserRole.Doctor || isEdit}
                value={formData.doctorId}
                onChange={(e) => setFormData({ ...formData, doctorId: e.target.value })}
                className="w-full rounded-xl border border-border bg-background px-4 py-3 text-sm outline-none focus:border-accent disabled:opacity-70 disabled:bg-accent/5 transition-colors"
              >
                <option value="">Seleccione el médico</option>
                {doctors.map(d => (
                  <option key={d.id} value={d.id}>Dr/a. {d.firstName} {d.lastName}</option>
                ))}
              </select>
            </div>

            <div className="space-y-2 lg:col-span-3">
              <label className="text-sm font-medium text-foreground">Cita Asociada (Opcional)</label>
              <select
                value={formData.appointmentId || ""}
                onChange={(e) => setFormData({ ...formData, appointmentId: e.target.value || null })}
                className="w-full rounded-xl border border-border bg-background px-4 py-3 text-sm outline-none focus:border-accent transition-colors"
              >
                <option value="">Ninguna cita específica / Consulta espontánea</option>
                {appointments
                  .filter(a => !formData.patientId || a.patientId === formData.patientId)
                  .map(a => (
                    <option key={a.id} value={a.id}>
                      {new Date(a.appointmentDate).toLocaleDateString()} - {a.reason}
                    </option>
                  ))
                }
              </select>
            </div>
          </div>

          <div className="rounded-xl border border-border bg-accent/5 p-6">
            <h3 className="mb-6 flex items-center gap-2 text-base font-semibold text-foreground">
              <HeartPulse className="size-5 text-accent" />
              Signos Vitales
            </h3>
            <div className="grid gap-5 sm:grid-cols-2 md:grid-cols-3">
              <div className="space-y-2">
                <label className="text-xs font-semibold uppercase tracking-wider text-muted-foreground">Presión Arterial (mmHg)</label>
                <input
                  type="text"
                  placeholder="Ej. 120/80"
                  value={formData.vitalSigns.bloodPressure}
                  onChange={(e) => setFormData({
                    ...formData,
                    vitalSigns: { ...formData.vitalSigns, bloodPressure: e.target.value }
                  })}
                  className="w-full rounded-xl border border-border bg-background px-4 py-2.5 text-sm outline-none focus:border-accent transition-colors"
                />
              </div>
              <div className="space-y-2">
                <label className="text-xs font-semibold uppercase tracking-wider text-muted-foreground">Temperatura (°C)</label>
                <input
                  type="number"
                  step="0.1"
                  placeholder="Ej. 36.5"
                  value={formData.vitalSigns.temperature}
                  onChange={(e) => setFormData({
                    ...formData,
                    vitalSigns: { ...formData.vitalSigns, temperature: e.target.value ? parseFloat(e.target.value) : "" }
                  })}
                  className="w-full rounded-xl border border-border bg-background px-4 py-2.5 text-sm outline-none focus:border-accent transition-colors"
                />
              </div>
              <div className="space-y-2">
                <label className="text-xs font-semibold uppercase tracking-wider text-muted-foreground">Peso (kg)</label>
                <input
                  type="number"
                  step="0.1"
                  placeholder="Ej. 70.5"
                  value={formData.vitalSigns.weight}
                  onChange={(e) => setFormData({
                    ...formData,
                    vitalSigns: { ...formData.vitalSigns, weight: e.target.value ? parseFloat(e.target.value) : "" }
                  })}
                  className="w-full rounded-xl border border-border bg-background px-4 py-2.5 text-sm outline-none focus:border-accent transition-colors"
                />
              </div>
              <div className="space-y-2">
                <label className="text-xs font-semibold uppercase tracking-wider text-muted-foreground">Estatura (cm)</label>
                <input
                  type="number"
                  step="1"
                  placeholder="Ej. 175"
                  value={formData.vitalSigns.height}
                  onChange={(e) => setFormData({
                    ...formData,
                    vitalSigns: { ...formData.vitalSigns, height: e.target.value ? parseFloat(e.target.value) : "" }
                  })}
                  className="w-full rounded-xl border border-border bg-background px-4 py-2.5 text-sm outline-none focus:border-accent transition-colors"
                />
              </div>
              <div className="space-y-2">
                <label className="text-xs font-semibold uppercase tracking-wider text-muted-foreground">Frecuencia Cardíaca (lpm)</label>
                <input
                  type="number"
                  placeholder="Ej. 80"
                  value={formData.vitalSigns.heartRate}
                  onChange={(e) => setFormData({
                    ...formData,
                    vitalSigns: { ...formData.vitalSigns, heartRate: e.target.value ? parseInt(e.target.value) : "" }
                  })}
                  className="w-full rounded-xl border border-border bg-background px-4 py-2.5 text-sm outline-none focus:border-accent transition-colors"
                />
              </div>
              <div className="space-y-2">
                <label className="text-xs font-semibold uppercase tracking-wider text-muted-foreground">Saturación Oxígeno (%)</label>
                <input
                  type="number"
                  placeholder="Ej. 98"
                  value={formData.vitalSigns.oxygenSaturation}
                  onChange={(e) => setFormData({
                    ...formData,
                    vitalSigns: { ...formData.vitalSigns, oxygenSaturation: e.target.value ? parseInt(e.target.value) : "" }
                  })}
                  className="w-full rounded-xl border border-border bg-background px-4 py-2.5 text-sm outline-none focus:border-accent transition-colors"
                />
              </div>
            </div>
          </div>

          <div className="space-y-6">
            <h3 className="flex items-center gap-2 text-base font-semibold text-foreground border-b border-border pb-3">
              <Activity className="size-5 text-accent" />
              Evaluación Clínica
            </h3>
            
            <div className="grid gap-6 md:grid-cols-2">
              <div className="space-y-2 md:col-span-2">
                <label className="text-sm font-medium text-foreground">Motivo de Consulta <span className="text-destructive">*</span></label>
                <input
                  required
                  type="text"
                  value={formData.reasonForVisit}
                  onChange={(e) => setFormData({ ...formData, reasonForVisit: e.target.value })}
                  className="w-full rounded-xl border border-border bg-background px-4 py-3 text-sm outline-none focus:border-accent transition-colors"
                  placeholder="Razón principal por la que acude el paciente..."
                />
              </div>

              <div className="space-y-2 md:col-span-2">
                <label className="text-sm font-medium text-foreground">Síntomas Reportados</label>
                <textarea
                  value={formData.symptoms}
                  onChange={(e) => setFormData({ ...formData, symptoms: e.target.value })}
                  className="w-full rounded-xl border border-border bg-background px-4 py-3 text-sm outline-none focus:border-accent resize-none h-24 transition-colors"
                  placeholder="Describa los síntomas que reporta el paciente..."
                />
              </div>

              <div className="space-y-2">
                <label className="text-sm font-medium text-foreground">Diagnóstico Clínico</label>
                <textarea
                  value={formData.diagnosis}
                  onChange={(e) => setFormData({ ...formData, diagnosis: e.target.value })}
                  className="w-full rounded-xl border border-border bg-background px-4 py-3 text-sm outline-none focus:border-accent resize-none h-32 transition-colors"
                  placeholder="Evaluación y diagnóstico médico oficial..."
                />
              </div>

              <div className="space-y-2">
                <label className="text-sm font-medium text-foreground">Plan de Tratamiento</label>
                <textarea
                  value={formData.treatment}
                  onChange={(e) => setFormData({ ...formData, treatment: e.target.value })}
                  className="w-full rounded-xl border border-border bg-background px-4 py-3 text-sm outline-none focus:border-accent resize-none h-32 transition-colors"
                  placeholder="Indicaciones, medicamentos prescritos, reposo sugerido..."
                />
              </div>

              <div className="space-y-2">
                <label className="text-sm font-medium text-foreground">Observaciones (Públicas)</label>
                <textarea
                  value={formData.observations}
                  onChange={(e) => setFormData({ ...formData, observations: e.target.value })}
                  className="w-full rounded-xl border border-border bg-background px-4 py-3 text-sm outline-none focus:border-accent resize-none h-24 transition-colors"
                  placeholder="Notas adicionales que pueden ser vistas por el paciente..."
                />
              </div>

              <div className="space-y-2">
                <label className="text-sm font-medium text-foreground">Notas Internas (Privadas)</label>
                <textarea
                  value={formData.internalNotes}
                  onChange={(e) => setFormData({ ...formData, internalNotes: e.target.value })}
                  className="w-full rounded-xl border border-dashed border-amber-500/30 bg-amber-500/5 px-4 py-3 text-sm outline-none focus:border-amber-500 resize-none h-24 transition-colors placeholder:text-muted-foreground/70"
                  placeholder="Notas solo visibles para el personal clínico del sistema..."
                />
              </div>
            </div>
          </div>

          <div className="flex items-center justify-end gap-4 pt-8 border-t border-border">
            <button
              type="button"
              onClick={onClose}
              disabled={isLoading}
              className="rounded-xl px-6 py-3 text-sm font-medium text-foreground hover:bg-accent/10 transition-colors disabled:opacity-50"
            >
              Cancelar
            </button>
            <button
              type="submit"
              disabled={isLoading}
              className="flex items-center gap-2 rounded-xl bg-accent px-8 py-3 text-sm font-medium text-white hover:bg-accent/90 shadow-sm transition-colors disabled:opacity-70"
            >
              {isLoading ? <Loader2 className="size-5 animate-spin" /> : null}
              {isEdit ? "Guardar Cambios" : "Finalizar Consulta"}
            </button>
          </div>
        </form>
      </div>
    </div>
  )
}
