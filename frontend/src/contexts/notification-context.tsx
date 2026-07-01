"use client"

import { createContext, useContext, useState, useEffect, useCallback, ReactNode } from "react"
import { setNotificationHandler } from "@/lib/notify"

import { AppNotification, NotificationContextValue } from "@/types/notification"

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

  const addNotification = useCallback((n: Omit<AppNotification, "id" | "timestamp" | "read">) => {
    const item: AppNotification = {
      ...n,
      id: crypto.randomUUID(),
      timestamp: Date.now(),
      read: false,
    }
    setNotifications((prev) => [item, ...prev].slice(0, MAX_ITEMS))
  }, [])

  useEffect(() => {
    localStorage.setItem(STORAGE_KEY, JSON.stringify(notifications))
  }, [notifications])

  useEffect(() => {
    setNotificationHandler(addNotification)
  }, [addNotification])

  function markAllRead() {
    setNotifications((prev) => prev.map((n) => ({ ...n, read: true })))
  }

  function clearAll() {
    setNotifications([])
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
