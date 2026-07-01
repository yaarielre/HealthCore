"use client"

import { useState } from "react"
import { 
  LayoutDashboard, 
  Users, 
  Calendar, 
  UserCog, 
  FileText,
  Stethoscope, 
  Settings,
  FolderHeart,
  Pill,
  Syringe,
  FlaskConical,
  ImageIcon
} from "lucide-react"
import { useAuth } from "@/hooks/useAuth"

import { MenuItem } from "@/types/sidebar"
import { UserRole } from "@/types/auth"
import { getRoleLabel } from "@/lib/utils"

const MENU_ITEMS: MenuItem[] = [
  { id: "dashboard", label: "Dashboard", icon: LayoutDashboard },
  { id: "patients", label: "Pacientes", icon: Users },
  { id: "appointments", label: "Citas Médicas", icon: Calendar },
  { id: "staff", label: "Personal Clínico", icon: UserCog },
  { id: "consultations", label: "Consultas", icon: Stethoscope },
  { id: "records", label: "Historias Clínicas", icon: FolderHeart },
  { id: "prescriptions", label: "Recetas Médicas", icon: Pill },
  { id: "immunizations", label: "Vacunas", icon: Syringe },
  { id: "orders", label: "Órdenes Médicas", icon: FlaskConical },
  { id: "medical-images", label: "Imágenes Médicas", icon: ImageIcon },
  { id: "settings", label: "Configuración", icon: Settings },
]

export function useSidebar() {
  const { user, logout } = useAuth()
  const [mobileOpen, setMobileOpen] = useState(false)

  const visibleMenuItems = MENU_ITEMS.filter(item => {
    const role = user?.role

    switch (item.id) {
      case "dashboard":
        return true
      case "patients":
      case "appointments":
        return role === UserRole.Administrator || role === UserRole.Manager || 
               role === UserRole.Doctor || role === UserRole.Nurse || 
               role === UserRole.Receptionist
      case "records":
      case "consultations":
      case "prescriptions":
      case "immunizations":
      case "orders":
      case "medical-images":
        return role === UserRole.Administrator || role === UserRole.Doctor || 
               role === UserRole.Nurse
      case "staff":
        return role === UserRole.Administrator || role === UserRole.Manager
      case "settings":
        return role === UserRole.Administrator
      default:
        return false
    }
  })

  return {
    user,
    logout,
    mobileOpen,
    setMobileOpen,
    visibleMenuItems,
    getRoleLabel,
  }
}
