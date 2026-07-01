"use client"

import type React from "react"
import { useState } from "react"
import Image from "next/image"
import { Eye, EyeOff, Lock, Mail, Loader2 } from "lucide-react"

import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Checkbox } from "@/components/ui/checkbox"
import { useAuth } from "@/hooks/useAuth"

export function LoginForm() {
  const [showPassword, setShowPassword] = useState(false)
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")
  
  const { login, loading } = useAuth()

  async function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault()
    if (!email || !password) return
    await login({ email, password })
  }

  return (
    <div className="w-full max-w-md">
      <div className="rounded-2xl border border-border bg-card p-8 shadow-xl shadow-primary/5 sm:p-10">
        {/* Logo */}
        <div className="flex flex-col items-center gap-2">
          <Image
            src="/healthcore-logo.png"
            alt="HealthCore Medical Center"
            width={220}
            height={150}
            priority
            className="h-auto w-44"
          />
        </div>

        <div className="mt-6 text-center">
          <h1 className="text-balance text-2xl font-semibold tracking-tight text-foreground">
            Bienvenido de nuevo
          </h1>
          <p className="mt-1 text-pretty text-sm leading-relaxed text-muted-foreground">
            Ingresa tus credenciales para acceder al sistema médico
          </p>
        </div>

        <form onSubmit={handleSubmit} className="mt-8 flex flex-col gap-5">
          <div className="flex flex-col gap-2">
            <Label htmlFor="email">Correo electrónico</Label>
            <div className="relative">
              <Mail className="pointer-events-none absolute left-3 top-1/2 size-4 -translate-y-1/2 text-muted-foreground" />
              <Input
                id="email"
                type="email"
                inputMode="email"
                autoComplete="email"
                placeholder="nombre@healthcore.com"
                required
                className="pl-9"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
            </div>
          </div>

          <div className="flex flex-col gap-2">
            <div className="flex items-center justify-between">
              <Label htmlFor="password">Contraseña</Label>
              <button
                type="button"
                className="text-sm font-medium text-accent transition-colors hover:text-accent/80"
              >
                ¿Olvidaste tu contraseña?
              </button>
            </div>
            <div className="relative">
              <Lock className="pointer-events-none absolute left-3 top-1/2 size-4 -translate-y-1/2 text-muted-foreground" />
              <Input
                id="password"
                type={showPassword ? "text" : "password"}
                autoComplete="current-password"
                placeholder="••••••••"
                required
                className="px-9"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
              <button
                type="button"
                onClick={() => setShowPassword((v) => !v)}
                aria-label={showPassword ? "Ocultar contraseña" : "Mostrar contraseña"}
                className="absolute right-3 top-1/2 -translate-y-1/2 text-muted-foreground transition-colors hover:text-foreground"
              >
                {showPassword ? <EyeOff className="size-4" /> : <Eye className="size-4" />}
              </button>
            </div>
          </div>

          <div className="flex items-center gap-2">
            <Checkbox id="remember" />
            <Label htmlFor="remember" className="text-sm font-normal text-muted-foreground">
              Mantener sesión iniciada
            </Label>
          </div>

          <Button type="submit" disabled={loading} className="mt-1 w-full">
            {loading ? (
              <>
                <Loader2 className="size-4 animate-spin mr-2" />
                Ingresando...
              </>
            ) : (
              "Iniciar sesión"
            )}
          </Button>
        </form>

        <p className="mt-6 text-center text-sm text-muted-foreground">
          ¿No tienes una cuenta? Contacta al administrador del sistema.
        </p>
      </div>

      <p className="mt-6 text-center text-xs leading-relaxed text-muted-foreground">
        Al continuar aceptas las políticas de privacidad y el manejo seguro de
        datos clínicos de HealthCore Medical Center.
      </p>
    </div>
  )
}

