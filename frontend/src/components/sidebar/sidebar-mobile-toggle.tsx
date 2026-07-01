"use client"

import { Menu, X } from "lucide-react"

interface SidebarMobileToggleProps {
  mobileOpen: boolean
  setMobileOpen: (open: boolean) => void
}

export function SidebarMobileToggle({ mobileOpen, setMobileOpen }: SidebarMobileToggleProps) {
  return (
    <>
      <button
        onClick={() => setMobileOpen(!mobileOpen)}
        aria-label={mobileOpen ? "Cerrar menú" : "Abrir menú"}
        aria-expanded={mobileOpen}
        className="fixed top-4 left-4 z-50 rounded-lg bg-card p-2 border border-border shadow-md lg:hidden text-foreground hover:bg-accent transition-colors"
      >
        {mobileOpen ? <X className="size-5" /> : <Menu className="size-5" />}
      </button>

      {mobileOpen && (
        <div
          onClick={() => setMobileOpen(false)}
          aria-hidden="true"
          className="fixed inset-0 z-40 bg-background/80 backdrop-blur-sm lg:hidden"
        />
      )}
    </>
  )
}
