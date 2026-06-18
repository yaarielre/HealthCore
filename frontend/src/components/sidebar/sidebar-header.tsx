"use client"

import Image from "next/image"

interface SidebarHeaderProps {
  collapsed: boolean
  setCollapsed: (collapsed: boolean) => void
}

export function SidebarHeader({ collapsed, setCollapsed }: SidebarHeaderProps) {
  return (
    <div className="relative flex h-16 items-center px-4 border-b border-border">
      <div className={`flex items-center gap-3 overflow-hidden ${collapsed ? "w-full justify-center" : ""}`}>
        <div className="flex size-10 shrink-0 items-center justify-center rounded-xl bg-accent/10 p-1.5 text-accent">
          <Image
            src="/healthcore-hc.png"
            alt="HealthCore Logo"
            width={32}
            height={32}
            className="h-auto w-full"
          />
        </div>
        {!collapsed && (
          <span className="text-lg font-bold tracking-tight text-foreground transition-opacity duration-200">
            HealthCore
          </span>
        )}
      </div>

      <button
        onClick={() => setCollapsed(!collapsed)}
        className="hidden absolute -right-3.5 top-1/2 -translate-y-1/2 z-50 size-7 items-center justify-center rounded-full border border-border bg-card text-muted-foreground hover:text-foreground lg:flex shadow-md transition-all duration-200 hover:scale-105 cursor-pointer"
        aria-label={collapsed ? "Expandir menú" : "Colapsar menú"}
      >
        {collapsed ? "→" : "←"}
      </button>
    </div>
  )
}
