"use client"

import { Search, Plus, Pill } from "lucide-react"
import { usePrescriptions } from "@/hooks/usePrescriptions"
import { PrescriptionsTable } from "./prescriptions-table"
import { PrescriptionForm } from "./prescription-form"
import { PrescriptionDetails } from "./prescription-details"
import { UserRole } from "@/types/auth"

export function PrescriptionsManagement() {
  const {
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
    userRole,
  } = usePrescriptions()

  const canCreate = userRole === UserRole.Administrator || userRole === UserRole.Doctor

  return (
    <div className="space-y-6 animate-in fade-in duration-500">
      {/* Page Header */}
      <div className="flex flex-col sm:flex-row justify-between gap-4">
        <div>
          <div className="flex items-center gap-3 mb-1">
            <span className="flex size-9 items-center justify-center rounded-xl bg-emerald-500/10 text-emerald-500">
              <Pill className="size-5" />
            </span>
            <h2 className="text-2xl font-bold tracking-tight text-foreground">Recetas Médicas</h2>
          </div>
          <p className="text-sm text-muted-foreground ml-12">
            Gestione las prescripciones y descargue las recetas en PDF.
          </p>
        </div>

        {canCreate && (
          <button
            onClick={openCreate}
            className="flex items-center gap-2 rounded-xl bg-emerald-600 px-4 py-2 text-sm font-medium text-white hover:bg-emerald-700 transition-colors shadow-sm h-fit"
          >
            <Plus className="size-4" />
            Nueva Receta
          </button>
        )}
      </div>

      {/* Search */}
      <div className="rounded-2xl border border-border bg-card p-4 shadow-sm">
        <div className="relative">
          <Search className="absolute left-3 top-1/2 -translate-y-1/2 size-4 text-muted-foreground" />
          <input
            type="text"
            placeholder="Buscar por medicamento, paciente o médico..."
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            className="w-full rounded-xl border-none bg-muted/50 py-2.5 pl-10 pr-4 text-sm text-foreground focus:outline-none focus:ring-2 focus:ring-primary/20"
          />
        </div>
      </div>

      {/* Table */}
      {isLoading ? (
        <div className="flex h-64 items-center justify-center">
          <div className="size-8 animate-spin rounded-full border-4 border-emerald-500 border-t-transparent" />
        </div>
      ) : (
        <PrescriptionsTable
          prescriptions={filteredPrescriptions}
          onEdit={openEdit}
          onDetails={openDetails}
          onDelete={handleDelete}
          onDownloadPdf={handleDownloadPdf}
          isPdfLoading={isPdfLoading}
          userRole={userRole}
        />
      )}

      {/* Create Modal */}
      {isCreateOpen && (
        <PrescriptionForm
          title="Nueva Receta Médica"
          formData={formData}
          setFormData={setFormData}
          onConsultationChange={handleConsultationChange}
          onSubmit={handleCreate}
          onClose={() => setIsCreateOpen(false)}
          isLoading={isSubmitLoading}
          consultations={consultations}
        />
      )}

      {/* Edit Modal */}
      {isEditOpen && selectedPrescription && (
        <PrescriptionForm
          title="Editar Receta Médica"
          formData={formData}
          setFormData={setFormData}
          onConsultationChange={handleConsultationChange}
          onSubmit={handleEdit}
          onClose={() => setIsEditOpen(false)}
          isLoading={isSubmitLoading}
          consultations={consultations}
          isEditMode
        />
      )}

      {/* Details Modal */}
      {isDetailsOpen && selectedPrescription && (
        <PrescriptionDetails
          prescription={selectedPrescription}
          onClose={() => setIsDetailsOpen(false)}
          onDownloadPdf={handleDownloadPdf}
          isPdfLoading={isPdfLoading}
        />
      )}
    </div>
  )
}
