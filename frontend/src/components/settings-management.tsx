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

  return (
    <div className="space-y-6">
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
        <div>
          <h2 className="text-2xl font-bold tracking-tight">Configuración de Cuenta</h2>
          <p className="text-muted-foreground text-sm">Administra tu perfil, seguridad y auditoría del sistema.</p>
        </div>
      </div>

      <div className="flex gap-2 border-b border-border">
        <button
          onClick={() => setActiveTab("profile")}
          className={`flex items-center gap-2 px-4 py-2.5 text-sm font-medium border-b-2 transition-colors ${
            activeTab === "profile" 
              ? "border-accent text-accent" 
              : "border-transparent text-muted-foreground hover:text-foreground"
          }`}
        >
          <User className="size-4" />
          Mi Perfil
        </button>
        <button
          onClick={() => setActiveTab("security")}
          className={`flex items-center gap-2 px-4 py-2.5 text-sm font-medium border-b-2 transition-colors ${
            activeTab === "security" 
              ? "border-accent text-accent" 
              : "border-transparent text-muted-foreground hover:text-foreground"
          }`}
        >
          <Lock className="size-4" />
          Seguridad
        </button>
        {user?.role === 1 && (
          <button
            onClick={() => setActiveTab("audit")}
            className={`flex items-center gap-2 px-4 py-2.5 text-sm font-medium border-b-2 transition-colors ${
              activeTab === "audit" 
                ? "border-accent text-accent" 
                : "border-transparent text-muted-foreground hover:text-foreground"
            }`}
          >
            <ShieldAlert className="size-4" />
            Auditoría
          </button>
        )}
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
                  <label className="text-sm font-medium text-foreground">Nombre Completo</label>
                  <input
                    type="text"
                    disabled
                    value={user?.fullName || ""}
                    className="w-full rounded-lg border border-border bg-accent/5 px-3 py-2 text-sm text-muted-foreground opacity-70"
                  />
                </div>
                <div className="space-y-1.5">
                  <label className="text-sm font-medium text-foreground">Correo Electrónico</label>
                  <input
                    type="email"
                    disabled
                    value={user?.email || ""}
                    className="w-full rounded-lg border border-border bg-accent/5 px-3 py-2 text-sm text-muted-foreground opacity-70"
                  />
                </div>
              </div>
              <div className="space-y-1.5">
                <label className="text-sm font-medium text-foreground">Nivel de Acceso (Rol)</label>
                <div className="flex items-center gap-2">
                  <span className="inline-flex items-center rounded-full bg-blue-500/10 px-2.5 py-1 text-xs font-semibold text-blue-500">
                    {user?.role === 1 ? "Administrador" : 
                     user?.role === 2 ? "Gerente Clínico" : 
                     user?.role === 3 ? "Médico" : "Personal"}
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
                <label className="text-sm font-medium text-foreground">Contraseña Actual</label>
                <input
                  type="password"
                  required
                  value={currentPassword}
                  onChange={(e) => setCurrentPassword(e.target.value)}
                  className="w-full rounded-lg border border-border bg-background px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-accent/50"
                  placeholder="••••••••"
                />
              </div>
              <div className="space-y-1.5">
                <label className="text-sm font-medium text-foreground">Nueva Contraseña</label>
                <input
                  type="password"
                  required
                  value={newPassword}
                  onChange={(e) => setNewPassword(e.target.value)}
                  className="w-full rounded-lg border border-border bg-background px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-accent/50"
                  placeholder="Mínimo 6 caracteres"
                />
              </div>
              <div className="space-y-1.5">
                <label className="text-sm font-medium text-foreground">Confirmar Nueva Contraseña</label>
                <input
                  type="password"
                  required
                  value={confirmPassword}
                  onChange={(e) => setConfirmPassword(e.target.value)}
                  className="w-full rounded-lg border border-border bg-background px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-accent/50"
                  placeholder="Repita la nueva contraseña"
                />
              </div>
              
              <div className="pt-2">
                <button
                  type="submit"
                  disabled={passwordLoading}
                  className="flex w-full sm:w-auto items-center justify-center gap-2 rounded-lg bg-accent px-4 py-2 text-sm font-semibold text-white transition-all hover:bg-accent/90 disabled:opacity-70"
                >
                  {passwordLoading ? (
                    <div className="size-4 animate-spin rounded-full border-2 border-white/20 border-t-white" />
                  ) : (
                    <Save className="size-4" />
                  )}
                  Actualizar Contraseña
                </button>
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
                    {logs.length === 0 ? (
                      <tr>
                        <td colSpan={6} className="py-8 text-center text-muted-foreground">
                          No hay registros de actividad disponibles.
                        </td>
                      </tr>
                    ) : (
                      logs.map((log) => (
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
