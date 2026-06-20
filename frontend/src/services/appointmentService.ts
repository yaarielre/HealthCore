import { apiRequest } from "./api"
import { Appointment, AppointmentStatus, CreateAppointmentDto, UpdateAppointmentDto } from "@/types/appointment"

export const appointmentService = {
  async getAll(): Promise<Appointment[]> {
    return await apiRequest<Appointment[]>("api/Appointments", { method: "GET" })
  },

  async getById(id: string): Promise<Appointment> {
    return await apiRequest<Appointment>(`api/Appointments/${id}`, { method: "GET" })
  },

  async getByDoctor(doctorId: string): Promise<Appointment[]> {
    return await apiRequest<Appointment[]>(`api/Appointments/doctor/${doctorId}`, { method: "GET" })
  },

  async getByDate(date: string): Promise<Appointment[]> {
    return await apiRequest<Appointment[]>(`api/Appointments/date/${date}`, { method: "GET" })
  },

  async create(dto: CreateAppointmentDto): Promise<Appointment> {
    return await apiRequest<Appointment>("api/Appointments", {
      method: "POST",
      body: {
        PatientId: dto.patientId,
        DoctorId: dto.doctorId,
        AppointmentDate: dto.appointmentDate,
        Reason: dto.reason,
        Notes: dto.notes || null,
      },
    })
  },

  async update(id: string, dto: UpdateAppointmentDto): Promise<Appointment> {
    return await apiRequest<Appointment>(`api/Appointments/${id}`, {
      method: "PUT",
      body: {
        AppointmentDate: dto.appointmentDate,
        Reason: dto.reason,
        Notes: dto.notes || null,
      },
    })
  },

  async changeStatus(id: string, status: AppointmentStatus): Promise<Appointment> {
    return await apiRequest<Appointment>(`api/Appointments/${id}/status`, {
      method: "PATCH",
      body: status,
    })
  },
}
