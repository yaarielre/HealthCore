"use client"

import { Search, Plus, ImageIcon, X, Save, Eye, Trash2, FileImage, Calendar, User } from "lucide-react"
import { useMedicalImages } from "@/hooks/useMedicalImages"
import { UserRole } from "@/types/auth"
import { MedicalImage } from "@/types/clinical"

// ─── Details Modal ────────────────────────────────────────────────────────────
function ImageDetailsModal({ image, onClose, onDelete, canDelete }: {
  image: MedicalImage; onClose: () => void; onDelete: (id: string) => void; canDelete: boolean
}) {
  const formatBytes = (bytes: number) => {
    if (bytes === 0) return "0 Bytes"
    const k = 1024, sizes = ["Bytes", "KB", "MB", "GB"]
    const i = Math.floor(Math.log(bytes) / Math.log(k))
    return parseFloat((bytes / Math.pow(k, i)).toFixed(2)) + " " + sizes[i]
  }

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-background/80 backdrop-blur-sm animate-in fade-in duration-200">
      <div className="w-full max-w-lg rounded-2xl border border-border bg-card shadow-xl animate-in zoom-in-95 duration-200">
        <div className="flex items-center justify-between border-b border-border p-6">
          <div className="flex items-center gap-3">
            <span className="flex size-10 items-center justify-center rounded-xl bg-cyan-500/10 text-cyan-500">
              <FileImage className="size-5" />
            </span>
            <div>
              <h2 className="text-lg font-bold text-foreground">{image.imageType}</h2>
              {image.bodyPart && <p className="text-xs text-muted-foreground">Zona: {image.bodyPart}</p>}
            </div>
          </div>
          <button onClick={onClose} className="rounded-full p-2 text-muted-foreground hover:bg-muted transition-colors">
            <X className="size-5" />
          </button>
        </div>
        <div className="p-6 space-y-4">
          <div className="grid grid-cols-2 gap-3 text-sm">
            <div><p className="text-xs text-muted-foreground">Paciente</p><p className="font-semibold">{image.patientName}</p></div>
            <div><p className="text-xs text-muted-foreground">Archivo</p><p className="font-mono text-xs truncate">{image.fileName}</p></div>
            <div><p className="text-xs text-muted-foreground">Tipo de contenido</p><p className="font-medium">{image.contentType}</p></div>
            <div><p className="text-xs text-muted-foreground">Tamaño</p><p className="font-medium">{formatBytes(image.fileSizeBytes)}</p></div>
            {image.takenAt && (
              <div>
                <p className="text-xs text-muted-foreground">Fecha de toma</p>
                <p className="font-medium">{new Date(image.takenAt).toLocaleDateString("es-DO")}</p>
              </div>
            )}
            {image.interpretedByName && (
              <div><p className="text-xs text-muted-foreground">Interpretado por</p><p className="font-medium">{image.interpretedByName}</p></div>
            )}
          </div>
          {image.filePath && (
            <div className="rounded-xl bg-cyan-500/5 border border-cyan-500/20 p-3">
              <p className="text-xs text-muted-foreground mb-1">Ruta del Archivo</p>
              <p className="font-mono text-xs break-all text-foreground">{image.filePath}</p>
            </div>
          )}
          {image.description && (
            <div>
              <p className="text-xs text-muted-foreground mb-1">Descripción</p>
              <p className="text-sm">{image.description}</p>
            </div>
          )}
          {image.interpretation && (
            <div className="rounded-xl bg-muted/30 border border-border p-3">
              <p className="text-xs font-medium text-muted-foreground mb-1">Interpretación Clínica</p>
              <p className="text-sm italic">{image.interpretation}</p>
            </div>
          )}
        </div>
        <div className="border-t border-border px-6 py-4 flex justify-between">
          {canDelete ? (
            <button onClick={() => onDelete(image.id)} className="flex items-center gap-1.5 rounded-xl px-3 py-2 text-sm text-destructive hover:bg-destructive/10 transition-colors">
              <Trash2 className="size-4" /> Eliminar
            </button>
          ) : <span />}
          <button onClick={onClose} className="rounded-xl px-4 py-2 text-sm font-medium text-muted-foreground hover:bg-muted transition-colors">
            Cerrar
          </button>
        </div>
      </div>
    </div>
  )
}

