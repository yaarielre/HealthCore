"use client"

import { Search } from "lucide-react"
import { MenuItem } from "@/types/sidebar"

interface SidebarNavProps {
  collapsed: boolean
  activeTab: string
  setActiveTab: (tab: string) => void
  setMobileOpen: (open: boolean) => void
  visibleMenuItems: MenuItem[]
}

export function SidebarNav({ 
  collapsed, 
  activeTab, 
  setActiveTab, 
  setMobileOpen, 
  visibleMenuItems 
}: SidebarNavProps) {
  return (
    <>
      {!collapsed && (
        <div className="px-4 py-3">
          <div className="relative">
            <Search className="absolute left-3 top-1/2 size-4 -translate-y-1/2 text-muted-foreground" />
            <input
              type="text"
              placeholder="Buscar paciente o cita..."
              className="w-full rounded-lg border border-border bg-background/50 py-1.5 pl-9 pr-4 text-xs text-foreground placeholder:text-muted-foreground focus:outline-none focus:ring-1 focus:ring-accent"
            />
          </div>
        </div>
      )}

      <nav className="flex-1 space-y-1 px-3 py-3 overflow-y-auto overflow-x-hidden">
        {visibleMenuItems.map((item) => {
          const Icon = item.icon
          const isActive = activeTab === item.id
          return (
            <button
              key={item.id}
              onClick={() => {
                setActiveTab(item.id)
                setMobileOpen(false)
              }}
              className={`flex w-full items-center gap-3 rounded-xl px-3 py-2.5 text-sm font-medium transition-all duration-200 group relative
                ${isActive 
                  ? "bg-accent/10 text-accent font-semibold shadow-sm shadow-accent/5" 
                  : "text-muted-foreground hover:bg-accent/5 hover:text-foreground"
                }
              `}
            >
              <Icon className={`size-5 transition-transform duration-200 group-hover:scale-105 ${isActive ? "text-accent" : "text-muted-foreground group-hover:text-foreground"}`} />
              {!collapsed && <span className="truncate">{item.label}</span>}
              
              {isActive && (
                <div className="absolute right-0 top-1/2 h-5 w-1 -translate-y-1/2 rounded-l bg-accent" />
              )}
              
              {collapsed && (
                <span className="absolute left-full ml-4 rounded-md bg-popover px-2 py-1 text-xs text-popover-foreground opacity-0 shadow-md transition-opacity duration-200 group-hover:opacity-100 pointer-events-none whitespace-nowrap z-50 border border-border">
                  {item.label}
                </span>
              )}
            </button>
          )
        })}
      </nav>
    </>
  )
}
