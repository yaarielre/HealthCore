import { apiRequest } from "./api"
import { Patient, CreatePatientDto, UpdatePatientDto } from "@/types/patient"

export const patientService = {
  async getAll(): Promise<Patient[]> {
    return await apiRequest<Patient[]>("api/Patients", { method: "GET" })
  },

  async getById(id: string): Promise<Patient> {
    return await apiRequest<Patient>(`api/Patients/${id}`, { method: "GET" })
  },

  async create(dto: CreatePatientDto): Promise<Patient> {
    return await apiRequest<Patient>("api/Patients", {
      method: "POST",
      body: {
        FirstName: dto.firstName,
        LastName: dto.lastName,
        Cedula: dto.cedula,
        BirthDate: dto.birthDate,
        Phone: dto.phone,
        Address: dto.address,
      },
    })
  },

  async update(id: string, dto: UpdatePatientDto): Promise<Patient> {
    return await apiRequest<Patient>(`api/Patients/${id}`, {
      method: "PUT",
      body: {
        FirstName: dto.firstName,
        LastName: dto.lastName,
        Phone: dto.phone,
        Address: dto.address,
      },
    })
  },

  async delete(id: string): Promise<void> {
    await apiRequest<void>(`api/Patients/${id}`, { method: "DELETE" })
  },
}
