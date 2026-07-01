import { apiRequest } from "./api"
import { Doctor } from "@/types/doctor"

export const doctorService = {
  getAll: () => apiRequest<Doctor[]>("api/Doctors"),
  getById: (id: string) => apiRequest<Doctor>(`api/Doctors/${id}`),
}
