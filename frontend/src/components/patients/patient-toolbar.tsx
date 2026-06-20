"use client"

import { Plus, RefreshCw, Search } from "lucide-react"

interface PatientToolbarProps {
  searchQuery: string
  isLoading: boolean
  onSearchChange: (value: string) => void
  onRefresh: () => void
  onCreateClick: () => void
}

export function PatientToolbar({
  searchQuery,
  isLoading,
  onSearchChange,
  onRefresh,
  onCreateClick,
}: PatientToolbarProps) {
  return (
    <div className="flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
      <div>
        <h2 className="text-xl font-bold text-foreground">Gestión de Pacientes</h2>
        <p className="text-sm text-muted-foreground mt-0.5">Registro y administración de pacientes de la clínica.</p>
      </div>

      <div className="flex items-center gap-2">
        <div className="relative">
          <Search className="absolute left-3 top-1/2 size-4 -translate-y-1/2 text-muted-foreground" />
          <input
            type="text"
            placeholder="ej. María García, 001-000..."
            value={searchQuery}
            onChange={(e) => onSearchChange(e.target.value)}
            className="w-64 rounded-xl border border-border bg-background py-2 pl-9 pr-4 text-sm text-foreground placeholder:text-muted-foreground focus:outline-none focus:ring-2 focus:ring-accent/50"
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
          Nuevo Paciente
        </button>
      </div>
    </div>
  )
}
