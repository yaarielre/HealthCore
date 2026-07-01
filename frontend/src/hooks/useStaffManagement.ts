import { useState, useEffect, useMemo, useCallback } from "react"
import { notify } from "@/lib/notify"
import { authService } from "@/services/authService"
import { UserRole } from "@/types/auth"
import { getRoleLabel, getErrorMessage } from "@/lib/utils"
import { StaffMember, StaffFormData, PasswordFormData } from "@/types/staff"

export const STAFF_ROLES: { value: UserRole; label: string }[] = [
  { value: UserRole.Doctor,       label: "Médico / Doctor" },
  { value: UserRole.Nurse,        label: "Enfermero/a" },
  { value: UserRole.Receptionist, label: "Recepcionista" },
  { value: UserRole.Cashier,      label: "Cajero/a" },
  { value: UserRole.Laboratory,   label: "Laboratorio" },
  { value: UserRole.Pharmacy,     label: "Farmacia" },
]

export const CLINICAL_ROLES = STAFF_ROLES.map(r => r.value)

export const EMPTY_FORM = {
  firstName: "",
  lastName: "",
  idNumber: "",
  email: "",
  password: "",
  phone: "",
  role: UserRole.Doctor as UserRole,
}



export function useStaffManagement() {
  const [users, setUsers] = useState<StaffMember[]>([])
  const [searchQuery, setSearchQuery] = useState("")
  const [isLoading, setIsLoading] = useState(false)
  const [isSubmitLoading, setIsSubmitLoading] = useState(false)

  const [isCreateOpen, setIsCreateOpen] = useState(false)
  const [formData, setFormData] = useState<StaffFormData>({ ...EMPTY_FORM })

  const [isPasswordOpen, setIsPasswordOpen] = useState(false)
  const [passwordData, setPasswordData] = useState<PasswordFormData>({
    userId: "",
    userName: "",
    newPassword: "",
  })

  const fetchStaff = useCallback(async () => {
    setIsLoading(true)
    try {
      const allUsers = await authService.getUsers() as StaffMember[]
      setUsers(allUsers.filter((u) => CLINICAL_ROLES.includes(u.role)))
    } catch (err: unknown) {
      notify.error("Error al cargar personal", { description: getErrorMessage(err, "No se pudo obtener la lista de personal clínico.") })
    } finally {
      setIsLoading(false)
    }
  }, [])

  useEffect(() => {
    fetchStaff()
  }, [fetchStaff])

  const filteredStaff = useMemo(() =>
    users.filter((member: StaffMember) => {
      const query = searchQuery.toLowerCase()
      return (
        `${member.firstName} ${member.lastName}`.toLowerCase().includes(query) ||
        member.email.toLowerCase().includes(query) ||
        member.idNumber.toLowerCase().includes(query)
      )
    }),
  [users, searchQuery])

  function openCreate() {
    setFormData({ ...EMPTY_FORM })
    setIsCreateOpen(true)
  }

  function openChangePassword(member: StaffMember) {
    setPasswordData({
      userId: member.id,
      userName: `${member.firstName} ${member.lastName}`,
      newPassword: "",
    })
    setIsPasswordOpen(true)
  }

  async function handleCreate(e: React.FormEvent) {
    e.preventDefault()
    setIsSubmitLoading(true)
    try {
      await authService.register({
        firstName: formData.firstName,
        lastName: formData.lastName,
        idNumber: formData.idNumber,
        email: formData.email,
        password: formData.password,
        phone: formData.phone,
        role: Number(formData.role) as UserRole,
        doctorId: null,
      })
      notify.success("Personal registrado", {
        description: `${formData.firstName} ${formData.lastName} fue registrado como ${getRoleLabel(Number(formData.role))}.`,
        visibleToRoles: [1, 2],
      })
      setIsCreateOpen(false)
      fetchStaff()
    } catch (err: unknown) {
      notify.error("Error al registrar", { description: getErrorMessage(err, "Ocurrió un error en el registro."), visibleToRoles: [1, 2] })
    } finally {
      setIsSubmitLoading(false)
    }
  }

  async function handleChangePassword(e: React.FormEvent) {
    e.preventDefault()
    setIsSubmitLoading(true)
    try {
      await authService.changePassword(passwordData.userId, passwordData.newPassword)
      notify.success("Contraseña actualizada", {
        description: `Se cambió la contraseña de ${passwordData.userName}.`,
        visibleToRoles: [1, 2],
      })
      setIsPasswordOpen(false)
      setPasswordData({ userId: "", userName: "", newPassword: "" })
    } catch (err: unknown) {
      notify.error("Error al cambiar contraseña", { description: getErrorMessage(err, "No se pudo actualizar la contraseña."), visibleToRoles: [1, 2] })
    } finally {
      setIsSubmitLoading(false)
    }
  }

  async function handleToggleStatus(member: StaffMember) {
    const nextStatus = member.status === 1 ? 2 : 1
    try {
      await authService.changeUserStatus(member.id, nextStatus)
      notify.success(`Personal ${nextStatus === 1 ? "activado" : "desactivado"}`, {
        description: `La cuenta de ${member.firstName} ${member.lastName} fue actualizada.`,
        visibleToRoles: [1, 2],
      })
      fetchStaff()
    } catch (err: unknown) {
      notify.error("Error al cambiar estado", { description: getErrorMessage(err, "No se pudo actualizar el estado de la cuenta."), visibleToRoles: [1, 2] })
    }
  }

  return {
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
  }
}
