"use client"

import { Plus, RefreshCw, Search } from "lucide-react"

interface StaffToolbarProps {
  searchQuery: string
  isLoading: boolean
  onSearch: (value: string) => void
  onRefresh: () => void
  onOpenCreate: () => void
}

export function StaffToolbar({ searchQuery, isLoading, onSearch, onRefresh, onOpenCreate }: StaffToolbarProps) {
  return (
    <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4">
      <div className="relative flex-1 max-w-md">
        <Search className="absolute left-3 top-1/2 size-4 -translate-y-1/2 text-muted-foreground" />
        <input
          type="text"
          placeholder="Buscar por nombre, correo o cédula..."
          value={searchQuery}
          onChange={(e) => onSearch(e.target.value)}
          className="w-full rounded-xl border border-border bg-card py-2 pl-10 pr-4 text-sm text-foreground placeholder:text-muted-foreground focus:outline-none focus:ring-2 focus:ring-accent/50 focus:border-accent"
        />
      </div>

      <div className="flex gap-2">
        <button
          onClick={onRefresh}
          className="flex items-center justify-center rounded-xl border border-border bg-card p-2 text-foreground hover:bg-accent/10 transition-colors"
        >
          <RefreshCw className={`size-5 ${isLoading ? "animate-spin" : ""}`} />
        </button>

        <button
          onClick={onOpenCreate}
          className="flex items-center gap-2 rounded-xl bg-accent text-white px-4 py-2 text-sm font-semibold hover:bg-accent/90 shadow-md shadow-accent/10 hover:shadow-accent/20 transition-all active:scale-95"
        >
          <Plus className="size-4" /> Registrar Personal
        </button>
      </div>
    </div>
  )
}
