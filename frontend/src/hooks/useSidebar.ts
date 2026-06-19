"use client"

import { useState } from "react"
import { 
  LayoutDashboard, 
  Users, 
  Calendar, 
  UserCog, 
  FileText, 
  Settings 
} from "lucide-react"
import { useAuth } from "@/hooks/useAuth"

import { MenuItem } from "@/types/sidebar"

export function useSidebar() {
  const { user, logout } = useAuth()
  const [mobileOpen, setMobileOpen] = useState(false)

  const menuItems: MenuItem[] = [
    { id: "dashboard", label: "Dashboard", icon: LayoutDashboard },
    { id: "patients", label: "Pacientes", icon: Users },
    { id: "appointments", label: "Citas Médicas", icon: Calendar },
    { id: "staff", label: "Personal Clínico", icon: UserCog },
    { id: "records", label: "Historiales", icon: FileText },
    { id: "settings", label: "Configuración", icon: Settings },
  ]

  const visibleMenuItems = menuItems.filter(item => {
    if (item.id === "staff") {
      return user?.role === 1 || user?.role === 2
    }
    return true
  })

  function getRoleLabel(roleNum?: number) {
    if (!roleNum) return "Personal"
    const roles: Record<number, string> = {
      1: "Administrador",
      2: "Administrador Clínico",
      3: "Médico / Doctor",
      4: "Enfermero/a",
      5: "Recepcionista",
      6: "Cajero/a",
      7: "Laboratorio",
      8: "Farmacia"
    }
    return roles[roleNum] || "Personal"
  }

  return {
    user,
    logout,
    mobileOpen,
    setMobileOpen,
    visibleMenuItems,
    getRoleLabel,
  }
}
