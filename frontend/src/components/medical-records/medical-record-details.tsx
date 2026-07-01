import { ArrowLeft, User, Phone, Droplet, Plus, Calendar, AlertCircle } from "lucide-react"
import { MedicalRecord, MedicalHistoryItem, BloodTypeMap, MedicalHistoryCategoryMap, MedicalHistoryCategory } from "@/types/medical-record"
import { UserRole } from "@/types/auth"

interface Props {
  record: MedicalRecord
  historyItems: MedicalHistoryItem[]
  onClose: () => void
  onAddHistoryItem: () => void
  userRole?: UserRole
}

export function MedicalRecordDetails({ record, historyItems, onClose, onAddHistoryItem, userRole }: Props) {
  const getCategoryIcon = (category: MedicalHistoryCategory) => {
    // Return a generic dot or use different colors per category
    return (
      <div className="mt-1 flex size-3 flex-shrink-0 items-center justify-center rounded-full bg-primary" />
    )
  }

  // Group items by category (optional) or just sort by date descending
  const sortedItems = [...historyItems].sort((a, b) => {
    const dateA = a.recordedDate ? new Date(a.recordedDate).getTime() : 0;
    const dateB = b.recordedDate ? new Date(b.recordedDate).getTime() : 0;
    return dateB - dateA;
  })

  return (
    <div className="space-y-6 animate-in fade-in slide-in-from-bottom-4 duration-500">
      <div className="flex flex-col sm:flex-row justify-between gap-4">
        <div className="flex items-center gap-4">
          <button
            onClick={onClose}
            className="rounded-full p-2 text-muted-foreground hover:bg-accent/10 hover:text-foreground transition-colors"
            title="Volver"
          >
            <ArrowLeft className="size-5" />
          </button>
          <div>
            <h2 className="text-2xl font-bold text-foreground">Expediente Clínico</h2>
            <p className="text-sm text-muted-foreground mt-1">Nº {record.recordNumber}</p>
          </div>
        </div>
        
        {(userRole === UserRole.Administrator || userRole === UserRole.Doctor) && (
          <button
            onClick={onAddHistoryItem}
            className="flex items-center gap-2 rounded-xl bg-primary px-4 py-2 text-sm font-medium text-primary-foreground hover:bg-primary/90 transition-colors shadow-sm h-fit"
          >
            <Plus className="size-4" />
            Añadir Antecedente
          </button>
        )}
      </div>

      <div className="grid gap-6 lg:grid-cols-3">
        {/* Columna Izquierda: Perfil Base */}
        <div className="space-y-6 lg:col-span-1">
          <div className="rounded-2xl border border-border bg-card p-6 shadow-sm">
            <h3 className="mb-6 flex items-center gap-2 text-sm font-semibold uppercase tracking-wider text-muted-foreground">
              <User className="size-4" /> Datos Generales
            </h3>
            
            <div className="space-y-5">
              <div>
                <p className="text-xs text-muted-foreground">Paciente</p>
                <p className="text-base font-medium text-foreground">{record.patientName}</p>
              </div>

              <div>
                <p className="text-xs text-muted-foreground">Tipo de Sangre</p>
                <p className="text-base font-medium text-red-500 flex items-center gap-2">
                  <Droplet className="size-4" />
                  {record.bloodType ? BloodTypeMap[record.bloodType] : "Desconocido"}
                </p>
              </div>

              <div>
                <p className="text-xs text-muted-foreground">Contacto de Emergencia</p>
                <div className="mt-1 bg-muted/30 p-3 rounded-lg border border-border/50">
                  <p className="text-sm font-medium text-foreground">{record.emergencyContactName || "No registrado"}</p>
                  {record.emergencyContactPhone && (
                    <p className="text-xs text-muted-foreground mt-1 flex items-center gap-1">
                      <Phone className="size-3" /> {record.emergencyContactPhone}
                    </p>
                  )}
                </div>
              </div>

              <div>
                <p className="text-xs text-muted-foreground">Alergias Globales</p>
                <p className="text-sm font-medium text-foreground mt-1 bg-red-500/5 p-3 rounded-lg border border-red-500/10 whitespace-pre-wrap">
                  {record.allergies || "Ninguna alergia registrada."}
                </p>
              </div>

              {record.notes && (
                <div>
                  <p className="text-xs text-muted-foreground">Notas Base</p>
                  <p className="text-sm text-foreground mt-1 bg-accent/5 p-3 rounded-lg border border-accent/10 whitespace-pre-wrap">
                    {record.notes}
                  </p>
                </div>
              )}
            </div>
          </div>
        </div>

        {/* Columna Derecha: Línea de tiempo de antecedentes */}
        <div className="space-y-6 lg:col-span-2">
          <div className="rounded-2xl border border-border bg-card p-6 shadow-sm h-full">
            <h3 className="mb-6 flex items-center gap-2 text-sm font-semibold uppercase tracking-wider text-muted-foreground">
              <AlertCircle className="size-4" /> Historial y Antecedentes
            </h3>

            {sortedItems.length === 0 ? (
              <div className="flex h-40 flex-col items-center justify-center rounded-xl border border-dashed border-border bg-muted/20">
                <p className="text-sm font-medium text-muted-foreground">No hay antecedentes registrados para este paciente.</p>
              </div>
            ) : (
              <div className="relative border-l border-border pl-6 ml-2 space-y-8">
                {sortedItems.map((item) => (
                  <div key={item.id} className="relative">
                    {/* Punto del timeline */}
                    <div className="absolute -left-[31px] flex h-6 w-6 items-center justify-center rounded-full bg-card border-2 border-primary">
                      <div className="h-2 w-2 rounded-full bg-primary" />
                    </div>

                    <div className="flex flex-col gap-1">
                      <div className="flex items-center justify-between">
                        <span className="inline-flex items-center rounded-md bg-accent/10 px-2 py-0.5 text-xs font-medium text-accent">
                          {MedicalHistoryCategoryMap[item.category]}
                        </span>
                        <span className="text-xs text-muted-foreground flex items-center gap-1">
                          <Calendar className="size-3" />
                          {item.recordedDate ? new Date(item.recordedDate).toLocaleDateString() : "Sin fecha"}
                        </span>
                      </div>
                      
                      <h4 className="text-base font-semibold text-foreground mt-1">
                        {item.description}
                      </h4>
                      
                      {item.severity && (
                        <p className="text-xs text-red-500 font-medium">Severidad: {item.severity}/10</p>
                      )}

                      {item.details && (
                        <p className="text-sm text-muted-foreground mt-2 bg-muted/20 p-3 rounded-xl">
                          {item.details}
                        </p>
                      )}
                      
                      <p className="text-[10px] text-muted-foreground mt-2 text-right">
                        Registrado por: {item.recordedByName}
                      </p>
                    </div>
                  </div>
                ))}
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  )
}
