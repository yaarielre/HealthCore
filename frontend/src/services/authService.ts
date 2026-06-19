import { apiRequest } from "./api"

import { LoginDto, RegisterDto, AuthResponseDto } from "@/types/auth"

export const authService = {
  async login(credentials: LoginDto): Promise<AuthResponseDto> {
    const response = await apiRequest<AuthResponseDto>("api/Auth/login", {
      method: "POST",
      body: {
        Email: credentials.email,
        Password: credentials.password
      }
    })

    if (response && response.token) {
      localStorage.setItem("healthcore_token", response.token)
      localStorage.setItem("healthcore_user", JSON.stringify({
        fullName: response.fullName,
        email: response.email,
        role: response.role
      }))
    }

    return response
  },

  async register(userData: RegisterDto): Promise<AuthResponseDto> {
    const response = await apiRequest<AuthResponseDto>("api/Auth/register", {
      method: "POST",
      body: {
        FirstName: userData.firstName,
        LastName: userData.lastName,
        IdNumber: userData.idNumber,
        Email: userData.email,
        Password: userData.password,
        Phone: userData.phone,
        Role: Number(userData.role),
        DoctorId: userData.doctorId || null
      }
    })

    return response
  },

  logout(): void {
    localStorage.removeItem("healthcore_token")
    localStorage.removeItem("healthcore_user")
  },

  getCurrentUser() {
    if (typeof window === "undefined") return null
    const user = localStorage.getItem("healthcore_user")
    return user ? JSON.parse(user) : null
  },

  isAuthenticated(): boolean {
    if (typeof window === "undefined") return false
    return !!localStorage.getItem("healthcore_token")
  },

  async getUsers(): Promise<unknown[]> {
    return await apiRequest<unknown[]>("api/Users", {
      method: "GET"
    })
  },

  async changePassword(userId: string, newPassword: string): Promise<void> {
    await apiRequest<void>(`api/Users/${userId}/password`, {
      method: "PATCH",
      body: {
        NewPassword: newPassword
      }
    })
  },

  async changeUserStatus(userId: string, status: number): Promise<void> {
    await apiRequest<void>(`api/Users/${userId}/status`, {
      method: "PATCH",
      body: status
    })
  }
}
