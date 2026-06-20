"use client"

import { useAppointments } from "@/hooks/useAppointments"
import { AppointmentToolbar } from "./appointments/appointment-toolbar"
import { AppointmentTable } from "./appointments/appointment-table"
import { AppointmentFormModal } from "./appointments/appointment-form-modal"
import { ChangeStatusModal } from "./appointments/change-status-modal"

export function AppointmentManagement() {
  const {
    patients,
    doctors,
    filteredAppointments,
    isLoading,
    isSubmitLoading,
    searchQuery,
    setSearchQuery,
    statusFilter,
    setStatusFilter,
    formData,
    setFormData,
    selectedAppointment,
    isCreateOpen,
    setIsCreateOpen,
    isEditOpen,
    setIsEditOpen,
    isStatusOpen,
    setIsStatusOpen,
    openCreate,
    openEdit,
    openChangeStatus,
    handleCreate,
    handleEdit,
    handleChangeStatus,
    fetchAppointments,
  } = useAppointments()

  return (
    <div className="flex flex-col gap-6">
      <AppointmentToolbar
        searchQuery={searchQuery}
        statusFilter={statusFilter}
        isLoading={isLoading}
        onSearchChange={setSearchQuery}
        onStatusChange={setStatusFilter}
        onRefresh={fetchAppointments}
        onCreateClick={openCreate}
      />

      <AppointmentTable
        appointments={filteredAppointments}
        isLoading={isLoading}
        onEdit={openEdit}
        onChangeStatus={openChangeStatus}
      />

      {isCreateOpen && (
        <AppointmentFormModal
          mode="create"
          formData={formData}
          patients={patients}
          doctors={doctors}
          isLoading={isSubmitLoading}
          onClose={() => setIsCreateOpen(false)}
          onSubmit={handleCreate}
          onChange={setFormData}
        />
      )}

      {isEditOpen && (
        <AppointmentFormModal
          mode="edit"
          formData={formData}
          patients={patients}
          doctors={doctors}
          isLoading={isSubmitLoading}
          onClose={() => setIsEditOpen(false)}
          onSubmit={handleEdit}
          onChange={setFormData}
        />
      )}

      {isStatusOpen && selectedAppointment && (
        <ChangeStatusModal
          appointment={selectedAppointment}
          isLoading={isSubmitLoading}
          onClose={() => setIsStatusOpen(false)}
          onConfirm={handleChangeStatus}
        />
      )}
    </div>
  )
}
