"use client"

import { useSidebar } from "@/hooks/useSidebar"
import { SidebarMobileToggle } from "./sidebar/sidebar-mobile-toggle"
import { SidebarHeader } from "./sidebar/sidebar-header"
import { SidebarNav } from "./sidebar/sidebar-nav"
import { SidebarFooter } from "./sidebar/sidebar-footer"

interface SidebarProps {
  activeTab: string
  setActiveTab: (tab: string) => void
  collapsed: boolean
  setCollapsed: (collapsed: boolean) => void
}

export function Sidebar({ activeTab, setActiveTab, collapsed, setCollapsed }: SidebarProps) {
  const { 
    user, 
    logout, 
    mobileOpen, 
    setMobileOpen, 
    visibleMenuItems, 
    getRoleLabel 
  } = useSidebar()

  return (
    <>
      <SidebarMobileToggle 
        mobileOpen={mobileOpen} 
        setMobileOpen={setMobileOpen} 
      />

      <aside
        className={`fixed inset-y-0 left-0 z-40 flex flex-col bg-card border-r border-border transition-all duration-300 ease-in-out
          ${mobileOpen ? "translate-x-0" : "-translate-x-full lg:translate-x-0"}
          ${collapsed ? "w-20" : "w-64"}
        `}
      >
        <SidebarHeader 
          collapsed={collapsed} 
          setCollapsed={setCollapsed} 
        />

        <SidebarNav 
          collapsed={collapsed}
          activeTab={activeTab}
          setActiveTab={setActiveTab}
          setMobileOpen={setMobileOpen}
          visibleMenuItems={visibleMenuItems}
        />

        <SidebarFooter 
          collapsed={collapsed}
          user={user}
          getRoleLabel={getRoleLabel}
          logout={logout}
        />
      </aside>
    </>
  )
}
