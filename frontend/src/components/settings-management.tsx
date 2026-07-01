"use client"

import { useState } from "react"
import { useSettings } from "@/hooks/useSettings"
import {
  User,
  Lock,
  ShieldAlert,
  Save,
  Activity,
  Calendar as CalendarIcon,
  Search
} from "lucide-react"
import { Input } from "@/components/ui/input"
import { Button } from "@/components/ui/button"
import { getRoleLabel } from "@/lib/utils"

export function SettingsManagement() {
  const {
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
  } = useSettings()

  const [logSearch, setLogSearch] = useState("")

  const filteredLogs = logs.filter((log) => {
    if (!logSearch) return true
    const query = logSearch.toLowerCase()
    return (
      log.userName.toLowerCase().includes(query) ||
      log.module.toLowerCase().includes(query) ||
      log.action.toLowerCase().includes(query)
    )
  })

  return (
    <div className="space-y-6">
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
        <div>
          <h2 className="text-2xl font-bold tracking-tight">Configuración de Cuenta</h2>
          <p className="text-muted-foreground text-sm">Administra tu perfil, seguridad y auditoría del sistema.</p>
        </div>
      </div>

      <div role="tablist" className="flex gap-2 border-b border-border">
        {[
          { id: "profile" as const, label: "Mi Perfil", icon: User },
          { id: "security" as const, label: "Seguridad", icon: Lock },
          ...(user?.role === 1 ? [{ id: "audit" as const, label: "Auditoría", icon: ShieldAlert }] : []),
        ].map(({ id, label, icon: Icon }) => (
          <button
            key={id}
            role="tab"
            aria-selected={activeTab === id}
            onClick={() => setActiveTab(id)}
            className={`flex items-center gap-2 px-4 py-2.5 text-sm font-medium border-b-2 transition-colors ${
              activeTab === id
                ? "border-accent text-accent"
                : "border-transparent text-muted-foreground hover:text-foreground"
            }`}
          >
            <Icon className="size-4" />
            {label}
          </button>
        ))}
      </div>

      <div className="mt-6">
        {activeTab === "profile" && (
          <div className="max-w-2xl rounded-2xl border border-border bg-card p-6 shadow-sm">
            <h3 className="text-lg font-semibold mb-4 flex items-center gap-2">
              <User className="size-5 text-accent" /> Datos Personales
            </h3>
            <div className="space-y-4">
              <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
                <div className="space-y-1.5">
                  <label htmlFor="fullName" className="text-sm font-medium text-foreground">Nombre Completo</label>
                  <Input id="fullName" value={user?.fullName || ""} disabled />
                </div>
                <div className="space-y-1.5">
                  <label htmlFor="email" className="text-sm font-medium text-foreground">Correo Electrónico</label>
                  <Input id="email" type="email" value={user?.email || ""} disabled />
                </div>
              </div>
              <div className="space-y-1.5">
                <label className="text-sm font-medium text-foreground">Nivel de Acceso (Rol)</label>
                <div className="flex items-center gap-2">
                  <span className="inline-flex items-center rounded-full bg-blue-500/10 px-2.5 py-1 text-xs font-semibold text-blue-500">
                    {getRoleLabel(user?.role)}
                  </span>
                </div>
              </div>
            </div>
          </div>
        )}

        {activeTab === "security" && (
          <div className="max-w-2xl rounded-2xl border border-border bg-card p-6 shadow-sm">
            <h3 className="text-lg font-semibold mb-4 flex items-center gap-2">
              <Lock className="size-5 text-accent" /> Cambiar Contraseña
            </h3>

            <form onSubmit={handlePasswordChange} className="space-y-4">
              {passwordSuccess && (
                <div className="rounded-lg bg-green-500/10 p-3 text-sm text-green-500 font-medium">
                  Contraseña actualizada correctamente.
                </div>
              )}
              {passwordError && (
                <div className="rounded-lg bg-destructive/10 p-3 text-sm text-destructive font-medium">
                  {passwordError}
                </div>
              )}

              <div className="space-y-1.5">
                <label htmlFor="currentPassword" className="text-sm font-medium text-foreground">Contraseña Actual</label>
                <Input id="currentPassword" type="password" required value={currentPassword} onChange={(e) => setCurrentPassword(e.target.value)} placeholder="••••••••" />
              </div>
              <div className="space-y-1.5">
                <label htmlFor="newPassword" className="text-sm font-medium text-foreground">Nueva Contraseña</label>
                <Input id="newPassword" type="password" required value={newPassword} onChange={(e) => setNewPassword(e.target.value)} placeholder="Mínimo 6 caracteres" />
              </div>
              <div className="space-y-1.5">
                <label htmlFor="confirmPassword" className="text-sm font-medium text-foreground">Confirmar Nueva Contraseña</label>
                <Input id="confirmPassword" type="password" required value={confirmPassword} onChange={(e) => setConfirmPassword(e.target.value)} placeholder="Repita la nueva contraseña" />
              </div>

              <div className="pt-2">
                <Button type="submit" disabled={passwordLoading}>
                  {passwordLoading ? (
                    <div className="size-4 animate-spin rounded-full border-2 border-white/20 border-t-white" />
                  ) : (
                    <Save className="size-4" />
                  )}
                  Actualizar Contraseña
                </Button>
              </div>
            </form>
          </div>
        )}

        {activeTab === "audit" && user?.role === 1 && (
          <div className="rounded-2xl border border-border bg-card p-6 shadow-sm">
            <div className="flex flex-col sm:flex-row sm:items-center justify-between mb-6 gap-4">
              <h3 className="text-lg font-semibold flex items-center gap-2">
                <Activity className="size-5 text-accent" /> Registros de Actividad (Logs)
              </h3>
              <div className="relative">
                <Search className="absolute left-2.5 top-1/2 size-4 -translate-y-1/2 text-muted-foreground" />
                <input
                  type="search"
                  placeholder="Buscar logs..."
                  value={logSearch}
                  onChange={(e) => setLogSearch(e.target.value)}
                  className="w-full sm:w-64 rounded-lg border border-border bg-background py-1.5 pl-8 pr-4 text-xs focus:outline-none focus:ring-1 focus:ring-accent"
                />
              </div>
            </div>

            {loading ? (
              <div className="flex h-40 items-center justify-center">
                <div className="size-6 animate-spin rounded-full border-2 border-accent/20 border-t-accent" />
              </div>
            ) : error ? (
              <div className="flex h-40 items-center justify-center text-sm text-destructive">
                {error}
              </div>
            ) : (
              <div className="overflow-x-auto">
                <table className="w-full text-left border-collapse">
                  <thead>
                    <tr className="border-b border-border text-xs text-muted-foreground">
                      <th className="py-3 font-medium">Fecha y Hora</th>
                      <th className="py-3 font-medium">Usuario</th>
                      <th className="py-3 font-medium">Módulo</th>
                      <th className="py-3 font-medium">Acción</th>
                      <th className="py-3 font-medium hidden md:table-cell">Detalles</th>
                      <th className="py-3 font-medium hidden lg:table-cell">IP</th>
                    </tr>
                  </thead>
                  <tbody className="divide-y divide-border text-xs">
                    {filteredLogs.length === 0 ? (
                      <tr>
                        <td colSpan={6} className="py-8 text-center text-muted-foreground">
                          {logSearch ? "No se encontraron registros con ese criterio." : "No hay registros de actividad disponibles."}
                        </td>
                      </tr>
                    ) : (
                      filteredLogs.map((log) => (
                        <tr key={log.id} className="hover:bg-accent/5 transition-colors">
                          <td className="py-3">
                            <div className="flex items-center gap-1.5 whitespace-nowrap">
                              <CalendarIcon className="size-3.5 text-muted-foreground" />
                              <span>{new Date(log.createdAt).toLocaleString()}</span>
                            </div>
                          </td>
                          <td className="py-3 font-medium text-foreground">{log.userName}</td>
                          <td className="py-3">
                            <span className="inline-flex rounded-full bg-accent/10 px-2 py-0.5 font-medium text-accent">
                              {log.module}
                            </span>
                          </td>
                          <td className="py-3 text-muted-foreground">{log.action}</td>
                          <td className="py-3 text-muted-foreground hidden md:table-cell max-w-[200px] truncate">
                            {log.details || "-"}
                          </td>
                          <td className="py-3 text-muted-foreground hidden lg:table-cell font-mono">
                            {log.ipAddress || "-"}
                          </td>
                        </tr>
                      ))
                    )}
                  </tbody>
                </table>
              </div>
            )}
          </div>
        )}
      </div>
    </div>
  )
}
