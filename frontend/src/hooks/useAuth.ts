import { useState, useEffect } from "react"
import { useRouter } from "next/navigation"
import { notify } from "@/lib/notify"
import { authService } from "@/services/authService"
import { LoginDto, RegisterDto, AuthUser } from "@/types/auth"

export function useAuth() {
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const [user, setUser] = useState<AuthUser | null>(null)
  const router = useRouter()

  useEffect(() => {
    try {
      setUser(authService.getCurrentUser())
    } catch {
      setUser(null)
    }
  }, [])

  async function handleLogin(credentials: LoginDto) {
    setLoading(true)
    setError(null)
    try {
      const response = await authService.login(credentials)
      setUser({
        id: response.id,
        fullName: response.fullName,
        email: response.email,
        role: response.role
      })
      notify.success(`¡Bienvenido, ${response.fullName}!`, {
        description: "Has iniciado sesión correctamente.",
      })
      
      router.push("/")
      return true
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Credenciales inválidas"
      setError(message)
      notify.error("Error al iniciar sesión", { description: message })
      return false
    } finally {
      setLoading(false)
    }
  }

  async function handleRegister(userData: RegisterDto) {
    setLoading(true)
    setError(null)
    try {
      await authService.register(userData)
      notify.success("¡Registro completado!", {
        description: "El usuario ha sido creado con éxito. Ahora puedes iniciar sesión.",
      })
      
      router.push("/")
      return true
    } catch (err: unknown) {
      const message = err instanceof Error ? err.message : "Error al registrar usuario"
      setError(message)
      notify.error("Error en el registro", { description: message })
      return false
    } finally {
      setLoading(false)
    }
  }

  function handleLogout() {
    authService.logout()
    setUser(null)
    notify.info("Sesión cerrada", { description: "Has salido del sistema." })
    router.push("/")
  }

  return {
    loading,
    error,
    user,
    isAuthenticated: !!user,
    login: handleLogin,
    register: handleRegister,
    logout: handleLogout,
  }
}
