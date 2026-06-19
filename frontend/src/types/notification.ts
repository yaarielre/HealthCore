export interface AppNotification {
  id: string
  title: string
  description?: string
  type: "success" | "error" | "warning" | "info"
  timestamp: number
  read: boolean
  visibleToRoles?: number[]
}

export interface NotificationContextValue {
  notifications: AppNotification[]
  addNotification: (n: Omit<AppNotification, "id" | "timestamp" | "read">) => void
  markAllRead: () => void
  clearAll: () => void
  unreadCount: number
}