// ─── Create Modal ─────────────────────────────────────────────────────────────
function CreateImageModal({ hook }: { hook: ReturnType<typeof useMedicalImages> }) {
  const { formData, setFormData, consultations, patients, IMAGE_TYPES, isSubmitLoading, handleCreate, setIsCreateOpen, handleConsultationChange } = hook
  const selectedConsultation = consultations.find((c) => c.id === formData.medicalConsultationId)

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-background/80 backdrop-blur-sm animate-in fade-in duration-200">
      <div className="w-full max-w-2xl rounded-2xl border border-border bg-card shadow-xl animate-in zoom-in-95 duration-200 max-h-[90vh] flex flex-col">
        <div className="flex items-center justify-between border-b border-border p-6">
          <div className="flex items-center gap-3">
            <span className="flex size-10 items-center justify-center rounded-xl bg-cyan-500/10 text-cyan-500">
              <ImageIcon className="size-5" />
            </span>
            <h2 className="text-xl font-bold text-foreground">Registrar Imagen Médica</h2>
          </div>
          <button onClick={() => setIsCreateOpen(false)} className="rounded-full p-2 text-muted-foreground hover:bg-muted transition-colors">
            <X className="size-5" />
          </button>
        </div>
        <form onSubmit={handleCreate} className="p-6 overflow-y-auto">
          <div className="grid grid-cols-1 gap-5 sm:grid-cols-2">
            {/* Consultation */}
            <div className="space-y-2 sm:col-span-2">
              <label className="text-sm font-medium text-foreground">Consulta Vinculada</label>
              <select value={formData.medicalConsultationId} onChange={(e) => handleConsultationChange(e.target.value)}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring">
                <option value="">Sin consulta específica</option>
                {consultations.map((c) => <option key={c.id} value={c.id}>{c.patientName} — {new Date(c.createdAt).toLocaleDateString("es-DO")}</option>)}
              </select>
            </div>

            {selectedConsultation && (
              <div className="sm:col-span-2 rounded-xl bg-cyan-500/5 border border-cyan-500/20 p-3 text-sm">
                <p className="text-xs text-muted-foreground">Paciente auto-seleccionado</p>
                <p className="font-semibold">{selectedConsultation.patientName}</p>
              </div>
            )}

            {!selectedConsultation && (
              <div className="space-y-2 sm:col-span-2">
                <label className="text-sm font-medium text-foreground">Paciente <span className="text-destructive">*</span></label>
                <select required={!formData.medicalConsultationId} value={formData.patientId} onChange={(e) => setFormData({ ...formData, patientId: e.target.value })}
                  className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring">
                  <option value="">Seleccione un paciente...</option>
                  {patients.map((p) => <option key={p.id} value={p.id}>{p.firstName} {p.lastName}</option>)}
                </select>
              </div>
            )}

            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Tipo de Imagen <span className="text-destructive">*</span></label>
              <select required value={formData.imageType} onChange={(e) => setFormData({ ...formData, imageType: e.target.value })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring">
                <option value="">Seleccione tipo...</option>
                {IMAGE_TYPES.map((t) => <option key={t} value={t}>{t}</option>)}
              </select>
            </div>
            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Zona / Parte del Cuerpo</label>
              <input value={formData.bodyPart} onChange={(e) => setFormData({ ...formData, bodyPart: e.target.value })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring"
                placeholder="Ej. Tórax, Abdomen, Rodilla..." />
            </div>
            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Nombre de Archivo <span className="text-destructive">*</span></label>
              <input required value={formData.fileName} onChange={(e) => setFormData({ ...formData, fileName: e.target.value })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring"
                placeholder="Ej. rx_torax_2024.jpg" />
            </div>
            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Ruta del Archivo <span className="text-destructive">*</span></label>
              <input required value={formData.filePath} onChange={(e) => setFormData({ ...formData, filePath: e.target.value })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring"
                placeholder="Ej. /images/patients/..." />
            </div>
            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Fecha de Toma</label>
              <input type="date" value={formData.takenAt} onChange={(e) => setFormData({ ...formData, takenAt: e.target.value })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring" />
            </div>
            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Tipo de Contenido</label>
              <select value={formData.contentType} onChange={(e) => setFormData({ ...formData, contentType: e.target.value })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring">
                <option value="image/jpeg">JPEG</option>
                <option value="image/png">PNG</option>
                <option value="application/dicom">DICOM</option>
                <option value="application/pdf">PDF</option>
              </select>
            </div>
            <div className="space-y-2 sm:col-span-2">
              <label className="text-sm font-medium text-foreground">Descripción</label>
              <input value={formData.description} onChange={(e) => setFormData({ ...formData, description: e.target.value })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring"
                placeholder="Breve descripción del estudio realizado..." />
            </div>
            <div className="space-y-2 sm:col-span-2">
              <label className="text-sm font-medium text-foreground">Interpretación Clínica</label>
              <textarea value={formData.interpretation} onChange={(e) => setFormData({ ...formData, interpretation: e.target.value })}
                className="w-full min-h-[80px] rounded-xl border border-input bg-background px-4 py-3 text-sm focus:outline-none focus:ring-2 focus:ring-ring resize-none"
                placeholder="Hallazgos e interpretación del radiólogo o médico..." />
            </div>
          </div>
          <div className="flex items-center justify-end gap-3 border-t border-border pt-5 mt-5">
            <button type="button" onClick={() => setIsCreateOpen(false)} disabled={isSubmitLoading}
              className="rounded-xl px-4 py-2.5 text-sm font-medium text-muted-foreground hover:bg-muted transition-colors disabled:opacity-50">
              Cancelar
            </button>
            <button type="submit" disabled={isSubmitLoading}
              className="flex items-center gap-2 rounded-xl bg-cyan-600 px-6 py-2.5 text-sm font-medium text-white hover:bg-cyan-700 transition-colors shadow-sm disabled:opacity-50">
              <Save className="size-4" />
              {isSubmitLoading ? "Guardando..." : "Registrar Imagen"}
            </button>
          </div>
        </form>
      </div>
    </div>
  )
}

// ─── Main ─────────────────────────────────────────────────────────────────────
export function MedicalImagesManagement() {
  const hook = useMedicalImages()
  const { filtered, isLoading, searchQuery, setSearchQuery, isCreateOpen, isDetailsOpen, setIsDetailsOpen, selectedImage, openCreate, openDetails, handleDelete, userRole } = hook

  const canCreate = userRole === UserRole.Administrator || userRole === UserRole.Doctor
  const canDelete = userRole === UserRole.Administrator

  return (
    <div className="space-y-6 animate-in fade-in duration-500">
      <div className="flex flex-col sm:flex-row justify-between gap-4">
        <div>
          <div className="flex items-center gap-3 mb-1">
            <span className="flex size-9 items-center justify-center rounded-xl bg-cyan-500/10 text-cyan-500">
              <ImageIcon className="size-5" />
            </span>
            <h2 className="text-2xl font-bold tracking-tight text-foreground">Imágenes Médicas</h2>
          </div>
          <p className="text-sm text-muted-foreground ml-12">Radiografías, ecografías y estudios de imagen de los pacientes.</p>
        </div>
        {canCreate && (
          <button onClick={openCreate} className="flex items-center gap-2 rounded-xl bg-cyan-600 px-4 py-2 text-sm font-medium text-white hover:bg-cyan-700 transition-colors shadow-sm h-fit">
            <Plus className="size-4" /> Registrar Imagen
          </button>
        )}
      </div>

      <div className="rounded-2xl border border-border bg-card p-4 shadow-sm">
        <div className="relative">
          <Search className="absolute left-3 top-1/2 -translate-y-1/2 size-4 text-muted-foreground" />
          <input type="text" placeholder="Buscar por paciente, tipo o zona..." value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            className="w-full rounded-xl border-none bg-muted/50 py-2.5 pl-10 pr-4 text-sm focus:outline-none focus:ring-2 focus:ring-primary/20" />
        </div>
      </div>

      {isLoading ? (
        <div className="flex h-64 items-center justify-center">
          <div className="size-8 animate-spin rounded-full border-4 border-cyan-500 border-t-transparent" />
        </div>
      ) : filtered.length === 0 ? (
        <div className="flex flex-col items-center justify-center rounded-2xl border border-dashed border-border bg-card py-16 text-center">
          <span className="flex size-14 items-center justify-center rounded-full bg-cyan-500/10 text-cyan-500 mb-3">
            <ImageIcon className="size-7" />
          </span>
          <p className="font-semibold text-foreground">Sin imágenes médicas</p>
          <p className="text-sm text-muted-foreground mt-1">
            {canCreate ? "Registra la primera imagen haciendo clic en el botón de arriba." : "No hay imágenes disponibles."}
          </p>
        </div>
      ) : (
        <div className="grid gap-4 sm:grid-cols-2 lg:grid-cols-3">
          {filtered.map((image) => (
            <div key={image.id} className="rounded-2xl border border-border bg-card p-4 shadow-sm hover:shadow-md transition-shadow">
              <div className="flex items-start justify-between mb-3">
                <div className="flex items-center gap-2">
                  <span className="flex size-9 items-center justify-center rounded-xl bg-cyan-500/10 text-cyan-500">
                    <FileImage className="size-4" />
                  </span>
                  <div>
                    <p className="font-semibold text-sm text-foreground">{image.imageType}</p>
                    {image.bodyPart && <p className="text-xs text-muted-foreground">{image.bodyPart}</p>}
                  </div>
                </div>
              </div>
              <div className="space-y-1.5 text-xs text-muted-foreground mb-4">
                <div className="flex items-center gap-1.5">
                  <User className="size-3" />
                  <span className="text-foreground font-medium">{image.patientName}</span>
                </div>
                {image.takenAt && (
                  <div className="flex items-center gap-1.5">
                    <Calendar className="size-3" />
                    <span>{new Date(image.takenAt).toLocaleDateString("es-DO")}</span>
                  </div>
                )}
                {image.description && <p className="truncate">{image.description}</p>}
              </div>
              <div className="flex items-center justify-between pt-3 border-t border-border">
                <span className="text-[10px] font-mono text-muted-foreground">{image.contentType}</span>
                <div className="flex gap-1">
                  <button onClick={() => openDetails(image)} className="rounded-lg p-1.5 text-muted-foreground hover:bg-muted hover:text-blue-500 transition-colors">
                    <Eye className="size-4" />
                  </button>
                  {canDelete && (
                    <button onClick={() => handleDelete(image.id)} className="rounded-lg p-1.5 text-muted-foreground hover:bg-destructive/10 hover:text-destructive transition-colors">
                      <Trash2 className="size-4" />
                    </button>
                  )}
                </div>
              </div>
            </div>
          ))}
        </div>
      )}

      {isCreateOpen && <CreateImageModal hook={hook} />}
      {isDetailsOpen && selectedImage && (
        <ImageDetailsModal image={selectedImage} onClose={() => setIsDetailsOpen(false)} onDelete={handleDelete} canDelete={canDelete} />
      )}
    </div>
  )
}
