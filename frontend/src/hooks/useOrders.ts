"use client"

import { useState, useEffect, useCallback } from "react"
import { orderService, orderTypeService } from "@/services/orderService"
import { medicalConsultationService } from "@/services/medicalConsultationService"
import { patientService } from "@/services/patientService"
import { notify } from "@/lib/notify"
import { Order, OrderType, CreateOrderFormData, OrderStatus, OrderPriority } from "@/types/clinical"
import { Patient } from "@/types/patient"
import { MedicalConsultation } from "@/types/medical-consultation"
import { useAuth } from "@/hooks/useAuth"

const EMPTY_FORM: CreateOrderFormData = {
  patientId: "",
  doctorId: "",
  medicalConsultationId: "",
  orderTypeId: "",
  priority: OrderPriority.Normal,
  notes: "",
  items: [{ itemName: "", description: "", quantity: 1, unitPrice: "" }],
}

export function useOrders() {
  const { user } = useAuth()
  const [orders, setOrders] = useState<Order[]>([])
  const [orderTypes, setOrderTypes] = useState<OrderType[]>([])
  const [patients, setPatients] = useState<Patient[]>([])
  const [consultations, setConsultations] = useState<MedicalConsultation[]>([])
  const [isLoading, setIsLoading] = useState(false)
  const [isSubmitLoading, setIsSubmitLoading] = useState(false)
  const [searchQuery, setSearchQuery] = useState("")
  const [isCreateOpen, setIsCreateOpen] = useState(false)
  const [isDetailsOpen, setIsDetailsOpen] = useState(false)
  const [selectedOrder, setSelectedOrder] = useState<Order | null>(null)
  const [formData, setFormData] = useState<CreateOrderFormData>({ ...EMPTY_FORM })

  const fetchAll = useCallback(async () => {
    setIsLoading(true)
    try {
      const [ords, types, pats, cons] = await Promise.all([
        orderService.getAll(),
        orderTypeService.getAll(),
        patientService.getAll(),
        medicalConsultationService.getAll(),
      ])
      setOrders(ords)
      setOrderTypes(types)
      setPatients(pats)
      setConsultations(cons)
    } catch (err: unknown) {
      notify.error("Error", { description: err instanceof Error ? err.message : "Error al cargar." })
    } finally {
      setIsLoading(false)
    }
  }, [])

  useEffect(() => { fetchAll() }, [fetchAll])

  const filtered = orders.filter((o) => {
    const q = searchQuery.toLowerCase()
    return (
      o.patientName.toLowerCase().includes(q) ||
      o.orderNumber.toLowerCase().includes(q) ||
      o.orderTypeName.toLowerCase().includes(q)
    )
  })

  function openCreate() {
    setFormData({ ...EMPTY_FORM, doctorId: user?.id || "" })
    setIsCreateOpen(true)
  }

  function openDetails(order: Order) {
    setSelectedOrder(order)
    setIsDetailsOpen(true)
  }

  function handleConsultationChange(consultationId: string) {
    const c = consultations.find((c) => c.id === consultationId)
    if (c) {
      setFormData((prev) => ({
        ...prev,
        medicalConsultationId: consultationId,
        patientId: c.patientId,
        doctorId: c.doctorId,
      }))
    } else {
      setFormData((prev) => ({ ...prev, medicalConsultationId: consultationId }))
    }
  }

  function addItem() {
    setFormData((prev) => ({
      ...prev,
      items: [...prev.items, { itemName: "", description: "", quantity: 1, unitPrice: "" }],
    }))
  }

  function removeItem(index: number) {
    setFormData((prev) => ({
      ...prev,
      items: prev.items.filter((_, i) => i !== index),
    }))
  }

  function updateItem(index: number, field: string, value: string | number) {
    setFormData((prev) => ({
      ...prev,
      items: prev.items.map((item, i) => (i === index ? { ...item, [field]: value } : item)),
    }))
  }

  async function handleCreate(e: React.FormEvent) {
    e.preventDefault()
    if (formData.items.length === 0) {
      notify.error("Error", { description: "Debe agregar al menos un estudio a la orden." })
      return
    }
    setIsSubmitLoading(true)
    try {
      await orderService.create(formData)
      notify.success("Orden médica creada exitosamente")
      setIsCreateOpen(false)
      fetchAll()
    } catch (err: unknown) {
      notify.error("Error", { description: err instanceof Error ? err.message : "Error al crear." })
    } finally {
      setIsSubmitLoading(false)
    }
  }

  async function handleUpdateStatus(order: Order, status: OrderStatus) {
    try {
      await orderService.update(order.id, status, order.priority, order.orderTypeId, order.notes || "")
      notify.success("Estado actualizado")
      fetchAll()
    } catch (err: unknown) {
      notify.error("Error", { description: err instanceof Error ? err.message : "Error al actualizar." })
    }
  }

  async function handleDelete(id: string) {
    try {
      await orderService.delete(id)
      notify.success("Orden eliminada")
      setOrders((prev) => prev.filter((o) => o.id !== id))
      if (isDetailsOpen) setIsDetailsOpen(false)
    } catch (err: unknown) {
      notify.error("Error", { description: err instanceof Error ? err.message : "Error al eliminar." })
    }
  }

  return {
    orders, filtered, orderTypes, patients, consultations,
    isLoading, isSubmitLoading, searchQuery, setSearchQuery,
    formData, setFormData, selectedOrder,
    isCreateOpen, setIsCreateOpen, isDetailsOpen, setIsDetailsOpen,
    openCreate, openDetails, handleConsultationChange,
    addItem, removeItem, updateItem,
    handleCreate, handleUpdateStatus, handleDelete,
    userRole: user?.role,
  }
}
