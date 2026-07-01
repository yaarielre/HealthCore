import { ArrowLeft, Edit2, Clock, Calendar, User, Stethoscope, HeartPulse, FileText, Activity } from "lucide-react"
import { MedicalConsultation } from "@/types/medical-consultation"
import { UserRole } from "@/types/auth"

interface Props {
  consultation: MedicalConsultation
  onClose: () => void
  onEdit: (consultation: MedicalConsultation) => void
  userRole?: UserRole
}

export function MedicalConsultationDetails({ consultation, onClose, onEdit, userRole }: Props) {
  const vs = consultation.vitalSigns && consultation.vitalSigns.length > 0 ? consultation.vitalSigns[0] : null
  const formattedDate = new Date(consultation.createdAt).toLocaleDateString("es-ES", {
    weekday: 'long', year: 'numeric', month: 'long', day: 'numeric', hour: '2-digit', minute: '2-digit'
  })

  return (
    <div className="space-y-6 animate-in fade-in slide-in-from-bottom-4 duration-500">
      <div className="flex flex-col sm:flex-row justify-between gap-4">
        <div className="flex items-center gap-4">
          <button
            onClick={onClose}
            className="rounded-full p-2 text-muted-foreground hover:bg-accent/10 hover:text-foreground transition-colors"
            title="Volver a la lista"
          >
            <ArrowLeft className="size-5" />
          </button>
          <div>
            <h2 className="text-2xl font-bold text-foreground">Detalles de la Consulta</h2>
            <p className="text-sm text-muted-foreground mt-1 capitalize">{formattedDate}</p>
          </div>
        </div>
        <button
          onClick={() => onEdit(consultation)}
          className="flex items-center gap-2 rounded-xl bg-accent px-4 py-2 text-sm font-medium text-white hover:bg-accent/90 transition-colors shadow-sm h-fit"
        >
          <Edit2 className="size-4" />
          Editar Consulta
        </button>
      </div>

      <div className="grid gap-6 lg:grid-cols-3">
        {/* Columna Izquierda: Info General y Signos Vitales */}
        <div className="space-y-6 lg:col-span-1">
          {/* Card de Actores */}
          <div className="rounded-2xl border border-border bg-card p-6 shadow-sm">
            <h3 className="mb-4 text-sm font-semibold uppercase tracking-wider text-muted-foreground">Involucrados</h3>
            <div className="space-y-4">
              <div className="flex items-start gap-3">
                <div className="rounded-full bg-blue-500/10 p-2 text-blue-500">
                  <User className="size-4" />
                </div>
                <div>
                  <p className="text-sm font-medium text-foreground">{consultation.patientName}</p>
                  <p className="text-xs text-muted-foreground">Paciente</p>
                </div>
              </div>
              <div className="flex items-start gap-3">
                <div className="rounded-full bg-accent/10 p-2 text-accent">
                  <Stethoscope className="size-4" />
                </div>
                <div>
                  <p className="text-sm font-medium text-foreground">Dr. {consultation.doctorName}</p>
                  <p className="text-xs text-muted-foreground">Médico Tratante</p>
                </div>
              </div>
            </div>
          </div>

          {/* Card de Signos Vitales */}
          <div className="rounded-2xl border border-border bg-card p-6 shadow-sm">
            <h3 className="mb-4 flex items-center gap-2 text-sm font-semibold uppercase tracking-wider text-muted-foreground">
              <HeartPulse className="size-4" /> Signos Vitales
            </h3>
            {vs ? (
              <div className="grid grid-cols-2 gap-4">
                <div>
                  <p className="text-xs text-muted-foreground">P. Arterial</p>
                  <p className="text-sm font-medium text-foreground">{vs.bloodPressure || "--"} <span className="text-xs font-normal">mmHg</span></p>
                </div>
                <div>
                  <p className="text-xs text-muted-foreground">Temperatura</p>
                  <p className="text-sm font-medium text-foreground">{vs.temperature || "--"} <span className="text-xs font-normal">°C</span></p>
                </div>
                <div>
                  <p className="text-xs text-muted-foreground">F. Cardíaca</p>
                  <p className="text-sm font-medium text-foreground">{vs.heartRate || "--"} <span className="text-xs font-normal">lpm</span></p>
                </div>
                <div>
                  <p className="text-xs text-muted-foreground">Sat. Oxígeno</p>
                  <p className="text-sm font-medium text-foreground">{vs.oxygenSaturation || "--"} <span className="text-xs font-normal">%</span></p>
                </div>
                <div>
                  <p className="text-xs text-muted-foreground">Peso</p>
                  <p className="text-sm font-medium text-foreground">{vs.weight || "--"} <span className="text-xs font-normal">kg</span></p>
                </div>
                <div>
                  <p className="text-xs text-muted-foreground">Estatura</p>
                  <p className="text-sm font-medium text-foreground">{vs.height || "--"} <span className="text-xs font-normal">cm</span></p>
                </div>
              </div>
            ) : (
              <p className="text-sm text-muted-foreground italic">No se registraron signos vitales.</p>
            )}
          </div>
        </div>

        {/* Columna Derecha: Detalles Clínicos */}
        <div className="space-y-6 lg:col-span-2">
          <div className="rounded-2xl border border-border bg-card p-6 shadow-sm">
            <h3 className="mb-6 flex items-center gap-2 text-sm font-semibold uppercase tracking-wider text-muted-foreground">
              <Activity className="size-4" /> Evaluación Clínica
            </h3>
            
            <div className="space-y-6">
              <div>
                <h4 className="text-sm font-medium text-foreground mb-1">Motivo de Consulta</h4>
                <p className="text-sm text-muted-foreground bg-accent/5 p-3 rounded-xl border border-border/50">{consultation.reasonForVisit}</p>
              </div>
              
              <div>
                <h4 className="text-sm font-medium text-foreground mb-1">Síntomas Reportados</h4>
                <p className="text-sm text-muted-foreground whitespace-pre-wrap">{consultation.symptoms || "Sin registro de síntomas."}</p>
              </div>
              
              <div>
                <h4 className="text-sm font-medium text-foreground mb-1">Diagnóstico</h4>
                <p className="text-sm text-foreground bg-blue-500/5 p-4 rounded-xl border border-blue-500/10 whitespace-pre-wrap">
                  {consultation.diagnosis || "Sin diagnóstico definitivo."}
                </p>
              </div>

              <div>
                <h4 className="text-sm font-medium text-foreground mb-1">Plan de Tratamiento</h4>
                <p className="text-sm text-foreground bg-green-500/5 p-4 rounded-xl border border-green-500/10 whitespace-pre-wrap">
                  {consultation.treatment || "Sin tratamiento registrado."}
                </p>
              </div>
            </div>
          </div>

          <div className="grid gap-6 md:grid-cols-2">
            <div className="rounded-2xl border border-border bg-card p-6 shadow-sm">
              <h4 className="flex items-center gap-2 text-sm font-semibold uppercase tracking-wider text-muted-foreground mb-3">
                <FileText className="size-4" /> Observaciones Generales
              </h4>
              <p className="text-sm text-muted-foreground whitespace-pre-wrap">
                {consultation.observations || "Sin observaciones."}
              </p>
            </div>
            
            {userRole !== UserRole.Patient && (
              <div className="rounded-2xl border border-amber-500/20 bg-amber-500/5 p-6 shadow-sm">
                <h4 className="flex items-center gap-2 text-sm font-semibold uppercase tracking-wider text-amber-600 dark:text-amber-500 mb-3">
                  Notas Internas (Privadas)
                </h4>
                <p className="text-sm text-amber-700/80 dark:text-amber-400/80 whitespace-pre-wrap">
                  {consultation.internalNotes || "Sin notas internas."}
                </p>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  )
}
