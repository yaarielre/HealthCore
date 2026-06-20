import { useState, useEffect, useCallback } from "react"
import { appointmentService } from "@/services/appointmentService"
import { patientService } from "@/services/patientService"
import { notify } from "@/lib/notify"
import { Appointment, AppointmentFormData, AppointmentStatus } from "@/types/appointment"
import { Patient } from "@/types/patient"

export const EMPTY_APPOINTMENT_FORM: AppointmentFormData = {
  patientId: "",
  doctorId: "",
  appointmentDate: "",
  reason: "",
  notes: "",
}

export function useAppointments() {
  const [appointments, setAppointments] = useState<Appointment[]>([])
  const [patients, setPatients] = useState<Patient[]>([])
  const [isLoading, setIsLoading] = useState(false)
  const [isSubmitLoading, setIsSubmitLoading] = useState(false)
  const [searchQuery, setSearchQuery] = useState("")
  const [statusFilter, setStatusFilter] = useState<AppointmentStatus | "all">("all")

  const [isCreateOpen, setIsCreateOpen] = useState(false)
  const [isEditOpen, setIsEditOpen] = useState(false)
  const [isStatusOpen, setIsStatusOpen] = useState(false)
  const [selectedAppointment, setSelectedAppointment] = useState<Appointment | null>(null)
  const [formData, setFormData] = useState<AppointmentFormData>({ ...EMPTY_APPOINTMENT_FORM })

  const fetchAppointments = useCallback(async () => {
    setIsLoading(true)
    try {
      const data = await appointmentService.getAll()
      setAppointments(data)
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "No se pudo cargar la lista de citas."
      notify.error("Error al cargar citas", { description: message })
    } finally {
      setIsLoading(false)
    }
  }, [])

  const fetchPatients = useCallback(async () => {
    try {
      const data = await patientService.getAll()
      setPatients(data)
    } catch {
      // silently fail - patients list is optional for selects
    }
  }, [])

  useEffect(() => {
    fetchAppointments()
    fetchPatients()
  }, [fetchAppointments, fetchPatients])

  const filteredAppointments = appointments.filter((a) => {
    const query = searchQuery.toLowerCase()
    const matchesSearch =
      a.patientName.toLowerCase().includes(query) ||
      a.doctorName.toLowerCase().includes(query) ||
      a.reason.toLowerCase().includes(query)
    const matchesStatus = statusFilter === "all" || a.status === statusFilter
    return matchesSearch && matchesStatus
  })

  function openCreate() {
    setFormData({ ...EMPTY_APPOINTMENT_FORM })
    setIsCreateOpen(true)
  }

  function openEdit(appointment: Appointment) {
    setSelectedAppointment(appointment)
    setFormData({
      patientId: appointment.patientId,
      doctorId: appointment.doctorId,
      appointmentDate: appointment.appointmentDate.slice(0, 16),
      reason: appointment.reason,
      notes: appointment.notes ?? "",
    })
    setIsEditOpen(true)
  }

  function openChangeStatus(appointment: Appointment) {
    setSelectedAppointment(appointment)
    setIsStatusOpen(true)
  }

  async function handleCreate(e: React.FormEvent) {
    e.preventDefault()
    setIsSubmitLoading(true)
    try {
      await appointmentService.create({
        patientId: formData.patientId,
        doctorId: formData.doctorId,
        appointmentDate: formData.appointmentDate,
        reason: formData.reason,
        notes: formData.notes || undefined,
      })
      notify.success("Cita programada", { description: "La cita fue registrada exitosamente." })
      setIsCreateOpen(false)
      fetchAppointments()
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Error al programar la cita."
      notify.error("Error al crear cita", { description: message })
    } finally {
      setIsSubmitLoading(false)
    }
  }

  async function handleEdit(e: React.FormEvent) {
    e.preventDefault()
    if (!selectedAppointment) return
    setIsSubmitLoading(true)
    try {
      await appointmentService.update(selectedAppointment.id, {
        appointmentDate: formData.appointmentDate,
        reason: formData.reason,
        notes: formData.notes || undefined,
      })
      notify.success("Cita actualizada", { description: "Los datos de la cita fueron actualizados." })
      setIsEditOpen(false)
      fetchAppointments()
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Error al actualizar la cita."
      notify.error("Error al actualizar", { description: message })
    } finally {
      setIsSubmitLoading(false)
    }
  }

  async function handleChangeStatus(newStatus: AppointmentStatus) {
    if (!selectedAppointment) return
    setIsSubmitLoading(true)
    try {
      await appointmentService.changeStatus(selectedAppointment.id, newStatus)
      notify.success("Estado actualizado", { description: "El estado de la cita fue cambiado." })
      setIsStatusOpen(false)
      setSelectedAppointment(null)
      fetchAppointments()
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Error al cambiar el estado."
      notify.error("Error al cambiar estado", { description: message })
    } finally {
      setIsSubmitLoading(false)
    }
  }

  return {
    appointments,
    patients,
    filteredAppointments,
    isLoading,
    isSubmitLoading,
    searchQuery,
    setSearchQuery,
    statusFilter,
    setStatusFilter,
    formData,
    setFormData,
    selectedAppointment,
    isCreateOpen,
    setIsCreateOpen,
    isEditOpen,
    setIsEditOpen,
    isStatusOpen,
    setIsStatusOpen,
    openCreate,
    openEdit,
    openChangeStatus,
    handleCreate,
    handleEdit,
    handleChangeStatus,
    fetchAppointments,
  }
}
