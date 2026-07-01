import { apiRequest } from "./api"
import { Order, OrderType, CreateOrderFormData, OrderStatus, OrderPriority } from "@/types/clinical"

export const orderTypeService = {
  getAll: () => apiRequest<OrderType[]>("api/OrderTypes"),
}

export const orderService = {
  getAll: () => apiRequest<Order[]>("api/Orders"),
  getById: (id: string) => apiRequest<Order>(`api/Orders/${id}`),
  getByPatientId: (patientId: string) => apiRequest<Order[]>(`api/Orders/patient/${patientId}`),

  create: (data: CreateOrderFormData) =>
    apiRequest<Order>("api/Orders", {
      method: "POST",
      body: {
        PatientId: data.patientId,
        DoctorId: data.doctorId,
        MedicalConsultationId: data.medicalConsultationId || null,
        OrderTypeId: data.orderTypeId,
        Priority: Number(data.priority),
        Notes: data.notes || null,
        Items: data.items.map((item) => ({
          ItemName: item.itemName,
          Description: item.description || null,
          Quantity: Number(item.quantity),
          UnitPrice: item.unitPrice === "" ? null : Number(item.unitPrice),
        })),
      },
    }),

  update: (id: string, status: OrderStatus, priority: OrderPriority, orderTypeId: string, notes: string) =>
    apiRequest<Order>(`api/Orders/${id}`, {
      method: "PUT",
      body: {
        OrderTypeId: orderTypeId,
        Priority: priority,
        Notes: notes || null,
        Status: status,
      },
    }),

  delete: (id: string) =>
    apiRequest<void>(`api/Orders/${id}`, { method: "DELETE" }),
}
