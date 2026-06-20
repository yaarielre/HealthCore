export interface UserActivityLog {
  id: string
  userId: string
  userName: string
  action: string
  module: string
  details?: string
  ipAddress?: string
  createdAt: string
}
