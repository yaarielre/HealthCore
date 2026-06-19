import { toast } from "sonner"
import type { AppNotification } from "@/types/notification"

type AddFn = (n: Omit<AppNotification, "id" | "timestamp" | "read">) => void

let _handler: AddFn | null = null

export function setNotificationHandler(fn: AddFn) {
  _handler = fn
}

export type NotifyOptions = {
  description?: string
  visibleToRoles?: number[]
}

function push(title: string, type: AppNotification["type"], options?: NotifyOptions) {
  _handler?.({
    title,
    description: options?.description,
    type,
    visibleToRoles: options?.visibleToRoles,
  })
}

export const notify = {
  success(title: string, options?: NotifyOptions) {
    toast.success(title, { description: options?.description })
    push(title, "success", options)
  },
  error(title: string, options?: NotifyOptions) {
    toast.error(title, { description: options?.description })
    push(title, "error", options)
  },
  warning(title: string, options?: NotifyOptions) {
    toast.warning(title, { description: options?.description })
    push(title, "warning", options)
  },
  info(title: string, options?: NotifyOptions) {
    toast.info(title, { description: options?.description })
    push(title, "info", options)
  },
}
