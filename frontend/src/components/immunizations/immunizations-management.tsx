"use client"

import { Search, Plus, Syringe, Edit2, Trash2, Calendar, User } from "lucide-react"
import { useImmunizations } from "@/hooks/useImmunizations"
import { UserRole } from "@/types/auth"
import { Immunization, CreateImmunizationFormData } from "@/types/clinical"
import { Patient } from "@/types/patient"
import { X, Save } from "lucide-react"

// ─── Inline Form ────────────────────────────────────────────────────────────
function ImmunizationForm({
  title, formData, setFormData, onSubmit, onClose, isLoading, patients, isEditMode = false
}: {
  title: string
  formData: CreateImmunizationFormData
  setFormData: (d: CreateImmunizationFormData) => void
  onSubmit: (e: React.FormEvent) => void
  onClose: () => void
  isLoading: boolean
  patients: Patient[]
  isEditMode?: boolean
}) {
  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-background/80 backdrop-blur-sm animate-in fade-in duration-200">
      <div className="w-full max-w-2xl rounded-2xl border border-border bg-card shadow-xl animate-in zoom-in-95 duration-200 max-h-[90vh] flex flex-col">
        <div className="flex items-center justify-between border-b border-border p-6">
          <div className="flex items-center gap-3">
            <span className="flex size-10 items-center justify-center rounded-xl bg-blue-500/10 text-blue-500">
              <Syringe className="size-5" />
            </span>
            <h2 className="text-xl font-bold text-foreground">{title}</h2>
          </div>
          <button onClick={onClose} className="rounded-full p-2 text-muted-foreground hover:bg-muted transition-colors">
            <X className="size-5" />
          </button>
        </div>
        <form onSubmit={onSubmit} className="p-6 overflow-y-auto">
          <div className="grid grid-cols-1 gap-5 sm:grid-cols-2">
            {!isEditMode && (
              <div className="space-y-2 sm:col-span-2">
                <label className="text-sm font-medium text-foreground">Paciente <span className="text-destructive">*</span></label>
                <select required value={formData.patientId} onChange={(e) => setFormData({ ...formData, patientId: e.target.value })}
                  className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring">
                  <option value="">Seleccione un paciente...</option>
                  {patients.map((p) => <option key={p.id} value={p.id}>{p.firstName} {p.lastName}</option>)}
                </select>
              </div>
            )}
            <div className="space-y-2 sm:col-span-2">
              <label className="text-sm font-medium text-foreground">Vacuna / Nombre <span className="text-destructive">*</span></label>
              <input required value={formData.vaccineName} onChange={(e) => setFormData({ ...formData, vaccineName: e.target.value })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring"
                placeholder="Ej. Influenza, Hepatitis B, Tétanos..." />
            </div>
            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Número de Dosis <span className="text-destructive">*</span></label>
              <input required type="number" min={1} value={formData.doseNumber} onChange={(e) => setFormData({ ...formData, doseNumber: Number(e.target.value) })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring" />
            </div>
            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Fecha de Aplicación <span className="text-destructive">*</span></label>
              <input required type="date" value={formData.applicationDate} onChange={(e) => setFormData({ ...formData, applicationDate: e.target.value })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring" />
            </div>
            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Próxima Dosis</label>
              <input type="date" value={formData.nextDoseDate} onChange={(e) => setFormData({ ...formData, nextDoseDate: e.target.value })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring" />
            </div>
            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Número de Lote</label>
              <input value={formData.batchNumber} onChange={(e) => setFormData({ ...formData, batchNumber: e.target.value })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring"
                placeholder="Ej. BATCH-2024-001" />
            </div>
            <div className="space-y-2 sm:col-span-2">
              <label className="text-sm font-medium text-foreground">Administrado Por</label>
              <input value={formData.administeredBy} onChange={(e) => setFormData({ ...formData, administeredBy: e.target.value })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring"
                placeholder="Nombre del profesional que administró la vacuna" />
            </div>
            <div className="space-y-2 sm:col-span-2">
              <label className="text-sm font-medium text-foreground">Notas</label>
              <textarea value={formData.notes} onChange={(e) => setFormData({ ...formData, notes: e.target.value })}
                className="w-full min-h-[80px] rounded-xl border border-input bg-background px-4 py-3 text-sm focus:outline-none focus:ring-2 focus:ring-ring resize-none"
                placeholder="Observaciones adicionales..." />
            </div>
          </div>
          <div className="flex items-center justify-end gap-3 border-t border-border pt-5 mt-5">
            <button type="button" onClick={onClose} disabled={isLoading}
              className="rounded-xl px-4 py-2.5 text-sm font-medium text-muted-foreground hover:bg-muted transition-colors disabled:opacity-50">
              Cancelar
            </button>
            <button type="submit" disabled={isLoading}
              className="flex items-center gap-2 rounded-xl bg-blue-600 px-6 py-2.5 text-sm font-medium text-white hover:bg-blue-700 transition-colors shadow-sm disabled:opacity-50">
              <Save className="size-4" />
              {isLoading ? "Guardando..." : "Guardar"}
            </button>
          </div>
        </form>
      </div>
    </div>
  )
}

// ─── Table Row ───────────────────────────────────────────────────────────────
function ImmunizationRow({ item, onEdit, onDelete, canEdit, canDelete }: {
  item: Immunization; onEdit: (i: Immunization) => void; onDelete: (id: string) => void
  canEdit: boolean; canDelete: boolean
}) {
  const appDate = new Date(item.applicationDate)
  const nextDate = item.nextDoseDate ? new Date(item.nextDoseDate) : null
  const isOverdue = nextDate && nextDate < new Date()

  return (
    <tr className="hover:bg-muted/20 transition-colors">
      <td className="px-6 py-4">
        <p className="font-semibold text-foreground">{item.vaccineName}</p>
        {item.batchNumber && <p className="text-xs text-muted-foreground mt-0.5">Lote: {item.batchNumber}</p>}
      </td>
      <td className="px-6 py-4 text-foreground">{item.patientName}</td>
      <td className="px-6 py-4">
        <span className="inline-flex items-center rounded-full bg-blue-500/10 px-2.5 py-0.5 text-xs font-medium text-blue-600">
          Dosis #{item.doseNumber}
        </span>
      </td>
      <td className="px-6 py-4">
        <div className="flex items-center gap-1.5 text-sm text-foreground">
          <Calendar className="size-3.5 text-muted-foreground" />
          {appDate.toLocaleDateString("es-DO", { day: "2-digit", month: "short", year: "numeric" })}
        </div>
      </td>
      <td className="px-6 py-4">
        {nextDate ? (
          <span className={`inline-flex items-center rounded-full px-2.5 py-0.5 text-xs font-medium ${isOverdue ? "bg-red-500/10 text-red-600" : "bg-amber-500/10 text-amber-600"}`}>
            {isOverdue ? "⚠ " : ""}{nextDate.toLocaleDateString("es-DO", { day: "2-digit", month: "short", year: "numeric" })}
          </span>
        ) : <span className="text-xs text-muted-foreground">—</span>}
      </td>
      <td className="px-6 py-4 text-muted-foreground text-sm">{item.administeredBy || "—"}</td>
      <td className="px-6 py-4">
        <div className="flex items-center justify-end gap-1">
          {canEdit && (
            <button onClick={() => onEdit(item)} className="rounded-lg p-1.5 text-muted-foreground hover:bg-muted hover:text-foreground transition-colors">
              <Edit2 className="size-4" />
            </button>
          )}
          {canDelete && (
            <button onClick={() => onDelete(item.id)} className="rounded-lg p-1.5 text-muted-foreground hover:bg-destructive/10 hover:text-destructive transition-colors">
              <Trash2 className="size-4" />
            </button>
          )}
        </div>
      </td>
    </tr>
  )
}

// ─── Main Management ─────────────────────────────────────────────────────────
export function ImmunizationsManagement() {
  const {
    filtered, patients, isLoading, isSubmitLoading,
    searchQuery, setSearchQuery, formData, setFormData,
    isCreateOpen, setIsCreateOpen, isEditOpen, setIsEditOpen,
    openCreate, openEdit, handleCreate, handleEdit, handleDelete,
    userRole,
  } = useImmunizations()

  const canCreate = userRole === UserRole.Administrator || userRole === UserRole.Doctor || userRole === UserRole.Nurse
  const canEdit = userRole === UserRole.Administrator || userRole === UserRole.Doctor
  const canDelete = userRole === UserRole.Administrator

  return (
    <div className="space-y-6 animate-in fade-in duration-500">
      <div className="flex flex-col sm:flex-row justify-between gap-4">
        <div>
          <div className="flex items-center gap-3 mb-1">
            <span className="flex size-9 items-center justify-center rounded-xl bg-blue-500/10 text-blue-500">
              <Syringe className="size-5" />
            </span>
            <h2 className="text-2xl font-bold tracking-tight text-foreground">Vacunas / Inmunizaciones</h2>
          </div>
          <p className="text-sm text-muted-foreground ml-12">Esquema de vacunación y registro de inmunizaciones de los pacientes.</p>
        </div>
        {canCreate && (
          <button onClick={openCreate} className="flex items-center gap-2 rounded-xl bg-blue-600 px-4 py-2 text-sm font-medium text-white hover:bg-blue-700 transition-colors shadow-sm h-fit">
            <Plus className="size-4" /> Registrar Vacuna
          </button>
        )}
      </div>

      <div className="rounded-2xl border border-border bg-card p-4 shadow-sm">
        <div className="relative">
          <Search className="absolute left-3 top-1/2 -translate-y-1/2 size-4 text-muted-foreground" />
          <input type="text" placeholder="Buscar por paciente o vacuna..." value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            className="w-full rounded-xl border-none bg-muted/50 py-2.5 pl-10 pr-4 text-sm focus:outline-none focus:ring-2 focus:ring-primary/20" />
        </div>
      </div>

      {isLoading ? (
        <div className="flex h-64 items-center justify-center">
          <div className="size-8 animate-spin rounded-full border-4 border-blue-500 border-t-transparent" />
        </div>
      ) : filtered.length === 0 ? (
        <div className="flex flex-col items-center justify-center rounded-2xl border border-dashed border-border bg-card py-16 text-center">
          <span className="flex size-14 items-center justify-center rounded-full bg-blue-500/10 text-blue-500 mb-3">
            <Syringe className="size-7" />
          </span>
          <p className="font-semibold text-foreground">Sin vacunas registradas</p>
          <p className="text-sm text-muted-foreground mt-1">
            {canCreate ? "Registra la primera vacuna haciendo clic en el botón de arriba." : "No hay inmunizaciones disponibles."}
          </p>
        </div>
      ) : (
        <div className="rounded-2xl border border-border bg-card shadow-sm overflow-hidden">
          <div className="overflow-x-auto">
            <table className="w-full text-left">
              <thead>
                <tr className="border-b border-border bg-muted/30 text-xs text-muted-foreground">
                  <th className="px-6 py-4 font-medium">Vacuna</th>
                  <th className="px-6 py-4 font-medium">Paciente</th>
                  <th className="px-6 py-4 font-medium">Dosis</th>
                  <th className="px-6 py-4 font-medium">Aplicación</th>
                  <th className="px-6 py-4 font-medium">Próxima Dosis</th>
                  <th className="px-6 py-4 font-medium">Administrado Por</th>
                  <th className="px-6 py-4 font-medium text-right">Acciones</th>
                </tr>
              </thead>
              <tbody className="divide-y divide-border text-sm">
                {filtered.map((item) => (
                  <ImmunizationRow key={item.id} item={item} onEdit={openEdit} onDelete={handleDelete} canEdit={canEdit} canDelete={canDelete} />
                ))}
              </tbody>
            </table>
          </div>
        </div>
      )}

      {isCreateOpen && (
        <ImmunizationForm title="Registrar Vacuna" formData={formData} setFormData={setFormData}
          onSubmit={handleCreate} onClose={() => setIsCreateOpen(false)}
          isLoading={isSubmitLoading} patients={patients} />
      )}
      {isEditOpen && (
        <ImmunizationForm title="Editar Vacuna" formData={formData} setFormData={setFormData}
          onSubmit={handleEdit} onClose={() => setIsEditOpen(false)}
          isLoading={isSubmitLoading} patients={patients} isEditMode />
      )}
    </div>
  )
}
