import { useState, useEffect, useCallback } from "react"
import { patientService } from "@/services/patientService"
import { notify } from "@/lib/notify"
import { Patient, PatientFormData } from "@/types/patient"

export const EMPTY_PATIENT_FORM: PatientFormData = {
  firstName: "",
  lastName: "",
  cedula: "",
  birthDate: "",
  phone: "",
  address: "",
}

export function usePatients() {
  const [patients, setPatients] = useState<Patient[]>([])
  const [isLoading, setIsLoading] = useState(false)
  const [isSubmitLoading, setIsSubmitLoading] = useState(false)
  const [searchQuery, setSearchQuery] = useState("")

  const [isCreateOpen, setIsCreateOpen] = useState(false)
  const [isEditOpen, setIsEditOpen] = useState(false)
  const [isDeleteOpen, setIsDeleteOpen] = useState(false)
  const [selectedPatient, setSelectedPatient] = useState<Patient | null>(null)
  const [formData, setFormData] = useState<PatientFormData>({ ...EMPTY_PATIENT_FORM })

  const fetchPatients = useCallback(async () => {
    setIsLoading(true)
    try {
      const data = await patientService.getAll()
      setPatients(data)
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "No se pudo obtener la lista de pacientes."
      notify.error("Error al cargar pacientes", { description: message })
    } finally {
      setIsLoading(false)
    }
  }, [])

  useEffect(() => {
    fetchPatients()
  }, [fetchPatients])

  const filteredPatients = patients.filter((p) => {
    const query = searchQuery.toLowerCase()
    return (
      `${p.firstName} ${p.lastName}`.toLowerCase().includes(query) ||
      p.cedula.toLowerCase().includes(query) ||
      p.phone.toLowerCase().includes(query)
    )
  })

  function openCreate() {
    setFormData({ ...EMPTY_PATIENT_FORM })
    setIsCreateOpen(true)
  }

  function openEdit(patient: Patient) {
    setSelectedPatient(patient)
    setFormData({
      firstName: patient.firstName,
      lastName: patient.lastName,
      cedula: patient.cedula,
      birthDate: patient.birthDate.split("T")[0],
      phone: patient.phone,
      address: patient.address,
    })
    setIsEditOpen(true)
  }

  function openDelete(patient: Patient) {
    setSelectedPatient(patient)
    setIsDeleteOpen(true)
  }

  async function handleCreate(e: React.FormEvent) {
    e.preventDefault()
    setIsSubmitLoading(true)
    try {
      await patientService.create(formData)
      notify.success("Paciente registrado", {
        description: `${formData.firstName} ${formData.lastName} fue registrado exitosamente.`,
      })
      setIsCreateOpen(false)
      fetchPatients()
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Error al registrar el paciente."
      notify.error("Error al registrar", { description: message })
    } finally {
      setIsSubmitLoading(false)
    }
  }

  async function handleEdit(e: React.FormEvent) {
    e.preventDefault()
    if (!selectedPatient) return
    setIsSubmitLoading(true)
    try {
      await patientService.update(selectedPatient.id, {
        firstName: formData.firstName,
        lastName: formData.lastName,
        phone: formData.phone,
        address: formData.address,
      })
      notify.success("Paciente actualizado", {
        description: `Los datos de ${formData.firstName} ${formData.lastName} fueron actualizados.`,
      })
      setIsEditOpen(false)
      fetchPatients()
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Error al actualizar el paciente."
      notify.error("Error al actualizar", { description: message })
    } finally {
      setIsSubmitLoading(false)
    }
  }

  async function handleDelete() {
    if (!selectedPatient) return
    setIsSubmitLoading(true)
    try {
      await patientService.delete(selectedPatient.id)
      notify.success("Paciente eliminado", {
        description: `${selectedPatient.firstName} ${selectedPatient.lastName} fue eliminado del sistema.`,
      })
      setIsDeleteOpen(false)
      setSelectedPatient(null)
      fetchPatients()
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Error al eliminar el paciente."
      notify.error("Error al eliminar", { description: message })
    } finally {
      setIsSubmitLoading(false)
    }
  }

  return {
    patients,
    filteredPatients,
    isLoading,
    isSubmitLoading,
    searchQuery,
    setSearchQuery,
    formData,
    setFormData,
    selectedPatient,
    isCreateOpen,
    setIsCreateOpen,
    isEditOpen,
    setIsEditOpen,
    isDeleteOpen,
    setIsDeleteOpen,
    openCreate,
    openEdit,
    openDelete,
    handleCreate,
    handleEdit,
    handleDelete,
    fetchPatients,
  }
}
