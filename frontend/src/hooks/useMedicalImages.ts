"use client"

import { useState, useEffect, useCallback } from "react"
import { medicalImageService } from "@/services/medicalImageService"
import { medicalConsultationService } from "@/services/medicalConsultationService"
import { patientService } from "@/services/patientService"
import { notify } from "@/lib/notify"
import { MedicalImage, CreateMedicalImageFormData } from "@/types/clinical"
import { Patient } from "@/types/patient"
import { MedicalConsultation } from "@/types/medical-consultation"
import { useAuth } from "@/hooks/useAuth"

const IMAGE_TYPES = ["Radiografía", "Ecografía", "Tomografía", "Resonancia Magnética", "Mamografía", "Endoscopía", "Otro"]

const EMPTY_FORM: CreateMedicalImageFormData = {
  patientId: "",
  medicalConsultationId: "",
  imageType: "",
  bodyPart: "",
  fileName: "",
  filePath: "",
  contentType: "image/jpeg",
  fileSizeBytes: "",
  description: "",
  interpretation: "",
  takenAt: new Date().toISOString().split("T")[0],
}

export function useMedicalImages() {
  const { user } = useAuth()
  const [images, setImages] = useState<MedicalImage[]>([])
  const [patients, setPatients] = useState<Patient[]>([])
  const [consultations, setConsultations] = useState<MedicalConsultation[]>([])
  const [isLoading, setIsLoading] = useState(false)
  const [isSubmitLoading, setIsSubmitLoading] = useState(false)
  const [searchQuery, setSearchQuery] = useState("")
  const [isCreateOpen, setIsCreateOpen] = useState(false)
  const [isDetailsOpen, setIsDetailsOpen] = useState(false)
  const [selectedImage, setSelectedImage] = useState<MedicalImage | null>(null)
  const [formData, setFormData] = useState<CreateMedicalImageFormData>({ ...EMPTY_FORM })

  const fetchAll = useCallback(async () => {
    setIsLoading(true)
    try {
      const [imgs, pats, cons] = await Promise.all([
        medicalImageService.getAll(),
        patientService.getAll(),
        medicalConsultationService.getAll(),
      ])
      setImages(imgs)
      setPatients(pats)
      setConsultations(cons)
    } catch (err: unknown) {
      notify.error("Error", { description: err instanceof Error ? err.message : "Error al cargar." })
    } finally {
      setIsLoading(false)
    }
  }, [])

  useEffect(() => { fetchAll() }, [fetchAll])

  const filtered = images.filter((i) => {
    const q = searchQuery.toLowerCase()
    return (
      i.patientName.toLowerCase().includes(q) ||
      i.imageType.toLowerCase().includes(q) ||
      (i.bodyPart || "").toLowerCase().includes(q)
    )
  })

  function openCreate() {
    setFormData({ ...EMPTY_FORM })
    setIsCreateOpen(true)
  }

  function openDetails(image: MedicalImage) {
    setSelectedImage(image)
    setIsDetailsOpen(true)
  }

  function handleConsultationChange(consultationId: string) {
    const c = consultations.find((c) => c.id === consultationId)
    if (c) {
      setFormData((prev) => ({ ...prev, medicalConsultationId: consultationId, patientId: c.patientId }))
    } else {
      setFormData((prev) => ({ ...prev, medicalConsultationId: consultationId }))
    }
  }

  async function handleCreate(e: React.FormEvent) {
    e.preventDefault()
    setIsSubmitLoading(true)
    try {
      await medicalImageService.create(formData)
      notify.success("Imagen médica registrada")
      setIsCreateOpen(false)
      fetchAll()
    } catch (err: unknown) {
      notify.error("Error", { description: err instanceof Error ? err.message : "Error al registrar." })
    } finally {
      setIsSubmitLoading(false)
    }
  }

  async function handleDelete(id: string) {
    try {
      await medicalImageService.delete(id)
      notify.success("Imagen eliminada")
      setImages((prev) => prev.filter((i) => i.id !== id))
      if (isDetailsOpen) setIsDetailsOpen(false)
    } catch (err: unknown) {
      notify.error("Error", { description: err instanceof Error ? err.message : "Error al eliminar." })
    }
  }

  return {
    images, filtered, patients, consultations, IMAGE_TYPES,
    isLoading, isSubmitLoading, searchQuery, setSearchQuery,
    formData, setFormData, selectedImage,
    isCreateOpen, setIsCreateOpen, isDetailsOpen, setIsDetailsOpen,
    openCreate, openDetails, handleConsultationChange,
    handleCreate, handleDelete,
    userRole: user?.role,
  }
}
