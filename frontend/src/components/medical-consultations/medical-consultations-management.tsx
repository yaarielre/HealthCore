import { Search, Plus } from "lucide-react"
import { useMedicalConsultations } from "@/hooks/useMedicalConsultations"
import { MedicalConsultationsTable } from "./medical-consultations-table"
import { MedicalConsultationForm } from "./medical-consultation-form"
import { MedicalConsultationDetails } from "./medical-consultation-details"
import { MedicalConsultationDeleteModal } from "./medical-consultation-delete-modal"

export function MedicalConsultationsManagement() {
  const {
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
    isEditOpen,
    isDeleteOpen,
    isDetailsOpen,
    setIsCreateOpen,
    setIsEditOpen,
    setIsDeleteOpen,
    setIsDetailsOpen,
    openCreate,
    openEdit,
    openDelete,
    openDetails,
    handleCreate,
    handleEdit,
    handleDelete,
    userRole
  } = useMedicalConsultations()

  if (isCreateOpen || isEditOpen) {
    return (
      <MedicalConsultationForm
        onClose={() => {
          setIsCreateOpen(false)
          setIsEditOpen(false)
        }}
        onSubmit={isEditOpen ? handleEdit : handleCreate}
        formData={formData}
        setFormData={setFormData}
        isLoading={isSubmitLoading}
        isEdit={isEditOpen}
        patients={patients}
        doctors={doctors}
        appointments={appointments}
        userRole={userRole}
      />
    )
  }

  if (isDetailsOpen && selectedConsultation) {
    return (
      <MedicalConsultationDetails
        consultation={selectedConsultation}
        onClose={() => setIsDetailsOpen(false)}
        onEdit={(consultation) => {
          setIsDetailsOpen(false)
          openEdit(consultation)
        }}
        userRole={userRole}
      />
    )
  }

  return (
    <div className="space-y-6 animate-in fade-in duration-500">
      <div className="flex flex-col sm:flex-row justify-between gap-4">
        <div>
          <h2 className="text-2xl font-bold text-foreground">Consultas Médicas</h2>
          <p className="text-sm text-muted-foreground mt-1">Gestión de atenciones, diagnósticos y signos vitales.</p>
        </div>
        <button
          onClick={openCreate}
          className="flex items-center gap-2 rounded-xl bg-accent px-4 py-2 text-sm font-medium text-white hover:bg-accent/90 transition-colors shadow-sm h-fit"
        >
          <Plus className="size-4" />
          Nueva Consulta
        </button>
      </div>

      <div className="flex items-center gap-2 rounded-xl border border-border bg-card px-4 py-2.5 shadow-sm max-w-md">
        <Search className="size-4 text-muted-foreground" />
        <input
          type="text"
          placeholder="Buscar por paciente, médico o motivo..."
          value={searchQuery}
          onChange={(e) => setSearchQuery(e.target.value)}
          className="flex-1 bg-transparent text-sm outline-none placeholder:text-muted-foreground text-foreground"
        />
      </div>

      {isLoading ? (
        <div className="flex h-64 items-center justify-center">
          <div className="size-8 animate-spin rounded-full border-4 border-accent/20 border-t-accent" />
        </div>
      ) : (
        <MedicalConsultationsTable
          consultations={filteredConsultations}
          onEdit={openEdit}
          onDelete={openDelete}
          onDetails={openDetails}
          userRole={userRole}
        />
      )}

      {selectedConsultation && (
        <MedicalConsultationDeleteModal
          isOpen={isDeleteOpen}
          onClose={() => setIsDeleteOpen(false)}
          onConfirm={handleDelete}
          patientName={selectedConsultation.patientName}
          date={selectedConsultation.createdAt}
          isLoading={isSubmitLoading}
        />
      )}
    </div>
  )
}
