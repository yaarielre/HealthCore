"use client"

import { useEffect, useRef, useState } from "react"
import { Bell, CheckCheck, Trash2, CheckCircle2, XCircle, AlertTriangle, Info } from "lucide-react"
import { useNotifications } from "@/contexts/notification-context"
import { AppNotification } from "@/types/notification"
import { useAuth } from "@/hooks/useAuth"

function relativeTime(timestamp: number): string {
  const diff = Date.now() - timestamp
  const minutes = Math.floor(diff / 60_000)
  const hours = Math.floor(diff / 3_600_000)
  const days = Math.floor(diff / 86_400_000)
  if (minutes < 1) return "Ahora mismo"
  if (minutes < 60) return `Hace ${minutes} min`
  if (hours < 24) return `Hace ${hours}h`
  return `Hace ${days}d`
}

const typeConfig: Record<AppNotification["type"], { icon: React.ElementType; className: string; dot: string }> = {
  success: { icon: CheckCircle2, className: "text-green-500 bg-green-500/10", dot: "bg-green-500" },
  error:   { icon: XCircle,       className: "text-red-500 bg-red-500/10",     dot: "bg-red-500"   },
  warning: { icon: AlertTriangle, className: "text-amber-500 bg-amber-500/10", dot: "bg-amber-500" },
  info:    { icon: Info,          className: "text-blue-500 bg-blue-500/10",   dot: "bg-blue-500"  },
}

function NotificationItem({ notification }: { notification: AppNotification }) {
  const config = typeConfig[notification.type]
  const Icon = config.icon
  return (
    <div className={`flex gap-3 p-3 rounded-xl transition-colors ${notification.read ? "opacity-60" : "bg-accent/5"}`}>
      <span className={`flex size-8 shrink-0 items-center justify-center rounded-lg ${config.className}`}>
        <Icon className="size-4" />
      </span>
      <div className="flex-1 min-w-0">
        <div className="flex items-start justify-between gap-2">
          <p className="text-xs font-semibold text-foreground leading-snug">{notification.title}</p>
          {!notification.read && (
            <span className={`mt-1 size-2 shrink-0 rounded-full ${config.dot}`} />
          )}
        </div>
        {notification.description && (
          <p className="text-[11px] text-muted-foreground mt-0.5 leading-snug">{notification.description}</p>
        )}
        <span className="text-[10px] text-muted-foreground/60 mt-1 block">{relativeTime(notification.timestamp)}</span>
      </div>
    </div>
  )
}

export function NotificationBell() {
  const { notifications, markAllRead, clearAll } = useNotifications()
  const { user } = useAuth()
  const [isOpen, setIsOpen] = useState(false)
  const ref = useRef<HTMLDivElement>(null)

  const visible = notifications.filter((n) => {
    if (!n.visibleToRoles || n.visibleToRoles.length === 0) return true
    if (!user || user.role === undefined) return false
    return n.visibleToRoles.includes(user.role)
  })

  const visibleUnread = visible.filter((n) => !n.read).length

  useEffect(() => {
    function handleClickOutside(e: MouseEvent) {
      if (ref.current && !ref.current.contains(e.target as Node)) {
        setIsOpen(false)
      }
    }
    function handleKeyDown(e: KeyboardEvent) {
      if (e.key === "Escape") setIsOpen(false)
    }
    document.addEventListener("mousedown", handleClickOutside)
    document.addEventListener("keydown", handleKeyDown)
    return () => {
      document.removeEventListener("mousedown", handleClickOutside)
      document.removeEventListener("keydown", handleKeyDown)
    }
  }, [])

  function handleOpen() {
    setIsOpen((prev) => !prev)
    if (!isOpen) markAllRead()
  }

  return (
    <div className="relative" ref={ref}>
      <button
        onClick={handleOpen}
        aria-label={`Notificaciones${visibleUnread > 0 ? ` (${visibleUnread} sin leer)` : ""}`}
        aria-haspopup="dialog"
        aria-expanded={isOpen}
        className="relative rounded-lg p-2 hover:bg-accent/10 text-foreground transition-colors"
      >
        <Bell className="size-5" />
        {visibleUnread > 0 && (
          <span className="absolute top-1 right-1 flex size-4 items-center justify-center rounded-full bg-accent text-[9px] font-bold text-white">
            {visibleUnread > 9 ? "9+" : visibleUnread}
          </span>
        )}
      </button>

      {isOpen && (
        <div role="dialog" aria-label="Notificaciones" className="absolute right-0 top-full mt-2 w-80 rounded-2xl border border-border bg-card shadow-xl shadow-black/10 z-50 flex flex-col overflow-hidden">
          <div className="flex items-center justify-between px-4 py-3 border-b border-border">
            <div>
              <h3 className="text-sm font-bold text-foreground">Notificaciones</h3>
              <p className="text-[11px] text-muted-foreground">
                {visible.length === 0 ? "Sin notificaciones" : `${visible.length} en total`}
              </p>
            </div>
            {visible.length > 0 && (
              <button
                onClick={clearAll}
                className="flex items-center gap-1 text-[11px] text-muted-foreground hover:text-destructive transition-colors"
              >
                <Trash2 className="size-3.5" /> Limpiar
              </button>
            )}
          </div>

          {visible.length === 0 ? (
            <div className="flex flex-col items-center justify-center py-10 px-4 text-center">
              <span className="flex size-12 items-center justify-center rounded-full bg-accent/10 text-accent">
                <CheckCheck className="size-6" />
              </span>
              <p className="mt-3 text-sm font-semibold text-foreground">Todo al día</p>
              <p className="mt-1 text-xs text-muted-foreground">No hay notificaciones por mostrar.</p>
            </div>
          ) : (
            <div className="flex flex-col gap-1 p-2 overflow-y-auto max-h-96">
              {visible.map((n) => (
                <NotificationItem key={n.id} notification={n} />
              ))}
            </div>
          )}
        </div>
      )}
    </div>
  )
}
