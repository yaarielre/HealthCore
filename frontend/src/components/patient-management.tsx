"use client"

import { usePatients } from "@/hooks/usePatients"
import { PatientToolbar } from "./patients/patient-toolbar"
import { PatientTable } from "./patients/patient-table"
import { PatientFormModal } from "./patients/patient-form-modal"
import { DeletePatientModal } from "./patients/delete-patient-modal"

export function PatientManagement() {
  const {
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
  } = usePatients()

  return (
    <div className="flex flex-col gap-6">
      <PatientToolbar
        searchQuery={searchQuery}
        isLoading={isLoading}
        onSearchChange={setSearchQuery}
        onRefresh={fetchPatients}
        onCreateClick={openCreate}
      />

      <PatientTable
        patients={filteredPatients}
        isLoading={isLoading}
        onEdit={openEdit}
        onDelete={openDelete}
      />

      {isCreateOpen && (
        <PatientFormModal
          mode="create"
          formData={formData}
          isLoading={isSubmitLoading}
          onClose={() => setIsCreateOpen(false)}
          onSubmit={handleCreate}
          onChange={setFormData}
        />
      )}

      {isEditOpen && (
        <PatientFormModal
          mode="edit"
          formData={formData}
          isLoading={isSubmitLoading}
          onClose={() => setIsEditOpen(false)}
          onSubmit={handleEdit}
          onChange={setFormData}
        />
      )}

      {isDeleteOpen && selectedPatient && (
        <DeletePatientModal
          patient={selectedPatient}
          isLoading={isSubmitLoading}
          onClose={() => setIsDeleteOpen(false)}
          onConfirm={handleDelete}
        />
      )}
    </div>
  )
}
