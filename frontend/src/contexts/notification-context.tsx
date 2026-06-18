"use client"

import { createContext, useContext, useState, useEffect, useCallback, ReactNode } from "react"
import { setNotificationHandler } from "@/lib/notify"

export interface AppNotification {
  id: string
  title: string
  description?: string
  type: "success" | "error" | "warning" | "info"
  timestamp: number
  read: boolean
  visibleToRoles?: number[]
}

interface NotificationContextValue {
  notifications: AppNotification[]
  addNotification: (n: Omit<AppNotification, "id" | "timestamp" | "read">) => void
  markAllRead: () => void
  clearAll: () => void
  unreadCount: number
}

const NotificationContext = createContext<NotificationContextValue | null>(null)

const STORAGE_KEY = "healthcore_notifications"
const MAX_ITEMS = 50

export function NotificationProvider({ children }: { children: ReactNode }) {
  const [notifications, setNotifications] = useState<AppNotification[]>([])

  useEffect(() => {
    try {
      const stored = localStorage.getItem(STORAGE_KEY)
      if (stored) setNotifications(JSON.parse(stored))
    } catch {
      //
    }
  }, [])

  function persist(items: AppNotification[]) {
    setNotifications(items)
    localStorage.setItem(STORAGE_KEY, JSON.stringify(items))
  }

  const addNotification = useCallback((n: Omit<AppNotification, "id" | "timestamp" | "read">) => {
    const item: AppNotification = {
      ...n,
      id: crypto.randomUUID(),
      timestamp: Date.now(),
      read: false,
    }
    setNotifications((prev) => {
      const updated = [item, ...prev].slice(0, MAX_ITEMS)
      localStorage.setItem(STORAGE_KEY, JSON.stringify(updated))
      return updated
    })
  }, [])

  useEffect(() => {
    setNotificationHandler(addNotification)
  }, [addNotification])

  function markAllRead() {
    persist(notifications.map((n) => ({ ...n, read: true })))
  }

  function clearAll() {
    persist([])
  }

  const unreadCount = notifications.filter((n) => !n.read).length

  return (
    <NotificationContext.Provider value={{ notifications, addNotification, markAllRead, clearAll, unreadCount }}>
      {children}
    </NotificationContext.Provider>
  )
}

export function useNotifications() {
  const ctx = useContext(NotificationContext)
  if (!ctx) throw new Error("useNotifications must be used within NotificationProvider")
  return ctx
}
