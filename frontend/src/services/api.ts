const BASE_URL = process.env.NEXT_PUBLIC_API_URL || "https://localhost:7011"

interface RequestOptions extends Omit<RequestInit, "body"> {
  body?: unknown
}

export async function apiRequest<T>(endpoint: string, options: RequestOptions = {}): Promise<T> {
  const url = `${BASE_URL.replace(/\/$/, "")}/${endpoint.replace(/^\//, "")}`

  const headers = new Headers(options.headers)
  
  if (!headers.has("Content-Type") && !(options.body instanceof FormData)) {
    headers.set("Content-Type", "application/json")
  }

  if (typeof window !== "undefined") {
    const token = localStorage.getItem("healthcore_token")
    if (token) {
      headers.set("Authorization", `Bearer ${token}`)
    }
  }

  const { body, ...restOptions } = options

  const config: RequestInit = {
    ...restOptions,
    headers,
  }

  if (body) {
    if (body instanceof FormData) {
      config.body = body
    } else {
      config.body = JSON.stringify(body)
    }
  }

  try {
    const response = await fetch(url, config)

    if (!response.ok) {
      let errorMessage = "Ocurrió un error inesperado"
      try {
        const errorData = await response.json()
        errorMessage = errorData.message || errorData.Message || errorMessage
      } catch {
        errorMessage = response.statusText || errorMessage
      }
      throw new Error(errorMessage)
    }

    if (response.status === 204) {
      return {} as T
    }

    return await response.json() as T
  } catch (error: unknown) {
    console.error(`Error en la petición API [${url}]:`, error)
    throw error
  }
}
