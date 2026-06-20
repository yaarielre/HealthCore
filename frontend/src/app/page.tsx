"use client"

import { useState, useEffect } from "react"
import Image from "next/image"
import { 
  ShieldCheck, 
  Activity, 
  Clock, 
  Users, 
  Calendar, 
  UserCog, 
  TrendingUp, 
  Plus, 
  Search
} from "lucide-react"

import { LoginForm } from "@/components/login-form"
import { Sidebar } from "@/components/sidebar"
import { DoctorManagement } from "@/components/doctor-management"
import { PatientManagement } from "@/components/patient-management"
import { AppointmentManagement } from "@/components/appointment-management"
import { NotificationBell } from "@/components/notification-bell"
import { NotificationProvider } from "@/contexts/notification-context"
import { useAuth } from "@/hooks/useAuth"

export default function Page() {
  const { isAuthenticated, user } = useAuth()
  const [activeTab, setActiveTab] = useState("dashboard")
  const [mounted, setMounted] = useState(false)
  const [collapsed, setCollapsed] = useState(false)

  useEffect(() => {
    setMounted(true)
  }, [])

  if (!mounted) {
    return (
      <div className="flex min-h-screen items-center justify-center bg-background text-muted-foreground">
        Cargando HealthCore...
      </div>
    )
  }

  if (!isAuthenticated) {
    return (
      <main className="grid min-h-svh lg:grid-cols-2">
        <section className="relative hidden flex-col justify-between overflow-hidden bg-[oklch(0.18_0.015_250)] p-12 text-white lg:flex">
          <div
            aria-hidden="true"
            className="pointer-events-none absolute inset-0 opacity-[0.06]"
            style={{
              backgroundImage:
                "radial-gradient(circle at 1px 1px, currentColor 1px, transparent 0)",
              backgroundSize: "28px 28px",
            }}
          />

          <div className="relative flex items-center gap-3">
            <div className="flex size-12 items-center justify-center rounded-xl bg-card p-1.5">
              <Image
                src="/healthcore-hc.png"
                alt="HealthCore"
                width={48}
                height={48}
                className="h-auto w-full"
              />
            </div>
            <span className="text-lg font-semibold tracking-tight">HealthCore</span>
          </div>

          <div className="relative max-w-md">
            <h2 className="text-balance text-3xl font-semibold leading-tight">
              El cuidado de tus pacientes, en un solo lugar.
            </h2>
            <p className="mt-4 text-pretty leading-relaxed text-white/65">
              Gestiona historiales clínicos, citas y resultados de manera segura
              desde la plataforma de HealthCore Medical Center.
            </p>

            <ul className="mt-8 flex flex-col gap-4">
              {[
                { icon: ShieldCheck, text: "Datos clínicos cifrados y protegidos" },
                { icon: Activity, text: "Monitoreo de pacientes en tiempo real" },
                { icon: Clock, text: "Acceso disponible las 24 horas" },
              ].map(({ icon: Icon, text }) => (
                <li key={text} className="flex items-center gap-3">
                  <span className="flex size-9 items-center justify-center rounded-lg bg-accent/20 text-accent">
                    <Icon className="size-4" />
                  </span>
                  <span className="text-sm text-white/85">{text}</span>
                </li>
              ))}
            </ul>
          </div>

          <p className="relative text-sm text-white/45">
            © {new Date().getFullYear()} HealthCore Medical Center
          </p>
        </section>

        <section className="flex items-center justify-center bg-background px-6 py-12">
          <LoginForm />
        </section>
      </main>
    )
  }

  return (
    <NotificationProvider>
    <div className="min-h-screen bg-background">
      <Sidebar activeTab={activeTab} setActiveTab={setActiveTab} collapsed={collapsed} setCollapsed={setCollapsed} />

      <main className={`transition-all duration-300 min-h-screen flex flex-col ${collapsed ? "lg:pl-20" : "lg:pl-64"}`}>
        <header className="flex h-16 items-center justify-between border-b border-border bg-card px-6 lg:px-8">
          <div>
            <h1 className="text-lg font-bold text-foreground">
              {activeTab === "dashboard" ? "Resumen Médico" : activeTab === "staff" ? "Personal Clínico" : activeTab}
            </h1>
            <p className="text-xs text-muted-foreground hidden sm:block">
              Bienvenido de nuevo, {user?.fullName || "Administrador"}
            </p>
          </div>

          <div className="flex items-center gap-4">
            <div className="relative hidden md:block">
              <Search className="absolute left-2.5 top-1/2 size-4 -translate-y-1/2 text-muted-foreground" />
              <input
                type="search"
                placeholder="Buscar..."
                className="w-60 rounded-lg border border-border bg-background py-1.5 pl-8 pr-4 text-xs focus:outline-none focus:ring-1 focus:ring-accent"
              />
            </div>

            <NotificationBell />

            <div className="flex items-center gap-2 border-l border-border pl-4">
              <div className="flex size-8 items-center justify-center rounded-full bg-accent/10 text-accent font-semibold text-xs">
                {user?.fullName ? user.fullName.charAt(0).toUpperCase() : "A"}
              </div>
              <div className="hidden xl:block text-left">
                <p className="text-xs font-semibold text-foreground">{user?.fullName}</p>
                <p className="text-[10px] text-muted-foreground">Administrador</p>
              </div>
            </div>
          </div>
        </header>

        <div className="flex-1 p-6 lg:p-8 space-y-6">
          {activeTab === "dashboard" && (
            <>
              <div className="grid gap-4 sm:grid-cols-2 lg:grid-cols-4">
                {[
                  { title: "Pacientes Registrados", value: "1,248", change: "+12% este mes", icon: Users, color: "text-blue-500 bg-blue-500/10" },
                  { title: "Citas para Hoy", value: "42", change: "6 pendientes de atender", icon: Calendar, color: "text-amber-500 bg-amber-500/10" },
                  { title: "Personal en Servicio", value: "18", change: "15 médicos disponibles", icon: UserCog, color: "text-green-500 bg-green-500/10" },
                  { title: "Tasa de Asistencia", value: "94.6%", change: "+2.4% vs mes anterior", icon: TrendingUp, color: "text-emerald-500 bg-emerald-500/10" }
                ].map((stat, i) => {
                  const Icon = stat.icon
                  return (
                    <div key={i} className="rounded-2xl border border-border bg-card p-6 shadow-sm">
                      <div className="flex items-center justify-between">
                        <span className="text-sm font-medium text-muted-foreground">{stat.title}</span>
                        <span className={`rounded-xl p-2 ${stat.color}`}>
                          <Icon className="size-5" />
                        </span>
                      </div>
                      <div className="mt-4">
                        <h3 className="text-2xl font-bold tracking-tight text-foreground">{stat.value}</h3>
                        <p className="text-xs text-muted-foreground mt-1">{stat.change}</p>
                      </div>
                    </div>
                  )
                })}
              </div>

              <div className="grid gap-6 lg:grid-cols-3">
                <div className="rounded-2xl border border-border bg-card p-6 shadow-sm lg:col-span-2">
                  <div className="flex items-center justify-between mb-4">
                    <div>
                      <h3 className="font-semibold text-foreground text-sm">Citas Programadas de Hoy</h3>
                      <p className="text-xs text-muted-foreground">Listado en tiempo real de consultas</p>
                    </div>
                    <button className="flex items-center gap-1.5 text-xs font-semibold text-accent hover:text-accent/80 transition-colors">
                      <Plus className="size-3.5" /> Nueva Cita
                    </button>
                  </div>

                  <div className="overflow-x-auto">
                    <table className="w-full text-left border-collapse">
                      <thead>
                        <tr className="border-b border-border text-xs text-muted-foreground">
                          <th className="py-3 font-medium">Paciente</th>
                          <th className="py-3 font-medium">Médico / Especialidad</th>
                          <th className="py-3 font-medium">Hora</th>
                          <th className="py-3 font-medium">Estado</th>
                        </tr>
                      </thead>
                      <tbody className="divide-y divide-border text-xs">
                        {[
                          { name: "Carlos Mendoza", doctor: "Dr. Hamilton (Cardiología)", time: "09:30 AM", status: "Realizada", statusColor: "text-green-500 bg-green-500/10" },
                          { name: "María Pérez", doctor: "Dra. García (Pediatría)", time: "10:15 AM", status: "En Consulta", statusColor: "text-blue-500 bg-blue-500/10" },
                          { name: "José Rodríguez", doctor: "Dr. Martínez (General)", time: "11:00 AM", status: "Esperando", statusColor: "text-amber-500 bg-amber-500/10" },
                          { name: "Ana Gómez", doctor: "Dra. Sánchez (Dermatología)", time: "11:45 AM", status: "Pendiente", statusColor: "text-muted-foreground bg-accent/20" }
                        ].map((appointment, index) => (
                          <tr key={index} className="hover:bg-accent/5 transition-colors">
                            <td className="py-3.5 font-semibold text-foreground">{appointment.name}</td>
                            <td className="py-3.5 text-muted-foreground">{appointment.doctor}</td>
                            <td className="py-3.5 text-muted-foreground">{appointment.time}</td>
                            <td className="py-3.5">
                              <span className={`inline-flex items-center rounded-full px-2 py-0.5 font-medium ${appointment.statusColor}`}>
                                {appointment.status}
                              </span>
                            </td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  </div>
                </div>

                <div className="rounded-2xl border border-border bg-card p-6 shadow-sm">
                  <h3 className="font-semibold text-foreground text-sm mb-4">Actividad del Sistema</h3>
                  <div className="space-y-4">
                    {[
                      { action: "Usuario Creado", details: "El Dr. Martínez fue registrado", time: "Hace 10 min", icon: UserCog, color: "text-blue-500" },
                      { action: "Cita Modificada", details: "Ana Gómez pospuso su consulta", time: "Hace 30 min", icon: Calendar, color: "text-amber-500" },
                      { action: "Triaje Completado", details: "Paciente Carlos Mendoza listo", time: "Hace 1 hora", icon: Activity, color: "text-green-500" }
                    ].map((log, index) => {
                      const LogIcon = log.icon
                      return (
                        <div key={index} className="flex gap-3 text-xs">
                          <span className={`flex size-8 shrink-0 items-center justify-center rounded-lg bg-accent/5 ${log.color}`}>
                            <LogIcon className="size-4" />
                          </span>
                          <div className="flex-1">
                            <p className="font-semibold text-foreground">{log.action}</p>
                            <p className="text-muted-foreground mt-0.5">{log.details}</p>
                            <span className="text-[10px] text-muted-foreground block mt-1">{log.time}</span>
                          </div>
                        </div>
                      )
                    })}
                  </div>
                </div>
              </div>
            </>
          )}

          {activeTab === "staff" && (
            user?.role === 1 || user?.role === 2 ? (
              <DoctorManagement />
            ) : (
              <div className="flex h-[60vh] flex-col items-center justify-center rounded-2xl border border-dashed border-border bg-card p-8 text-center shadow-sm">
                <span className="flex size-14 items-center justify-center rounded-full bg-destructive/10 text-destructive">
                  <ShieldCheck className="size-7" />
                </span>
                <h3 className="mt-4 font-semibold text-foreground text-base">Acceso Denegado</h3>
                <p className="mt-2 text-sm text-muted-foreground max-w-sm">
                  Solo Administradores y Gerentes Clínicos pueden gestionar el personal.
                </p>
              </div>
            )
          )}

          {activeTab === "patients" && (
            <PatientManagement />
          )}

          {activeTab === "appointments" && (
            <AppointmentManagement />
          )}

          {activeTab !== "dashboard" && activeTab !== "staff" && activeTab !== "patients" && activeTab !== "appointments" && (
            <div className="flex h-[60vh] flex-col items-center justify-center rounded-2xl border border-dashed border-border bg-card p-8 text-center shadow-sm">
              <span className="flex size-14 items-center justify-center rounded-full bg-accent/10 text-accent">
                <Users className="size-7" />
              </span>
              <h3 className="mt-4 font-semibold text-foreground text-base capitalize">{activeTab}</h3>
              <p className="mt-2 text-sm text-muted-foreground max-w-sm">
                La sección de {activeTab} aún no tiene lógica asociada. Los datos de prueba se cargarán próximamente.
              </p>
            </div>
          )}
        </div>
      </main>
    </div>
    </NotificationProvider>
  )
}
