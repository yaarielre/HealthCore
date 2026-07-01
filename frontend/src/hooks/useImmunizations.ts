"use client"

import { useState, useEffect, useCallback } from "react"
import { immunizationService } from "@/services/immunizationService"
import { patientService } from "@/services/patientService"
import { notify } from "@/lib/notify"
import { Immunization, CreateImmunizationFormData, UpdateImmunizationFormData } from "@/types/clinical"
import { Patient } from "@/types/patient"
import { useAuth } from "@/hooks/useAuth"

const EMPTY_FORM: CreateImmunizationFormData = {
  patientId: "",
  vaccineName: "",
  doseNumber: 1,
  applicationDate: new Date().toISOString().split("T")[0],
  nextDoseDate: "",
  batchNumber: "",
  administeredBy: "",
  notes: "",
}

export function useImmunizations() {
  const { user } = useAuth()
  const [immunizations, setImmunizations] = useState<Immunization[]>([])
  const [patients, setPatients] = useState<Patient[]>([])
  const [isLoading, setIsLoading] = useState(false)
  const [isSubmitLoading, setIsSubmitLoading] = useState(false)
  const [searchQuery, setSearchQuery] = useState("")
  const [isCreateOpen, setIsCreateOpen] = useState(false)
  const [isEditOpen, setIsEditOpen] = useState(false)
  const [selectedItem, setSelectedItem] = useState<Immunization | null>(null)
  const [formData, setFormData] = useState<CreateImmunizationFormData>({ ...EMPTY_FORM })

  const fetchAll = useCallback(async () => {
    setIsLoading(true)
    try {
      const [imm, pats] = await Promise.all([immunizationService.getAll(), patientService.getAll()])
      setImmunizations(imm)
      setPatients(pats)
    } catch (err: unknown) {
      notify.error("Error", { description: err instanceof Error ? err.message : "Error al cargar." })
    } finally {
      setIsLoading(false)
    }
  }, [])

  useEffect(() => { fetchAll() }, [fetchAll])

  const filtered = immunizations.filter((i) => {
    const q = searchQuery.toLowerCase()
    return i.patientName.toLowerCase().includes(q) || i.vaccineName.toLowerCase().includes(q)
  })

  function openCreate() {
    setFormData({ ...EMPTY_FORM, administeredBy: user?.fullName || "" })
    setIsCreateOpen(true)
  }

  function openEdit(item: Immunization) {
    setSelectedItem(item)
    setFormData({
      patientId: item.patientId,
      vaccineName: item.vaccineName,
      doseNumber: item.doseNumber,
      applicationDate: item.applicationDate.split("T")[0],
      nextDoseDate: item.nextDoseDate ? item.nextDoseDate.split("T")[0] : "",
      batchNumber: item.batchNumber || "",
      administeredBy: item.administeredBy || "",
      notes: item.notes || "",
    })
    setIsEditOpen(true)
  }

  async function handleCreate(e: React.FormEvent) {
    e.preventDefault()
    setIsSubmitLoading(true)
    try {
      await immunizationService.create(formData)
      notify.success("Vacuna registrada exitosamente")
      setIsCreateOpen(false)
      fetchAll()
    } catch (err: unknown) {
      notify.error("Error", { description: err instanceof Error ? err.message : "Error al registrar." })
    } finally {
      setIsSubmitLoading(false)
    }
  }

  async function handleEdit(e: React.FormEvent) {
    e.preventDefault()
    if (!selectedItem) return
    setIsSubmitLoading(true)
    try {
      const payload: UpdateImmunizationFormData = {
        vaccineName: formData.vaccineName,
        doseNumber: formData.doseNumber,
        applicationDate: formData.applicationDate,
        nextDoseDate: formData.nextDoseDate,
        batchNumber: formData.batchNumber,
        administeredBy: formData.administeredBy,
        notes: formData.notes,
      }
      await immunizationService.update(selectedItem.id, payload)
      notify.success("Vacuna actualizada")
      setIsEditOpen(false)
      fetchAll()
    } catch (err: unknown) {
      notify.error("Error", { description: err instanceof Error ? err.message : "Error al actualizar." })
    } finally {
      setIsSubmitLoading(false)
    }
  }

  async function handleDelete(id: string) {
    try {
      await immunizationService.delete(id)
      notify.success("Vacuna eliminada")
      setImmunizations((prev) => prev.filter((i) => i.id !== id))
    } catch (err: unknown) {
      notify.error("Error", { description: err instanceof Error ? err.message : "Error al eliminar." })
    }
  }

  return {
    immunizations, filtered, patients, isLoading, isSubmitLoading,
    searchQuery, setSearchQuery,
    formData, setFormData,
    selectedItem, isCreateOpen, setIsCreateOpen, isEditOpen, setIsEditOpen,
    openCreate, openEdit, handleCreate, handleEdit, handleDelete,
    userRole: user?.role,
  }
}
