import { useState, useEffect } from "react"
import { useAuth } from "@/hooks/useAuth"
import { authService } from "@/services/authService"
import { UserActivityLog } from "@/types/log"

export function useSettings() {
  const { user } = useAuth()
  const [activeTab, setActiveTab] = useState("profile")
  const [logs, setLogs] = useState<UserActivityLog[]>([])
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)

  const [currentPassword, setCurrentPassword] = useState("")
  const [newPassword, setNewPassword] = useState("")
  const [confirmPassword, setConfirmPassword] = useState("")
  const [passwordLoading, setPasswordLoading] = useState(false)
  const [passwordSuccess, setPasswordSuccess] = useState(false)
  const [passwordError, setPasswordError] = useState<string | null>(null)

  const loadLogs = async () => {
    try {
      setLoading(true)
      const data = await authService.getLogs()
      setLogs(data)
      setError(null)
    } catch (err) {
      console.error("Error loading logs:", err)
      setError("No se pudieron cargar los registros de auditoría")
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    if (activeTab === "audit" && user?.role === 1) {
      loadLogs()
    }
  }, [activeTab, user])

  const handlePasswordChange = async (e: React.FormEvent) => {
    e.preventDefault()
    setPasswordError(null)
    setPasswordSuccess(false)

    if (newPassword !== confirmPassword) {
      setPasswordError("Las contraseñas nuevas no coinciden")
      return
    }

    if (newPassword.length < 6) {
      setPasswordError("La contraseña debe tener al menos 6 caracteres")
      return
    }

    try {
      setPasswordLoading(true)
      const users = await authService.getUsers() as { id: string, email: string }[]
      const currentUserRecord = users.find(u => u.email === user?.email)
      
      if (!currentUserRecord) {
        throw new Error("No se pudo identificar el usuario actual en la base de datos")
      }

      await authService.changePassword(currentUserRecord.id, newPassword)
      
      setPasswordSuccess(true)
      setCurrentPassword("")
      setNewPassword("")
      setConfirmPassword("")
    } catch (err: unknown) {
      if (err instanceof Error) {
        setPasswordError(err.message)
      } else {
        setPasswordError("Error al cambiar la contraseña")
      }
    } finally {
      setPasswordLoading(false)
    }
  }

  return {
    user,
    activeTab,
    setActiveTab,
    logs,
    loading,
    error,
    handlePasswordChange,
    currentPassword,
    setCurrentPassword,
    newPassword,
    setNewPassword,
    confirmPassword,
    setConfirmPassword,
    passwordLoading,
    passwordSuccess,
    passwordError
  }
}
