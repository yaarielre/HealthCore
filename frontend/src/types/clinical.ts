// ─── Immunizations ───────────────────────────────────────────────────────────
export interface Immunization {
  id: string
  patientId: string
  patientName: string
  vaccineName: string
  doseNumber: number
  applicationDate: string
  nextDoseDate?: string | null
  batchNumber?: string | null
  administeredBy?: string | null
  notes?: string | null
  createdAt: string
  updatedAt?: string | null
}

export interface CreateImmunizationFormData {
  patientId: string
  vaccineName: string
  doseNumber: number | ""
  applicationDate: string
  nextDoseDate: string
  batchNumber: string
  administeredBy: string
  notes: string
}

export interface UpdateImmunizationFormData {
  vaccineName: string
  doseNumber: number | ""
  applicationDate: string
  nextDoseDate: string
  batchNumber: string
  administeredBy: string
  notes: string
}

// ─── Orders ──────────────────────────────────────────────────────────────────
export enum OrderStatus {
  Pending = 1,
  InProgress = 2,
  Completed = 3,
  Cancelled = 4,
}

export enum OrderPriority {
  Normal = 1,
  Urgent = 2,
  High = 3,
}

export const OrderStatusMap: Record<OrderStatus, string> = {
  [OrderStatus.Pending]: "Pendiente",
  [OrderStatus.InProgress]: "En Progreso",
  [OrderStatus.Completed]: "Completada",
  [OrderStatus.Cancelled]: "Cancelada",
}

export const OrderPriorityMap: Record<OrderPriority, string> = {
  [OrderPriority.Normal]: "Normal",
  [OrderPriority.Urgent]: "Urgente",
  [OrderPriority.High]: "Alta",
}

export interface OrderItem {
  id: string
  orderId: string
  orderNumber: string
  itemName: string
  description?: string | null
  quantity: number
  unitPrice?: number | null
  totalPrice?: number | null
  results?: string | null
  resultUrl?: string | null
  resultedAt?: string | null
  resultedBy?: string | null
  isCompleted: boolean
  createdAt: string
}

export interface Order {
  id: string
  orderNumber: string
  patientId: string
  patientName: string
  doctorId: string
  doctorName: string
  medicalConsultationId?: string | null
  orderTypeId: string
  orderTypeName: string
  status: OrderStatus
  priority: OrderPriority
  notes?: string | null
  itemCount: number
  orderedAt: string
  completedAt?: string | null
  cancelledAt?: string | null
  createdAt: string
}

export interface OrderType {
  id: string
  name: string
  description?: string | null
  isActive: boolean
  createdAt: string
}

export interface CreateOrderItemFormData {
  itemName: string
  description: string
  quantity: number | ""
  unitPrice: number | ""
}

export interface CreateOrderFormData {
  patientId: string
  doctorId: string
  medicalConsultationId: string
  orderTypeId: string
  priority: OrderPriority | ""
  notes: string
  items: CreateOrderItemFormData[]
}

// ─── Medical Images ───────────────────────────────────────────────────────────
export interface MedicalImage {
  id: string
  patientId: string
  patientName: string
  medicalConsultationId?: string | null
  orderItemId?: string | null
  imageType: string
  bodyPart?: string | null
  fileName: string
  filePath: string
  contentType: string
  fileSizeBytes: number
  description?: string | null
  interpretation?: string | null
  interpretedById?: string | null
  interpretedByName?: string | null
  takenAt?: string | null
  createdAt: string
}

export interface CreateMedicalImageFormData {
  patientId: string
  medicalConsultationId: string
  imageType: string
  bodyPart: string
  fileName: string
  filePath: string
  contentType: string
  fileSizeBytes: number | ""
  description: string
  interpretation: string
  takenAt: string
}
