"use client"

import { LogOut } from "lucide-react"

interface SidebarFooterProps {
  collapsed: boolean
  user: { fullName?: string; role?: number } | null
  getRoleLabel: (roleNum?: number) => string
  logout: () => void
}

export function SidebarFooter({ collapsed, user, getRoleLabel, logout }: SidebarFooterProps) {
  return (
    <div className={`border-t border-border bg-background/30 transition-all duration-300 ${collapsed ? "p-2" : "p-4"}`}>
      <div className={`flex items-center overflow-hidden ${collapsed ? "justify-center gap-0" : "gap-3"}`}>
        <div className="flex size-10 shrink-0 items-center justify-center rounded-full bg-accent/20 text-accent font-bold">
          {user?.fullName ? user.fullName.charAt(0).toUpperCase() : "U"}
        </div>
        {!collapsed && (
          <div className="flex-1 overflow-hidden">
            <h4 className="truncate text-sm font-semibold text-foreground">
              {user?.fullName || "Usuario HealthCore"}
            </h4>
            <p className="truncate text-xs text-muted-foreground">
              {getRoleLabel(user?.role)}
            </p>
          </div>
        )}
      </div>

      <button
        onClick={logout}
        className={`mt-4 flex w-full items-center rounded-xl py-2 text-sm font-medium text-destructive hover:bg-destructive/10 transition-all duration-200 group relative
          ${collapsed ? "justify-center px-0 gap-0" : "px-3 gap-3"}
        `}
      >
        <LogOut className="size-5 transition-transform group-hover:translate-x-0.5" />
        {!collapsed && <span>Cerrar sesión</span>}
        
        {collapsed && (
          <span className="absolute left-full ml-4 rounded-md bg-destructive text-white px-2 py-1 text-xs opacity-0 shadow-md transition-opacity duration-200 group-hover:opacity-100 pointer-events-none whitespace-nowrap z-50">
            Cerrar sesión
          </span>
        )}
      </button>
    </div>
  )
}
