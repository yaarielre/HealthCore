import { FolderOpen, Edit2, Droplet, User, Phone } from "lucide-react"
import { MedicalRecord, BloodTypeMap } from "@/types/medical-record"
import { UserRole } from "@/types/auth"

interface Props {
  records: MedicalRecord[]
  onEdit: (record: MedicalRecord) => void
  onDetails: (record: MedicalRecord) => void
  userRole?: UserRole
}

export function MedicalRecordsTable({ records, onEdit, onDetails, userRole }: Props) {
  if (records.length === 0) {
    return (
      <div className="flex h-64 flex-col items-center justify-center rounded-2xl border border-dashed border-border bg-card">
        <FolderOpen className="mb-4 size-10 text-muted-foreground/50" />
        <p className="text-lg font-medium text-foreground">No hay expedientes médicos</p>
        <p className="text-sm text-muted-foreground">Comienza creando un expediente para un paciente.</p>
      </div>
    )
  }

  return (
    <div className="overflow-hidden rounded-2xl border border-border bg-card shadow-sm">
      <div className="overflow-x-auto">
        <table className="w-full text-left text-sm">
          <thead className="bg-muted/50 text-muted-foreground">
            <tr>
              <th className="px-6 py-4 font-medium uppercase tracking-wider text-xs">Paciente</th>
              <th className="px-6 py-4 font-medium uppercase tracking-wider text-xs">Nº Expediente</th>
              <th className="px-6 py-4 font-medium uppercase tracking-wider text-xs">Tipo de Sangre</th>
              <th className="px-6 py-4 font-medium uppercase tracking-wider text-xs">Contacto Emergencia</th>
              <th className="px-6 py-4 font-medium text-right uppercase tracking-wider text-xs">Acciones</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-border">
            {records.map((record) => (
              <tr 
                key={record.id} 
                className="group hover:bg-muted/30 transition-colors"
              >
                <td className="px-6 py-4 whitespace-nowrap">
                  <div className="flex items-center gap-3">
                    <div className="rounded-full bg-blue-500/10 p-2 text-blue-500">
                      <User className="size-4" />
                    </div>
                    <span className="font-medium text-foreground">{record.patientName}</span>
                  </div>
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-muted-foreground">
                  <span className="inline-flex items-center rounded-md bg-accent/10 px-2.5 py-0.5 text-xs font-medium text-accent">
                    {record.recordNumber}
                  </span>
                </td>
                <td className="px-6 py-4 whitespace-nowrap">
                  <div className="flex items-center gap-2 text-muted-foreground">
                    <Droplet className="size-4 text-red-500/70" />
                    {record.bloodType ? BloodTypeMap[record.bloodType] : "Desconocido"}
                  </div>
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-muted-foreground">
                  {record.emergencyContactName ? (
                    <div className="flex flex-col">
                      <span>{record.emergencyContactName}</span>
                      <span className="text-xs flex items-center gap-1"><Phone className="size-3"/> {record.emergencyContactPhone}</span>
                    </div>
                  ) : (
                    "No registrado"
                  )}
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
                  <div className="flex items-center justify-end gap-2">
                    <button
                      onClick={() => onDetails(record)}
                      className="p-2 text-muted-foreground hover:text-accent hover:bg-accent/10 rounded-lg transition-colors"
                      title="Abrir Expediente"
                    >
                      <FolderOpen className="size-4" />
                    </button>
                    {(userRole === UserRole.Administrator || userRole === UserRole.Doctor) && (
                      <button
                        onClick={() => onEdit(record)}
                        className="p-2 text-muted-foreground hover:text-blue-500 hover:bg-blue-500/10 rounded-lg transition-colors"
                        title="Editar Datos Base"
                      >
                        <Edit2 className="size-4" />
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
