"use client"

import { useStaffManagement } from "@/hooks/useStaffManagement"
import { StaffToolbar } from "@/components/staff/staff-toolbar"
import { StaffTable } from "@/components/staff/staff-table"
import { CreateStaffModal } from "@/components/staff/create-staff-modal"
import { ChangePasswordModal } from "@/components/staff/change-password-modal"

export function DoctorManagement() {
  const {
    filteredStaff,
    searchQuery,
    setSearchQuery,
    isLoading,
    isSubmitLoading,
    isCreateOpen,
    setIsCreateOpen,
    formData,
    setFormData,
    isPasswordOpen,
    setIsPasswordOpen,
    passwordData,
    setPasswordData,
    fetchStaff,
    openCreate,
    openChangePassword,
    handleCreate,
    handleChangePassword,
    handleToggleStatus,
  } = useStaffManagement()

  return (
    <div className="space-y-6">
      <StaffToolbar
        searchQuery={searchQuery}
        isLoading={isLoading}
        onSearch={setSearchQuery}
        onRefresh={fetchStaff}
        onOpenCreate={openCreate}
      />

      <div className="rounded-2xl border border-border bg-card shadow-sm overflow-hidden">
        <StaffTable
          staff={filteredStaff}
          isLoading={isLoading}
          searchQuery={searchQuery}
          onChangePassword={openChangePassword}
          onToggleStatus={handleToggleStatus}
        />
      </div>

      {isCreateOpen && (
        <CreateStaffModal
          formData={formData}
          isSubmitLoading={isSubmitLoading}
          onSubmit={handleCreate}
          onClose={() => setIsCreateOpen(false)}
          onChange={setFormData}
        />
      )}

      {isPasswordOpen && (
        <ChangePasswordModal
          passwordData={passwordData}
          isSubmitLoading={isSubmitLoading}
          onSubmit={handleChangePassword}
          onClose={() => setIsPasswordOpen(false)}
          onChange={setPasswordData}
        />
      )}
    </div>
  )
}
