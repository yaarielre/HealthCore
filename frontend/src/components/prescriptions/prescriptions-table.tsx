import { Edit2, Trash2, Eye, Download, Loader2, Pill } from "lucide-react"
import { Prescription } from "@/types/prescription"
import { UserRole } from "@/types/auth"

interface Props {
  prescriptions: Prescription[]
  onEdit: (p: Prescription) => void
  onDetails: (p: Prescription) => void
  onDelete: (id: string) => void
  onDownloadPdf: (p: Prescription) => void
  isPdfLoading: string | null
  userRole?: UserRole
}

export function PrescriptionsTable({
  prescriptions,
  onEdit,
  onDetails,
  onDelete,
  onDownloadPdf,
  isPdfLoading,
  userRole,
}: Props) {
  const canEdit = userRole === UserRole.Administrator || userRole === UserRole.Doctor
  const canDelete = userRole === UserRole.Administrator

  if (prescriptions.length === 0) {
    return (
      <div className="flex flex-col items-center justify-center rounded-2xl border border-dashed border-border bg-card py-16 text-center">
        <span className="flex size-14 items-center justify-center rounded-full bg-emerald-500/10 text-emerald-500 mb-3">
          <Pill className="size-7" />
        </span>
        <p className="font-semibold text-foreground">Sin recetas registradas</p>
        <p className="text-sm text-muted-foreground mt-1">
          {canEdit ? "Crea la primera receta médica haciendo clic en el botón de arriba." : "No hay recetas disponibles en este momento."}
        </p>
      </div>
    )
  }

  return (
    <div className="rounded-2xl border border-border bg-card shadow-sm overflow-hidden">
      <div className="overflow-x-auto">
        <table className="w-full text-left">
          <thead>
            <tr className="border-b border-border bg-muted/30 text-xs text-muted-foreground">
              <th className="px-6 py-4 font-medium">Medicamento</th>
              <th className="px-6 py-4 font-medium">Paciente</th>
              <th className="px-6 py-4 font-medium">Médico</th>
              <th className="px-6 py-4 font-medium">Dosis / Frecuencia</th>
              <th className="px-6 py-4 font-medium">Duración</th>
              <th className="px-6 py-4 font-medium">Fecha</th>
              <th className="px-6 py-4 font-medium text-right">Acciones</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-border text-sm">
            {prescriptions.map((p) => (
              <tr key={p.id} className="hover:bg-muted/20 transition-colors">
                <td className="px-6 py-4">
                  <p className="font-semibold text-foreground">{p.medication}</p>
                  {p.instructions && (
                    <p className="text-xs text-muted-foreground mt-0.5 max-w-[200px] truncate">{p.instructions}</p>
                  )}
                </td>
                <td className="px-6 py-4 text-foreground">{p.patientName}</td>
                <td className="px-6 py-4 text-muted-foreground">{p.doctorName}</td>
                <td className="px-6 py-4">
                  <p className="text-foreground">{p.dosage}</p>
                  <p className="text-xs text-muted-foreground">{p.frequency}</p>
                </td>
                <td className="px-6 py-4">
                  <span className="inline-flex items-center rounded-full bg-emerald-500/10 px-2.5 py-0.5 text-xs font-medium text-emerald-600">
                    {p.duration}
                  </span>
                </td>
                <td className="px-6 py-4 text-muted-foreground whitespace-nowrap">
                  {new Date(p.createdAt).toLocaleDateString("es-DO", {
                    day: "2-digit",
                    month: "short",
                    year: "numeric",
                  })}
                </td>
                <td className="px-6 py-4">
                  <div className="flex items-center justify-end gap-1">
                    {/* PDF */}
                    <button
                      onClick={() => onDownloadPdf(p)}
                      disabled={isPdfLoading === p.id}
                      title="Descargar PDF"
                      className="rounded-lg p-1.5 text-muted-foreground hover:bg-muted hover:text-foreground transition-colors disabled:opacity-50"
                    >
                      {isPdfLoading === p.id ? (
                        <Loader2 className="size-4 animate-spin" />
                      ) : (
                        <Download className="size-4" />
                      )}
                    </button>
                    {/* Ver */}
                    <button
                      onClick={() => onDetails(p)}
                      title="Ver detalles"
                      className="rounded-lg p-1.5 text-muted-foreground hover:bg-muted hover:text-blue-500 transition-colors"
                    >
                      <Eye className="size-4" />
                    </button>
                    {/* Editar */}
                    {canEdit && (
                      <button
                        onClick={() => onEdit(p)}
                        title="Editar"
                        className="rounded-lg p-1.5 text-muted-foreground hover:bg-muted hover:text-foreground transition-colors"
                      >
                        <Edit2 className="size-4" />
                      </button>
                    )}
                    {/* Eliminar */}
                    {canDelete && (
                      <button
                        onClick={() => onDelete(p.id)}
                        title="Eliminar"
                        className="rounded-lg p-1.5 text-muted-foreground hover:bg-destructive/10 hover:text-destructive transition-colors"
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
    </div>
  )
}
