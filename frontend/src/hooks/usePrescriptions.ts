"use client"

import { useState, useEffect, useCallback } from "react"
import { prescriptionService } from "@/services/prescriptionService"
import { medicalConsultationService } from "@/services/medicalConsultationService"
import { notify } from "@/lib/notify"
import { Prescription, CreatePrescriptionFormData, UpdatePrescriptionFormData } from "@/types/prescription"
import { MedicalConsultation } from "@/types/medical-consultation"
import { useAuth } from "@/hooks/useAuth"

export const EMPTY_PRESCRIPTION_FORM: CreatePrescriptionFormData = {
  patientId: "",
  doctorId: "",
  medicalConsultationId: "",
  medication: "",
  dosage: "",
  frequency: "",
  duration: "",
  instructions: "",
}

export function usePrescriptions() {
  const { user } = useAuth()

  const [prescriptions, setPrescriptions] = useState<Prescription[]>([])
  const [consultations, setConsultations] = useState<MedicalConsultation[]>([])
  const [isLoading, setIsLoading] = useState(false)
  const [isSubmitLoading, setIsSubmitLoading] = useState(false)
  const [isPdfLoading, setIsPdfLoading] = useState<string | null>(null)
  const [searchQuery, setSearchQuery] = useState("")

  const [isCreateOpen, setIsCreateOpen] = useState(false)
  const [isEditOpen, setIsEditOpen] = useState(false)
  const [isDetailsOpen, setIsDetailsOpen] = useState(false)

  const [selectedPrescription, setSelectedPrescription] = useState<Prescription | null>(null)
  const [formData, setFormData] = useState<CreatePrescriptionFormData>({ ...EMPTY_PRESCRIPTION_FORM })

  const fetchPrescriptions = useCallback(async () => {
    setIsLoading(true)
    try {
      const data = await prescriptionService.getAll()
      setPrescriptions(data)
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Error al cargar las recetas."
      notify.error("Error al cargar", { description: message })
    } finally {
      setIsLoading(false)
    }
  }, [])

  const fetchConsultations = useCallback(async () => {
    try {
      const data = await medicalConsultationService.getAll()
      setConsultations(data)
    } catch (err: unknown) {
      console.error("Error al cargar consultas:", err)
    }
  }, [])

  useEffect(() => {
    fetchPrescriptions()
    fetchConsultations()
  }, [fetchPrescriptions, fetchConsultations])

  const filteredPrescriptions = prescriptions.filter((p) => {
    const query = searchQuery.toLowerCase()
    return (
      p.patientName.toLowerCase().includes(query) ||
      p.doctorName.toLowerCase().includes(query) ||
      p.medication.toLowerCase().includes(query)
    )
  })

  function openCreate() {
    setFormData({
      ...EMPTY_PRESCRIPTION_FORM,
      // Pre-fill doctorId if user is a doctor
      doctorId: user?.id || "",
    })
    setIsCreateOpen(true)
  }

  function openEdit(prescription: Prescription) {
    setSelectedPrescription(prescription)
    setFormData({
      patientId: prescription.patientId,
      doctorId: prescription.doctorId,
      medicalConsultationId: prescription.medicalConsultationId,
      medication: prescription.medication,
      dosage: prescription.dosage,
      frequency: prescription.frequency,
      duration: prescription.duration,
      instructions: prescription.instructions || "",
    })
    setIsEditOpen(true)
  }

  function openDetails(prescription: Prescription) {
    setSelectedPrescription(prescription)
    setIsDetailsOpen(true)
  }

  // When a consultation is selected, auto-fill patientId and doctorId
  function handleConsultationChange(consultationId: string) {
    const consultation = consultations.find((c) => c.id === consultationId)
    if (consultation) {
      setFormData((prev) => ({
        ...prev,
        medicalConsultationId: consultationId,
        patientId: consultation.patientId,
        doctorId: consultation.doctorId,
      }))
    } else {
      setFormData((prev) => ({ ...prev, medicalConsultationId: consultationId }))
    }
  }

  async function handleCreate(e: React.FormEvent) {
    e.preventDefault()
    setIsSubmitLoading(true)
    try {
      await prescriptionService.create(formData)
      notify.success("Receta creada exitosamente")
      setIsCreateOpen(false)
      fetchPrescriptions()
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Error al crear la receta."
      notify.error("Error al registrar", { description: message })
    } finally {
      setIsSubmitLoading(false)
    }
  }

  async function handleEdit(e: React.FormEvent) {
    e.preventDefault()
    if (!selectedPrescription) return
    setIsSubmitLoading(true)
    try {
      const payload: UpdatePrescriptionFormData = {
        doctorId: formData.doctorId,
        medicalConsultationId: formData.medicalConsultationId,
        medication: formData.medication,
        dosage: formData.dosage,
        frequency: formData.frequency,
        duration: formData.duration,
        instructions: formData.instructions,
      }
      await prescriptionService.update(selectedPrescription.id, payload)
      notify.success("Receta actualizada")
      setIsEditOpen(false)
      fetchPrescriptions()
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Error al actualizar la receta."
      notify.error("Error al actualizar", { description: message })
    } finally {
      setIsSubmitLoading(false)
    }
  }

  async function handleDelete(id: string) {
    try {
      await prescriptionService.delete(id)
      notify.success("Receta eliminada")
      setPrescriptions((prev) => prev.filter((p) => p.id !== id))
      if (isDetailsOpen) setIsDetailsOpen(false)
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Error al eliminar la receta."
      notify.error("Error al eliminar", { description: message })
    }
  }

  async function handleDownloadPdf(prescription: Prescription) {
    setIsPdfLoading(prescription.id)
    try {
      await prescriptionService.downloadPdf(prescription.id, prescription.patientName)
      notify.success("PDF descargado")
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Error al descargar el PDF."
      notify.error("Error al descargar", { description: message })
    } finally {
      setIsPdfLoading(null)
    }
  }

  return {
    prescriptions,
    filteredPrescriptions,
    consultations,
    isLoading,
    isSubmitLoading,
    isPdfLoading,
    searchQuery,
    setSearchQuery,
    formData,
    setFormData,
    selectedPrescription,
    isCreateOpen,
    setIsCreateOpen,
    isEditOpen,
    setIsEditOpen,
    isDetailsOpen,
    setIsDetailsOpen,
    openCreate,
    openEdit,
    openDetails,
    handleConsultationChange,
    handleCreate,
    handleEdit,
    handleDelete,
    handleDownloadPdf,
    userRole: user?.role,
  }
}
