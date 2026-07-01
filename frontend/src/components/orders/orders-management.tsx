"use client"

import { Search, Plus, FlaskConical, Trash2, Eye, X, Save, ChevronDown } from "lucide-react"
import { useOrders } from "@/hooks/useOrders"
import { UserRole } from "@/types/auth"
import { Order, OrderStatus, OrderStatusMap, OrderPriority, OrderPriorityMap } from "@/types/clinical"

const statusColors: Record<OrderStatus, string> = {
  [OrderStatus.Pending]: "bg-amber-500/10 text-amber-600",
  [OrderStatus.InProgress]: "bg-blue-500/10 text-blue-600",
  [OrderStatus.Completed]: "bg-green-500/10 text-green-600",
  [OrderStatus.Cancelled]: "bg-red-500/10 text-red-600",
}
const priorityColors: Record<OrderPriority, string> = {
  [OrderPriority.Normal]: "bg-muted text-muted-foreground",
  [OrderPriority.High]: "bg-orange-500/10 text-orange-600",
  [OrderPriority.Urgent]: "bg-red-500/10 text-red-600",
}

// ─── Order Details Modal ─────────────────────────────────────────────────────
function OrderDetailsModal({ order, onClose, onUpdateStatus, onDelete, canDelete }: {
  order: Order; onClose: () => void
  onUpdateStatus: (order: Order, status: OrderStatus) => void
  onDelete: (id: string) => void; canDelete: boolean
}) {
  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-background/80 backdrop-blur-sm animate-in fade-in duration-200">
      <div className="w-full max-w-lg rounded-2xl border border-border bg-card shadow-xl animate-in zoom-in-95 duration-200">
        <div className="flex items-center justify-between border-b border-border p-6">
          <div className="flex items-center gap-3">
            <span className="flex size-10 items-center justify-center rounded-xl bg-violet-500/10 text-violet-500">
              <FlaskConical className="size-5" />
            </span>
            <div>
              <h2 className="text-lg font-bold text-foreground">Orden #{order.orderNumber}</h2>
              <p className="text-xs text-muted-foreground">{order.orderTypeName}</p>
            </div>
          </div>
          <button onClick={onClose} className="rounded-full p-2 text-muted-foreground hover:bg-muted transition-colors">
            <X className="size-5" />
          </button>
        </div>
        <div className="p-6 space-y-4">
          <div className="grid grid-cols-2 gap-3 text-sm">
            <div><p className="text-xs text-muted-foreground">Paciente</p><p className="font-semibold">{order.patientName}</p></div>
            <div><p className="text-xs text-muted-foreground">Médico</p><p className="font-semibold">{order.doctorName}</p></div>
            <div>
              <p className="text-xs text-muted-foreground">Estado</p>
              <span className={`inline-flex items-center rounded-full px-2.5 py-0.5 text-xs font-medium ${statusColors[order.status]}`}>
                {OrderStatusMap[order.status]}
              </span>
            </div>
            <div>
              <p className="text-xs text-muted-foreground">Prioridad</p>
              <span className={`inline-flex items-center rounded-full px-2.5 py-0.5 text-xs font-medium ${priorityColors[order.priority]}`}>
                {OrderPriorityMap[order.priority]}
              </span>
            </div>
            <div><p className="text-xs text-muted-foreground">Estudios</p><p className="font-semibold">{order.itemCount} ítem(s)</p></div>
            <div><p className="text-xs text-muted-foreground">Fecha</p><p className="font-semibold">{new Date(order.orderedAt).toLocaleDateString("es-DO")}</p></div>
          </div>
          {order.notes && (
            <div className="rounded-xl bg-muted/30 border border-border p-3">
              <p className="text-xs text-muted-foreground mb-1">Notas</p>
              <p className="text-sm">{order.notes}</p>
            </div>
          )}
          {/* Quick status update */}
          <div className="space-y-2">
            <p className="text-xs font-medium text-muted-foreground uppercase tracking-wider">Actualizar Estado</p>
            <div className="flex flex-wrap gap-2">
              {Object.entries(OrderStatusMap).map(([key, label]) => {
                const s = Number(key) as OrderStatus
                return (
                  <button key={key} onClick={() => onUpdateStatus(order, s)}
                    className={`rounded-full px-3 py-1 text-xs font-medium border transition-colors ${order.status === s ? statusColors[s] + " border-current" : "border-border text-muted-foreground hover:bg-muted"}`}>
                    {label}
                  </button>
                )
              })}
            </div>
          </div>
        </div>
        <div className="border-t border-border px-6 py-4 flex justify-between">
          {canDelete ? (
            <button onClick={() => onDelete(order.id)} className="flex items-center gap-1.5 rounded-xl px-3 py-2 text-sm text-destructive hover:bg-destructive/10 transition-colors">
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

// ─── Create Order Modal ───────────────────────────────────────────────────────
function CreateOrderModal({ hook }: { hook: ReturnType<typeof useOrders> }) {
  const { formData, setFormData, consultations, orderTypes, patients, isSubmitLoading, handleCreate, setIsCreateOpen, handleConsultationChange, addItem, removeItem, updateItem } = hook
  const selectedConsultation = consultations.find((c) => c.id === formData.medicalConsultationId)

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-background/80 backdrop-blur-sm animate-in fade-in duration-200">
      <div className="w-full max-w-2xl rounded-2xl border border-border bg-card shadow-xl animate-in zoom-in-95 duration-200 max-h-[90vh] flex flex-col">
        <div className="flex items-center justify-between border-b border-border p-6">
          <div className="flex items-center gap-3">
            <span className="flex size-10 items-center justify-center rounded-xl bg-violet-500/10 text-violet-500">
              <FlaskConical className="size-5" />
            </span>
            <h2 className="text-xl font-bold text-foreground">Nueva Orden Médica</h2>
          </div>
          <button onClick={() => setIsCreateOpen(false)} className="rounded-full p-2 text-muted-foreground hover:bg-muted transition-colors">
            <X className="size-5" />
          </button>
        </div>
        <form onSubmit={handleCreate} className="p-6 overflow-y-auto space-y-5">
          {/* Consultation */}
          <div className="space-y-2">
            <label className="text-sm font-medium text-foreground">Consulta Médica Vinculada</label>
            <select value={formData.medicalConsultationId} onChange={(e) => handleConsultationChange(e.target.value)}
              className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring">
              <option value="">Sin consulta (orden directa)</option>
              {consultations.map((c) => (
                <option key={c.id} value={c.id}>{c.patientName} — {c.doctorName} ({new Date(c.createdAt).toLocaleDateString("es-DO")})</option>
              ))}
            </select>
          </div>

          {/* Auto-filled info */}
          {selectedConsultation && (
            <div className="rounded-xl bg-violet-500/5 border border-violet-500/20 p-3 grid grid-cols-2 gap-2 text-sm">
              <div><p className="text-xs text-muted-foreground">Paciente</p><p className="font-semibold">{selectedConsultation.patientName}</p></div>
              <div><p className="text-xs text-muted-foreground">Médico</p><p className="font-semibold">{selectedConsultation.doctorName}</p></div>
            </div>
          )}

          {/* If no consultation: manual patient selection */}
          {!selectedConsultation && (
            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Paciente <span className="text-destructive">*</span></label>
              <select required={!formData.medicalConsultationId} value={formData.patientId} onChange={(e) => setFormData({ ...formData, patientId: e.target.value })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring">
                <option value="">Seleccione un paciente...</option>
                {patients.map((p) => <option key={p.id} value={p.id}>{p.firstName} {p.lastName}</option>)}
              </select>
            </div>
          )}

          <div className="grid grid-cols-2 gap-4">
            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Tipo de Orden <span className="text-destructive">*</span></label>
              <select required value={formData.orderTypeId} onChange={(e) => setFormData({ ...formData, orderTypeId: e.target.value })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring">
                <option value="">Seleccione tipo...</option>
                {orderTypes.map((t) => <option key={t.id} value={t.id}>{t.name}</option>)}
              </select>
            </div>
            <div className="space-y-2">
              <label className="text-sm font-medium text-foreground">Prioridad <span className="text-destructive">*</span></label>
              <select required value={formData.priority} onChange={(e) => setFormData({ ...formData, priority: Number(e.target.value) as OrderPriority })}
                className="w-full rounded-xl border border-input bg-background px-4 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-ring">
                {Object.entries(OrderPriorityMap).map(([k, v]) => <option key={k} value={k}>{v}</option>)}
              </select>
            </div>
          </div>

          {/* Items */}
          <div className="space-y-3">
            <div className="flex items-center justify-between">
              <label className="text-sm font-medium text-foreground">Estudios / Exámenes <span className="text-destructive">*</span></label>
              <button type="button" onClick={addItem} className="flex items-center gap-1 text-xs font-medium text-violet-600 hover:text-violet-700">
                <Plus className="size-3" /> Agregar ítem
              </button>
            </div>
            {formData.items.map((item, idx) => (
              <div key={idx} className="rounded-xl border border-border bg-muted/20 p-3 space-y-2">
                <div className="flex items-center justify-between">
                  <p className="text-xs font-medium text-muted-foreground">Ítem #{idx + 1}</p>
                  {formData.items.length > 1 && (
                    <button type="button" onClick={() => removeItem(idx)} className="text-xs text-destructive hover:underline">Eliminar</button>
                  )}
                </div>
                <div className="grid grid-cols-2 gap-2">
                  <input required value={item.itemName} onChange={(e) => updateItem(idx, "itemName", e.target.value)}
                    className="col-span-2 rounded-lg border border-input bg-background px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-ring"
                    placeholder="Nombre del examen (Ej. Hemograma completo)" />
                  <input value={item.description} onChange={(e) => updateItem(idx, "description", e.target.value)}
                    className="col-span-2 rounded-lg border border-input bg-background px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-ring"
                    placeholder="Descripción o indicación (opcional)" />
                  <div className="flex items-center gap-2">
                    <label className="text-xs text-muted-foreground whitespace-nowrap">Cant.</label>
                    <input type="number" min={1} value={item.quantity} onChange={(e) => updateItem(idx, "quantity", Number(e.target.value))}
                      className="w-full rounded-lg border border-input bg-background px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-ring" />
                  </div>
                  <div className="flex items-center gap-2">
                    <label className="text-xs text-muted-foreground whitespace-nowrap">Precio</label>
                    <input type="number" min={0} step="0.01" value={item.unitPrice} onChange={(e) => updateItem(idx, "unitPrice", e.target.value)}
                      className="w-full rounded-lg border border-input bg-background px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-ring"
                      placeholder="0.00" />
                  </div>
                </div>
              </div>
            ))}
          </div>

          <div className="space-y-2">
            <label className="text-sm font-medium text-foreground">Notas Adicionales</label>
            <textarea value={formData.notes} onChange={(e) => setFormData({ ...formData, notes: e.target.value })}
              className="w-full min-h-[70px] rounded-xl border border-input bg-background px-4 py-3 text-sm focus:outline-none focus:ring-2 focus:ring-ring resize-none"
              placeholder="Instrucciones especiales para el laboratorio..." />
          </div>

          <div className="flex items-center justify-end gap-3 border-t border-border pt-4">
            <button type="button" onClick={() => setIsCreateOpen(false)} disabled={isSubmitLoading}
              className="rounded-xl px-4 py-2.5 text-sm font-medium text-muted-foreground hover:bg-muted transition-colors disabled:opacity-50">
              Cancelar
            </button>
            <button type="submit" disabled={isSubmitLoading}
              className="flex items-center gap-2 rounded-xl bg-violet-600 px-6 py-2.5 text-sm font-medium text-white hover:bg-violet-700 transition-colors shadow-sm disabled:opacity-50">
              <Save className="size-4" />
              {isSubmitLoading ? "Creando..." : "Crear Orden"}
            </button>
          </div>
        </form>
      </div>
    </div>
  )
}

// ─── Main ─────────────────────────────────────────────────────────────────────
export function OrdersManagement() {
  const hook = useOrders()
  const { filtered, isLoading, searchQuery, setSearchQuery, isCreateOpen, isDetailsOpen, setIsDetailsOpen, selectedOrder, openCreate, openDetails, handleUpdateStatus, handleDelete, userRole } = hook

  const canCreate = userRole === UserRole.Administrator || userRole === UserRole.Doctor
  const canDelete = userRole === UserRole.Administrator

  return (
    <div className="space-y-6 animate-in fade-in duration-500">
      <div className="flex flex-col sm:flex-row justify-between gap-4">
        <div>
          <div className="flex items-center gap-3 mb-1">
            <span className="flex size-9 items-center justify-center rounded-xl bg-violet-500/10 text-violet-500">
              <FlaskConical className="size-5" />
            </span>
            <h2 className="text-2xl font-bold tracking-tight text-foreground">Órdenes Médicas</h2>
          </div>
          <p className="text-sm text-muted-foreground ml-12">Gestione órdenes de laboratorio y estudios médicos.</p>
        </div>
        {canCreate && (
          <button onClick={openCreate} className="flex items-center gap-2 rounded-xl bg-violet-600 px-4 py-2 text-sm font-medium text-white hover:bg-violet-700 transition-colors shadow-sm h-fit">
            <Plus className="size-4" /> Nueva Orden
          </button>
        )}
      </div>

      <div className="rounded-2xl border border-border bg-card p-4 shadow-sm">
        <div className="relative">
          <Search className="absolute left-3 top-1/2 -translate-y-1/2 size-4 text-muted-foreground" />
          <input type="text" placeholder="Buscar por paciente, número de orden o tipo..." value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            className="w-full rounded-xl border-none bg-muted/50 py-2.5 pl-10 pr-4 text-sm focus:outline-none focus:ring-2 focus:ring-primary/20" />
        </div>
      </div>

      {isLoading ? (
        <div className="flex h-64 items-center justify-center">
          <div className="size-8 animate-spin rounded-full border-4 border-violet-500 border-t-transparent" />
        </div>
      ) : filtered.length === 0 ? (
        <div className="flex flex-col items-center justify-center rounded-2xl border border-dashed border-border bg-card py-16 text-center">
          <span className="flex size-14 items-center justify-center rounded-full bg-violet-500/10 text-violet-500 mb-3">
            <FlaskConical className="size-7" />
          </span>
          <p className="font-semibold text-foreground">Sin órdenes médicas</p>
          <p className="text-sm text-muted-foreground mt-1">
            {canCreate ? "Crea la primera orden haciendo clic en el botón de arriba." : "No hay órdenes disponibles."}
          </p>
        </div>
      ) : (
        <div className="rounded-2xl border border-border bg-card shadow-sm overflow-hidden">
          <div className="overflow-x-auto">
            <table className="w-full text-left">
              <thead>
                <tr className="border-b border-border bg-muted/30 text-xs text-muted-foreground">
                  <th className="px-6 py-4 font-medium">N° Orden</th>
                  <th className="px-6 py-4 font-medium">Paciente</th>
                  <th className="px-6 py-4 font-medium">Tipo</th>
                  <th className="px-6 py-4 font-medium">Estado</th>
                  <th className="px-6 py-4 font-medium">Prioridad</th>
                  <th className="px-6 py-4 font-medium">Estudios</th>
                  <th className="px-6 py-4 font-medium">Fecha</th>
                  <th className="px-6 py-4 font-medium text-right">Acciones</th>
                </tr>
              </thead>
              <tbody className="divide-y divide-border text-sm">
                {filtered.map((order) => (
                  <tr key={order.id} className="hover:bg-muted/20 transition-colors">
                    <td className="px-6 py-4 font-mono text-xs text-foreground">{order.orderNumber}</td>
                    <td className="px-6 py-4 font-medium text-foreground">{order.patientName}</td>
                    <td className="px-6 py-4 text-muted-foreground">{order.orderTypeName}</td>
                    <td className="px-6 py-4">
                      <span className={`inline-flex items-center rounded-full px-2.5 py-0.5 text-xs font-medium ${statusColors[order.status]}`}>
                        {OrderStatusMap[order.status]}
                      </span>
                    </td>
                    <td className="px-6 py-4">
                      <span className={`inline-flex items-center rounded-full px-2.5 py-0.5 text-xs font-medium ${priorityColors[order.priority]}`}>
                        {OrderPriorityMap[order.priority]}
                      </span>
                    </td>
                    <td className="px-6 py-4 text-center">
                      <span className="inline-flex items-center rounded-full bg-muted px-2.5 py-0.5 text-xs font-medium">{order.itemCount}</span>
                    </td>
                    <td className="px-6 py-4 text-muted-foreground whitespace-nowrap">
                      {new Date(order.orderedAt).toLocaleDateString("es-DO", { day: "2-digit", month: "short", year: "numeric" })}
                    </td>
                    <td className="px-6 py-4">
                      <div className="flex items-center justify-end gap-1">
                        <button onClick={() => openDetails(order)} className="rounded-lg p-1.5 text-muted-foreground hover:bg-muted hover:text-blue-500 transition-colors">
                          <Eye className="size-4" />
                        </button>
                        {canDelete && (
                          <button onClick={() => handleDelete(order.id)} className="rounded-lg p-1.5 text-muted-foreground hover:bg-destructive/10 hover:text-destructive transition-colors">
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
      )}

      {isCreateOpen && <CreateOrderModal hook={hook} />}
      {isDetailsOpen && selectedOrder && (
        <OrderDetailsModal order={selectedOrder} onClose={() => setIsDetailsOpen(false)}
          onUpdateStatus={handleUpdateStatus} onDelete={handleDelete} canDelete={canDelete} />
      )}
    </div>
  )
}
