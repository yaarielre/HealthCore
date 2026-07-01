import { useState, useEffect, useCallback } from "react"
import { medicalConsultationService } from "@/services/medicalConsultationService"
import { patientService } from "@/services/patientService"
import { doctorService } from "@/services/doctorService"
import { appointmentService } from "@/services/appointmentService"
import { notify } from "@/lib/notify"
import { MedicalConsultation, CreateMedicalConsultationFormData } from "@/types/medical-consultation"
import { Patient } from "@/types/patient"
import { Doctor } from "@/types/doctor"
import { Appointment } from "@/types/appointment"
import { useAuth } from "@/hooks/useAuth"
import { UserRole } from "@/types/auth"

export const EMPTY_CONSULTATION_FORM: CreateMedicalConsultationFormData = {
  patientId: "",
  doctorId: "",
  appointmentId: null,
  reasonForVisit: "",
  symptoms: "",
  diagnosis: "",
  treatment: "",
  observations: "",
  internalNotes: "",
  vitalSigns: {
    bloodPressure: "",
    temperature: "",
    weight: "",
    height: "",
    heartRate: "",
    oxygenSaturation: ""
  }
}

export function useMedicalConsultations() {
  const { user } = useAuth()
  
  const [consultations, setConsultations] = useState<MedicalConsultation[]>([])
  const [patients, setPatients] = useState<Patient[]>([])
  const [doctors, setDoctors] = useState<Doctor[]>([])
  const [appointments, setAppointments] = useState<Appointment[]>([])
  
  const [isLoading, setIsLoading] = useState(false)
  const [isSubmitLoading, setIsSubmitLoading] = useState(false)
  const [searchQuery, setSearchQuery] = useState("")

  const [isCreateOpen, setIsCreateOpen] = useState(false)
  const [isEditOpen, setIsEditOpen] = useState(false)
  const [isDeleteOpen, setIsDeleteOpen] = useState(false)
  const [isDetailsOpen, setIsDetailsOpen] = useState(false)
  
  const [selectedConsultation, setSelectedConsultation] = useState<MedicalConsultation | null>(null)
  const [formData, setFormData] = useState<CreateMedicalConsultationFormData>({ ...EMPTY_CONSULTATION_FORM })

  const fetchConsultations = useCallback(async () => {
    setIsLoading(true)
    try {
      const data = await medicalConsultationService.getAll()
      setConsultations(data)
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Error al obtener las consultas médicas."
      notify.error("Error al cargar", { description: message })
    } finally {
      setIsLoading(false)
    }
  }, [])

  const fetchDependencies = useCallback(async () => {
    try {
      const [patientsData, doctorsData, appointmentsData] = await Promise.all([
        patientService.getAll(),
        doctorService.getAll(),
        appointmentService.getAll()
      ])
      setPatients(patientsData)
      setDoctors(doctorsData)
      setAppointments(appointmentsData)
    } catch (err: unknown) {
      console.error(err)
    }
  }, [])

  useEffect(() => {
    fetchConsultations()
    fetchDependencies()
  }, [fetchConsultations, fetchDependencies])

  const filteredConsultations = consultations.filter((c) => {
    const query = searchQuery.toLowerCase()
    return (
      c.patientName.toLowerCase().includes(query) ||
      c.doctorName.toLowerCase().includes(query) ||
      c.reasonForVisit.toLowerCase().includes(query)
    )
  })

  function openCreate() {
    let defaultDoctorId = ""
    if (user?.role === UserRole.Doctor) {
      const currentDoctor = doctors.find(d => d.email === user.email)
      if (currentDoctor) {
        defaultDoctorId = currentDoctor.id
      }
    }
    
    setFormData({ 
      ...EMPTY_CONSULTATION_FORM,
      doctorId: defaultDoctorId
    })
    setIsCreateOpen(true)
  }

  function openEdit(consultation: MedicalConsultation) {
    setSelectedConsultation(consultation)
    const vs = consultation.vitalSigns && consultation.vitalSigns.length > 0 ? consultation.vitalSigns[0] : null
    
    setFormData({
      patientId: consultation.patientId,
      doctorId: consultation.doctorId,
      appointmentId: consultation.appointmentId,
      reasonForVisit: consultation.reasonForVisit,
      symptoms: consultation.symptoms || "",
      diagnosis: consultation.diagnosis || "",
      treatment: consultation.treatment || "",
      observations: consultation.observations || "",
      internalNotes: consultation.internalNotes || "",
      vitalSigns: {
        bloodPressure: vs?.bloodPressure || "",
        temperature: vs?.temperature ?? "",
        weight: vs?.weight ?? "",
        height: vs?.height ?? "",
        heartRate: vs?.heartRate ?? "",
        oxygenSaturation: vs?.oxygenSaturation ?? ""
      }
    })
    setIsEditOpen(true)
  }

  function openDelete(consultation: MedicalConsultation) {
    setSelectedConsultation(consultation)
    setIsDeleteOpen(true)
  }

  function openDetails(consultation: MedicalConsultation) {
    setSelectedConsultation(consultation)
    setIsDetailsOpen(true)
  }

  async function handleCreate(e: React.FormEvent) {
    e.preventDefault()
    setIsSubmitLoading(true)
    
    const cleanedVitalSigns = {
      bloodPressure: formData.vitalSigns.bloodPressure || null,
      temperature: formData.vitalSigns.temperature === "" ? null : formData.vitalSigns.temperature,
      weight: formData.vitalSigns.weight === "" ? null : formData.vitalSigns.weight,
      height: formData.vitalSigns.height === "" ? null : formData.vitalSigns.height,
      heartRate: formData.vitalSigns.heartRate === "" ? null : formData.vitalSigns.heartRate,
      oxygenSaturation: formData.vitalSigns.oxygenSaturation === "" ? null : formData.vitalSigns.oxygenSaturation,
    }
    
    try {
      await medicalConsultationService.create({
        ...formData,
        vitalSigns: cleanedVitalSigns as any
      })
      notify.success("Consulta médica registrada exitosamente")
      setIsCreateOpen(false)
      fetchConsultations()
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Error al registrar la consulta."
      notify.error("Error al registrar", { description: message })
    } finally {
      setIsSubmitLoading(false)
    }
  }

  async function handleEdit(e: React.FormEvent) {
    e.preventDefault()
    if (!selectedConsultation) return
    setIsSubmitLoading(true)
    
    const cleanedVitalSigns = {
      bloodPressure: formData.vitalSigns.bloodPressure || null,
      temperature: formData.vitalSigns.temperature === "" ? null : formData.vitalSigns.temperature,
      weight: formData.vitalSigns.weight === "" ? null : formData.vitalSigns.weight,
      height: formData.vitalSigns.height === "" ? null : formData.vitalSigns.height,
      heartRate: formData.vitalSigns.heartRate === "" ? null : formData.vitalSigns.heartRate,
      oxygenSaturation: formData.vitalSigns.oxygenSaturation === "" ? null : formData.vitalSigns.oxygenSaturation,
    }
    
    try {
      await medicalConsultationService.update(selectedConsultation.id, {
        doctorId: formData.doctorId,
        appointmentId: formData.appointmentId,
        reasonForVisit: formData.reasonForVisit,
        symptoms: formData.symptoms,
        diagnosis: formData.diagnosis,
        treatment: formData.treatment,
        observations: formData.observations,
        internalNotes: formData.internalNotes,
        vitalSigns: cleanedVitalSigns as any
      })
      notify.success("Consulta médica actualizada")
      setIsEditOpen(false)
      fetchConsultations()
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Error al actualizar la consulta."
      notify.error("Error al actualizar", { description: message })
    } finally {
      setIsSubmitLoading(false)
    }
  }

  async function handleDelete() {
    if (!selectedConsultation) return
    setIsSubmitLoading(true)
    try {
      await medicalConsultationService.delete(selectedConsultation.id)
      notify.success("Consulta eliminada")
      setIsDeleteOpen(false)
      setSelectedConsultation(null)
      fetchConsultations()
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Error al eliminar la consulta."
      notify.error("Error al eliminar", { description: message })
    } finally {
      setIsSubmitLoading(false)
    }
  }

  return {
    consultations,
    filteredConsultations,
    patients,
    doctors,
    appointments,
    isLoading,
    isSubmitLoading,
    searchQuery,
    setSearchQuery,
    formData,
    setFormData,
    selectedConsultation,
    isCreateOpen,
    setIsCreateOpen,
    isEditOpen,
    setIsEditOpen,
    isDeleteOpen,
    setIsDeleteOpen,
    isDetailsOpen,
    setIsDetailsOpen,
    openCreate,
    openEdit,
    openDelete,
    openDetails,
    handleCreate,
    handleEdit,
    handleDelete,
    fetchConsultations,
    userRole: user?.role
  }
}
