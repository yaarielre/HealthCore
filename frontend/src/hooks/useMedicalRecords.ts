import { useState, useEffect, useCallback } from "react"
import { medicalRecordService } from "@/services/medicalRecordService"
import { medicalHistoryItemService } from "@/services/medicalHistoryItemService"
import { patientService } from "@/services/patientService"
import { notify } from "@/lib/notify"
import { MedicalRecord, CreateMedicalRecordFormData, MedicalHistoryItem, CreateMedicalHistoryItemFormData, BloodType, MedicalHistoryCategory } from "@/types/medical-record"
import { Patient } from "@/types/patient"
import { useAuth } from "@/hooks/useAuth"

export const EMPTY_RECORD_FORM: CreateMedicalRecordFormData = {
  patientId: "",
  recordNumber: "",
  bloodType: "",
  allergies: "",
  emergencyContactName: "",
  emergencyContactPhone: "",
  notes: ""
}

export const EMPTY_HISTORY_ITEM_FORM: CreateMedicalHistoryItemFormData = {
  patientId: "",
  category: "",
  description: "",
  details: "",
  recordedDate: new Date().toISOString().split('T')[0],
  severity: "",
  recordedById: ""
}

export function useMedicalRecords() {
  const { user } = useAuth()
  
  const [records, setRecords] = useState<MedicalRecord[]>([])
  const [patients, setPatients] = useState<Patient[]>([])
  const [historyItems, setHistoryItems] = useState<MedicalHistoryItem[]>([])
  
  const [isLoading, setIsLoading] = useState(false)
  const [isSubmitLoading, setIsSubmitLoading] = useState(false)
  const [searchQuery, setSearchQuery] = useState("")

  const [isCreateRecordOpen, setIsCreateRecordOpen] = useState(false)
  const [isEditRecordOpen, setIsEditRecordOpen] = useState(false)
  const [isDetailsOpen, setIsDetailsOpen] = useState(false)
  const [isCreateItemOpen, setIsCreateItemOpen] = useState(false)
  
  const [selectedRecord, setSelectedRecord] = useState<MedicalRecord | null>(null)
  
  const [recordFormData, setRecordFormData] = useState<CreateMedicalRecordFormData>({ ...EMPTY_RECORD_FORM })
  const [itemFormData, setItemFormData] = useState<CreateMedicalHistoryItemFormData>({ ...EMPTY_HISTORY_ITEM_FORM })

  const fetchRecords = useCallback(async () => {
    setIsLoading(true)
    try {
      const data = await medicalRecordService.getAll()
      setRecords(data)
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Error al obtener los expedientes."
      notify.error("Error al cargar", { description: message })
    } finally {
      setIsLoading(false)
    }
  }, [])

  const fetchPatients = useCallback(async () => {
    try {
      const data = await patientService.getAll()
      setPatients(data)
    } catch (err: unknown) {
      console.error(err)
    }
  }, [])

  useEffect(() => {
    fetchRecords()
    fetchPatients()
  }, [fetchRecords, fetchPatients])

  const filteredRecords = records.filter((r) => {
    const query = searchQuery.toLowerCase()
    return (
      r.patientName.toLowerCase().includes(query) ||
      r.recordNumber.toLowerCase().includes(query)
    )
  })

  // --- Record Methods ---

  function openCreateRecord() {
    // Generate a random record number like REC-12345
    const randomNum = Math.floor(10000 + Math.random() * 90000)
    setRecordFormData({ 
      ...EMPTY_RECORD_FORM,
      recordNumber: `REC-${randomNum}`
    })
    setIsCreateRecordOpen(true)
  }

  function openEditRecord(record: MedicalRecord) {
    setSelectedRecord(record)
    setRecordFormData({
      patientId: record.patientId,
      recordNumber: record.recordNumber,
      bloodType: record.bloodType ?? "",
      allergies: record.allergies || "",
      emergencyContactName: record.emergencyContactName || "",
      emergencyContactPhone: record.emergencyContactPhone || "",
      notes: record.notes || ""
    })
    setIsEditRecordOpen(true)
  }

  async function openDetails(record: MedicalRecord) {
    setSelectedRecord(record)
    setIsLoading(true)
    try {
      const items = await medicalHistoryItemService.getByPatientId(record.patientId)
      setHistoryItems(items)
      setIsDetailsOpen(true)
    } catch (err: unknown) {
      notify.error("Error", { description: "No se pudieron cargar los antecedentes." })
    } finally {
      setIsLoading(false)
    }
  }

  async function handleCreateRecord(e: React.FormEvent) {
    e.preventDefault()
    setIsSubmitLoading(true)
    
    try {
      const payload = {
        ...recordFormData,
        bloodType: recordFormData.bloodType === "" ? null : recordFormData.bloodType
      }
      
      await medicalRecordService.create(payload as any)
      notify.success("Expediente creado exitosamente")
      setIsCreateRecordOpen(false)
      fetchRecords()
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Error al crear el expediente."
      notify.error("Error al registrar", { description: message })
    } finally {
      setIsSubmitLoading(false)
    }
  }

  async function handleEditRecord(e: React.FormEvent) {
    e.preventDefault()
    if (!selectedRecord) return
    setIsSubmitLoading(true)
    
    try {
      const payload = {
        recordNumber: recordFormData.recordNumber,
        bloodType: recordFormData.bloodType === "" ? null : recordFormData.bloodType,
        allergies: recordFormData.allergies,
        emergencyContactName: recordFormData.emergencyContactName,
        emergencyContactPhone: recordFormData.emergencyContactPhone,
        notes: recordFormData.notes,
        isActive: true
      }
      
      await medicalRecordService.update(selectedRecord.id, payload as any)
      notify.success("Expediente actualizado")
      setIsEditRecordOpen(false)
      fetchRecords()
      
      // Update selected record if details is open
      if (isDetailsOpen) {
        setSelectedRecord({
          ...selectedRecord,
          bloodType: payload.bloodType as any,
          allergies: payload.allergies,
          emergencyContactName: payload.emergencyContactName,
          emergencyContactPhone: payload.emergencyContactPhone,
          notes: payload.notes
        })
      }
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Error al actualizar."
      notify.error("Error al actualizar", { description: message })
    } finally {
      setIsSubmitLoading(false)
    }
  }

  // --- History Item Methods ---

  function openCreateItem() {
    if (!selectedRecord) return
    
    setItemFormData({ 
      ...EMPTY_HISTORY_ITEM_FORM,
      patientId: selectedRecord.patientId,
      // Default to the current user (in a real app you'd extract the Guid from the token, for now we will send a dummy or null if possible)
      // Since recordedById is required, we use the user id if available, or a generic placeholder.
      // Note: The backend expects a Guid.
      recordedById: user?.id || "00000000-0000-0000-0000-000000000000"
    })
    setIsCreateItemOpen(true)
  }

  async function handleCreateItem(e: React.FormEvent) {
    e.preventDefault()
    setIsSubmitLoading(true)
    
    try {
      const payload = {
        ...itemFormData,
        severity: itemFormData.severity === "" ? null : Number(itemFormData.severity),
        recordedDate: itemFormData.recordedDate || new Date().toISOString()
      }
      
      await medicalHistoryItemService.create(payload as any)
      notify.success("Antecedente registrado")
      setIsCreateItemOpen(false)
      
      // Refresh items list
      if (selectedRecord) {
        const items = await medicalHistoryItemService.getByPatientId(selectedRecord.patientId)
        setHistoryItems(items)
      }
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Error al registrar el antecedente."
      notify.error("Error al registrar", { description: message })
    } finally {
      setIsSubmitLoading(false)
    }
  }

  return {
    records,
    filteredRecords,
    patients,
    historyItems,
    isLoading,
    isSubmitLoading,
    searchQuery,
    setSearchQuery,
    
    recordFormData,
    setRecordFormData,
    itemFormData,
    setItemFormData,
    
    selectedRecord,
    
    isCreateRecordOpen,
    setIsCreateRecordOpen,
    isEditRecordOpen,
    setIsEditRecordOpen,
    isDetailsOpen,
    setIsDetailsOpen,
    isCreateItemOpen,
    setIsCreateItemOpen,
    
    openCreateRecord,
    openEditRecord,
    openDetails,
    openCreateItem,
    
    handleCreateRecord,
    handleEditRecord,
    handleCreateItem,
    
    userRole: user?.role
  }
}
