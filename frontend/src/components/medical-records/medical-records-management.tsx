import { Search, Plus } from "lucide-react"
import { useMedicalRecords } from "@/hooks/useMedicalRecords"
import { MedicalRecordsTable } from "./medical-records-table"
import { MedicalRecordForm } from "./medical-record-form"
import { MedicalHistoryItemForm } from "./medical-history-item-form"
import { MedicalRecordDetails } from "./medical-record-details"
import { UserRole } from "@/types/auth"

export function MedicalRecordsManagement() {
  const {
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
    userRole
  } = useMedicalRecords()

  if (isDetailsOpen && selectedRecord) {
    return (
      <>
        <MedicalRecordDetails
          record={selectedRecord}
          historyItems={historyItems}
          onClose={() => setIsDetailsOpen(false)}
          onAddHistoryItem={openCreateItem}
          userRole={userRole}
        />
        
        {isCreateItemOpen && (
          <MedicalHistoryItemForm
            formData={itemFormData}
            setFormData={setItemFormData}
            onSubmit={handleCreateItem}
            onClose={() => setIsCreateItemOpen(false)}
            isLoading={isSubmitLoading}
          />
        )}
      </>
    )
  }

  return (
    <div className="space-y-6 animate-in fade-in duration-500">
      <div className="flex flex-col sm:flex-row justify-between gap-4">
        <div>
          <h2 className="text-2xl font-bold tracking-tight text-foreground">Historias Clínicas</h2>
          <p className="text-sm text-muted-foreground mt-1">
            Gestione los expedientes y antecedentes de sus pacientes.
          </p>
        </div>
        
        {(userRole === UserRole.Administrator || userRole === UserRole.Doctor) && (
          <button
            onClick={openCreateRecord}
            className="flex items-center gap-2 rounded-xl bg-primary px-4 py-2 text-sm font-medium text-primary-foreground hover:bg-primary/90 transition-colors shadow-sm h-fit"
          >
            <Plus className="size-4" />
            Crear Expediente
          </button>
        )}
      </div>

      <div className="rounded-2xl border border-border bg-card p-4 shadow-sm">
        <div className="relative">
          <Search className="absolute left-3 top-1/2 -translate-y-1/2 size-4 text-muted-foreground" />
          <input
            type="text"
            placeholder="Buscar expediente por paciente o número..."
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            className="w-full rounded-xl border-none bg-muted/50 py-2.5 pl-10 pr-4 text-sm text-foreground focus:outline-none focus:ring-2 focus:ring-primary/20"
          />
        </div>
      </div>

      {isLoading && records.length === 0 ? (
        <div className="flex h-64 items-center justify-center">
          <div className="size-8 animate-spin rounded-full border-4 border-primary border-t-transparent" />
        </div>
      ) : (
        <MedicalRecordsTable
          records={filteredRecords}
          onEdit={openEditRecord}
          onDetails={openDetails}
          userRole={userRole}
        />
      )}

      {isCreateRecordOpen && (
        <MedicalRecordForm
          title="Crear Expediente Base"
          formData={recordFormData}
          setFormData={setRecordFormData}
          onSubmit={handleCreateRecord}
          onClose={() => setIsCreateRecordOpen(false)}
          isLoading={isSubmitLoading}
          patients={patients.filter(p => !records.some(r => r.patientId === p.id))}
        />
      )}

      {isEditRecordOpen && (
        <MedicalRecordForm
          title="Editar Expediente Base"
          formData={recordFormData}
          setFormData={setRecordFormData}
          onSubmit={handleEditRecord}
          onClose={() => setIsEditRecordOpen(false)}
          isLoading={isSubmitLoading}
          patients={patients}
          isEditMode
        />
      )}
    </div>
  )
}
