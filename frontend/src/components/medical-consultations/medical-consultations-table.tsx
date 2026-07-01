import { Edit2, Trash2, Stethoscope, Clock, Eye } from "lucide-react"
import { MedicalConsultation } from "@/types/medical-consultation"
import { UserRole } from "@/types/auth"

interface Props {
  consultations: MedicalConsultation[]
  onEdit: (consultation: MedicalConsultation) => void
  onDelete: (consultation: MedicalConsultation) => void
  onDetails: (consultation: MedicalConsultation) => void
  userRole?: UserRole
}

function formatDate(dateString: string) {
  const d = new Date(dateString)
  return d.toLocaleDateString("es-ES", {
    year: "numeric",
    month: "short",
    day: "numeric",
    hour: "2-digit",
    minute: "2-digit"
  })
}

export function MedicalConsultationsTable({ consultations, onEdit, onDelete, onDetails, userRole }: Props) {
  if (consultations.length === 0) {
    return (
      <div className="flex h-64 flex-col items-center justify-center rounded-2xl border border-dashed border-border bg-card">
        <Stethoscope className="size-8 text-muted-foreground mb-4" />
        <h3 className="text-sm font-semibold text-foreground">No hay consultas médicas registradas</h3>
        <p className="text-xs text-muted-foreground mt-1">Aquí aparecerá el historial de atenciones a pacientes.</p>
      </div>
    )
  }

  return (
    <div className="overflow-x-auto rounded-2xl border border-border bg-card shadow-sm">
      <table className="w-full text-left text-sm">
        <thead className="border-b border-border bg-accent/5">
          <tr>
            <th className="px-6 py-4 font-semibold text-foreground">Fecha</th>
            <th className="px-6 py-4 font-semibold text-foreground">Paciente</th>
            <th className="px-6 py-4 font-semibold text-foreground">Médico Tratante</th>
            <th className="px-6 py-4 font-semibold text-foreground">Motivo</th>
            <th className="px-6 py-4 font-semibold text-foreground">Diagnóstico</th>
            <th className="px-6 py-4 text-right font-semibold text-foreground">Acciones</th>
          </tr>
        </thead>
        <tbody className="divide-y divide-border">
          {consultations.map((consultation) => (
            <tr key={consultation.id} className="transition-colors hover:bg-accent/5">
              <td className="px-6 py-4 text-foreground whitespace-nowrap">
                <div className="flex items-center gap-2 text-xs">
                  <Clock className="size-3.5 text-muted-foreground" />
                  {formatDate(consultation.createdAt)}
                </div>
              </td>
              <td className="px-6 py-4 font-medium text-foreground">{consultation.patientName}</td>
              <td className="px-6 py-4 text-muted-foreground">{consultation.doctorName}</td>
              <td className="px-6 py-4 text-muted-foreground truncate max-w-[150px]" title={consultation.reasonForVisit}>
                {consultation.reasonForVisit}
              </td>
              <td className="px-6 py-4 text-muted-foreground truncate max-w-[150px]" title={consultation.diagnosis || ""}>
                {consultation.diagnosis || "No especificado"}
              </td>
              <td className="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
                <div className="flex items-center justify-end gap-2">
                  <button
                    onClick={() => onDetails(consultation)}
                    className="p-2 text-muted-foreground hover:text-accent hover:bg-accent/10 rounded-lg transition-colors"
                    title="Ver detalles"
                  >
                    <Eye className="size-4" />
                  </button>
                  <button
                    onClick={() => onEdit(consultation)}
                    className="p-2 text-muted-foreground hover:text-blue-500 hover:bg-blue-500/10 rounded-lg transition-colors"
                    title="Editar"
                  >
                    <Edit2 className="size-4" />
                  </button>
                  {userRole === UserRole.Administrator && (
                    <button
                      onClick={() => onDelete(consultation)}
                      className="p-2 text-muted-foreground hover:text-destructive hover:bg-destructive/10 rounded-lg transition-colors"
                      title="Eliminar"
                    >
                      <Trash2 className="size-4" />
                    </button>
                  )}
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}
