"use client"

import { RefreshCw, Plus, Search } from "lucide-react"
import { AppointmentStatus, APPOINTMENT_STATUS_CONFIG } from "@/types/appointment"

interface AppointmentToolbarProps {
  searchQuery: string
  statusFilter: AppointmentStatus | "all"
  isLoading: boolean
  onSearchChange: (value: string) => void
  onStatusChange: (status: AppointmentStatus | "all") => void
  onRefresh: () => void
  onCreateClick: () => void
}

export function AppointmentToolbar({
  searchQuery,
  statusFilter,
  isLoading,
  onSearchChange,
  onStatusChange,
  onRefresh,
  onCreateClick,
}: AppointmentToolbarProps) {
  return (
    <div className="flex flex-col gap-4">
      <div className="flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
        <div>
          <h2 className="text-xl font-bold text-foreground">Citas Médicas</h2>
          <p className="text-sm text-muted-foreground mt-0.5">Gestión y seguimiento de citas programadas.</p>
        </div>

        <div className="flex items-center gap-2">
          <div className="relative">
            <Search className="absolute left-3 top-1/2 size-4 -translate-y-1/2 text-muted-foreground" />
            <input
              type="text"
              placeholder="ej. Juan Pérez, Dr. García..."
              value={searchQuery}
              onChange={(e) => onSearchChange(e.target.value)}
              className="w-60 rounded-xl border border-border bg-background py-2 pl-9 pr-4 text-sm text-foreground placeholder:text-muted-foreground focus:outline-none focus:ring-2 focus:ring-accent/50"
            />
          </div>

          <button
            onClick={onRefresh}
            disabled={isLoading}
            className="flex items-center gap-2 rounded-xl border border-border bg-card px-3 py-2 text-sm font-medium text-muted-foreground transition-colors hover:bg-accent/5 hover:text-foreground disabled:opacity-50"
          >
            <RefreshCw className={`size-4 ${isLoading ? "animate-spin" : ""}`} />
          </button>

          <button
            onClick={onCreateClick}
            className="flex items-center gap-2 rounded-xl bg-accent px-4 py-2 text-sm font-semibold text-white transition-all hover:brightness-110 active:scale-95"
          >
            <Plus className="size-4" />
            Nueva Cita
          </button>
        </div>
      </div>

      <div className="flex items-center gap-2 flex-wrap">
        <button
          onClick={() => onStatusChange("all")}
          className={`rounded-full px-3 py-1 text-xs font-semibold transition-colors ${statusFilter === "all" ? "bg-accent text-white" : "border border-border text-muted-foreground hover:bg-accent/5"}`}
        >
          Todas
        </button>
        {Object.entries(APPOINTMENT_STATUS_CONFIG).map(([key, config]) => (
          <button
            key={key}
            onClick={() => onStatusChange(Number(key) as AppointmentStatus)}
            className={`rounded-full px-3 py-1 text-xs font-semibold transition-colors ${statusFilter === Number(key) ? config.className + " ring-2 ring-offset-1 ring-accent/30" : "border border-border text-muted-foreground hover:bg-accent/5"}`}
          >
            {config.label}
          </button>
        ))}
      </div>
    </div>
  )
}
