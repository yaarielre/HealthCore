"use client"

import { Briefcase, FileText, Key, Loader2, Mail, Phone, User, UserCheck, UserX } from "lucide-react"
import { getRoleLabel } from "@/hooks/useStaffManagement"
import { StaffMember } from "@/types/staff"

interface StaffTableProps {
  staff: StaffMember[]
  isLoading: boolean
  searchQuery: string
  onChangePassword: (member: StaffMember) => void
  onToggleStatus: (member: StaffMember) => void
}

function LoadingState() {
  return (
    <div className="flex h-64 flex-col items-center justify-center">
      <Loader2 className="size-8 text-accent animate-spin" />
      <p className="mt-2 text-sm text-muted-foreground">Cargando personal...</p>
    </div>
  )
}

function EmptyState({ searchQuery }: { searchQuery: string }) {
  return (
    <div className="flex h-64 flex-col items-center justify-center p-6 text-center">
      <div className="flex size-12 items-center justify-center rounded-full bg-accent/10 text-accent">
        <User className="size-6" />
      </div>
      <h3 className="mt-4 font-semibold text-foreground text-sm">No se encontró personal</h3>
      <p className="mt-1 text-xs text-muted-foreground max-w-xs">
        {searchQuery
          ? "Intenta con otro término de búsqueda."
          : "Comienza registrando personal clínico en el sistema."}
      </p>
    </div>
  )
}

function StaffRow({ member, onChangePassword, onToggleStatus }: {
  member: StaffMember
  onChangePassword: (member: StaffMember) => void
  onToggleStatus: (member: StaffMember) => void
}) {
  const isActive = member.status === 1

  return (
    <tr className="hover:bg-accent/5 transition-colors">
      <td className="p-4">
        <div className="flex items-center gap-3">
          <div className="flex size-10 shrink-0 items-center justify-center rounded-full bg-accent/15 text-accent font-bold">
            {member.firstName.charAt(0).toUpperCase()}
          </div>
          <p className="font-semibold text-foreground">{member.firstName} {member.lastName}</p>
        </div>
      </td>

      <td className="p-4">
        <span className="inline-flex items-center gap-1.5 rounded-full bg-accent/10 px-2.5 py-0.5 text-xs font-medium text-accent">
          <Briefcase className="size-3" />
          {getRoleLabel(member.role)}
        </span>
      </td>

      <td className="p-4 text-muted-foreground">
        <div className="flex items-center gap-1.5 text-xs">
          <FileText className="size-3.5" />
          <span>{member.idNumber}</span>
        </div>
      </td>

      <td className="p-4 text-muted-foreground space-y-1">
        <div className="flex items-center gap-1.5 text-xs">
          <Mail className="size-3.5" />
          <span>{member.email}</span>
        </div>
        <div className="flex items-center gap-1.5 text-xs">
          <Phone className="size-3.5" />
          <span>{member.phone || "Sin teléfono"}</span>
        </div>
      </td>

      <td className="p-4">
        <span className={`inline-flex items-center rounded-full px-2.5 py-0.5 text-xs font-medium ${
          isActive ? "bg-green-500/10 text-green-500" : "bg-red-500/10 text-red-500"
        }`}>
          {isActive ? "Activo" : "Inactivo"}
        </span>
      </td>

      <td className="p-4 text-right">
        <div className="flex items-center justify-end gap-2">
          <button
            onClick={() => onChangePassword(member)}
            className="flex size-8 items-center justify-center rounded-lg border border-border bg-card text-muted-foreground hover:text-foreground hover:bg-accent/10 transition-colors"
            title="Cambiar Contraseña"
          >
            <Key className="size-4" />
          </button>

          <button
            onClick={() => onToggleStatus(member)}
            className={`flex size-8 items-center justify-center rounded-lg border border-border bg-card transition-colors ${
              isActive ? "text-red-500 hover:bg-red-500/10" : "text-green-500 hover:bg-green-500/10"
            }`}
            title={isActive ? "Desactivar" : "Activar"}
          >
            {isActive ? <UserX className="size-4" /> : <UserCheck className="size-4" />}
          </button>
        </div>
      </td>
    </tr>
  )
}

export function StaffTable({ staff, isLoading, searchQuery, onChangePassword, onToggleStatus }: StaffTableProps) {
  if (isLoading && staff.length === 0) return <LoadingState />
  if (staff.length === 0) return <EmptyState searchQuery={searchQuery} />

  return (
    <div className="overflow-x-auto">
      <table className="w-full text-left border-collapse">
        <thead>
          <tr className="border-b border-border bg-background/50 text-xs text-muted-foreground font-medium">
            <th className="p-4">Personal</th>
            <th className="p-4">Cargo</th>
            <th className="p-4">Cédula</th>
            <th className="p-4">Contacto</th>
            <th className="p-4">Estado</th>
            <th className="p-4 text-right">Acciones</th>
          </tr>
        </thead>
        <tbody className="divide-y divide-border text-sm">
          {staff.map((member) => (
            <StaffRow
              key={member.id}
              member={member}
              onChangePassword={onChangePassword}
              onToggleStatus={onToggleStatus}
            />
          ))}
        </tbody>
      </table>
    </div>
  )
}
