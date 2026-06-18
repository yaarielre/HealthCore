# HealthCore

HealthCore es un sistema de gestión clínica integral diseñado bajo los principios de **Clean Architecture** (Backend) y **Clean Code/Modularización** (Frontend). El proyecto incluye un API robusto en .NET 9 y una interfaz de usuario moderna y responsiva en Next.js.

## 🏗️ Estructura del Proyecto

El proyecto está dividido principalmente en dos áreas (Backend y Frontend):

### Backend (`/backend`)
Desarrollado en **.NET 9** utilizando **C#** y **Entity Framework Core**. Sigue un enfoque de Arquitectura Limpia (Clean Architecture), separando las responsabilidades en distintas capas:
- **`HealthCore.API`**: Capa de presentación (Controladores, configuración de Swagger).
- **`HealthCore.Application`**: Casos de uso, interfaces y lógica de la aplicación.
- **`HealthCore.Domain`**: Entidades centrales, enumeraciones y lógica del negocio pura.
- **`HealthCore.Infrastructure`**: Implementación de acceso a datos (Entity Framework Core), repositorios y servicios externos.

### Frontend (`/frontend`)
Desarrollado con **Next.js** (React) y **Tailwind CSS**. Implementa una arquitectura modular:
- **Hooks personalizados** para la lógica de negocio (`useAuth`, `useStaffManagement`, `useSidebar`).
- **Context API** para estado global (ej. `NotificationContext`).
- **Sistema de Componentes** atómicos y reutilizables (Sidebar, UI elements, Modales).
- Integración completa con el backend mediante un cliente de API (`api.ts`).

---

## ✨ Características Actuales

- **Autenticación y Autorización**: Sistema basado en JWT. Diferenciación estricta por roles de usuario (Administrator, Manager, Doctor, Nurse, etc.).
- **Gestión de Personal Clínico**: Módulo completo para administradores con CRUD de empleados, cambio de estado (activo/inactivo) y restablecimiento de contraseñas.
- **Sistema de Notificaciones**: Panel interactivo integrado en el dashboard con persistencia local y filtrado inteligente basado en el rol del usuario logueado.
- **UI/UX Moderna**: Diseño responsivo con menús colapsables, feedback visual (toasts) e interfaces intuitivas para escritorio y móvil.

---

## 🚀 Requisitos Previos

Para poder ejecutar este proyecto localmente, necesitas tener instaladas las siguientes herramientas:

1. **[.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)** o superior (Para el Backend).
2. **[Node.js](https://nodejs.org/)** (v18 o superior recomendado, para el Frontend).
3. Un IDE recomendado como **Visual Studio 2022**, **JetBrains Rider** o **Visual Studio Code**.
4. **SQL Server** (Para entorno de desarrollo local, viene configurado por defecto para usar `(localdb)\MSSQLLocalDB`).
5. **Git** para el control de versiones.

---

## 🛠️ Instalación y Configuración Local

Sigue estos pasos para clonar y ejecutar el proyecto en tu máquina local:

### 1. Clonar el repositorio

Abre tu terminal y ejecuta el siguiente comando:

```bash
git clone <URL_DEL_REPOSITORIO>
cd HealthCore
```

### 2. Configurar la Base de Datos

El proyecto usa **Entity Framework Core** con SQL Server. La cadena de conexión predeterminada apunta a una base de datos local `(localdb)\MSSQLLocalDB`.

Si necesitas cambiar la cadena de conexión, puedes hacerlo modificando el archivo `appsettings.json` ubicado en `backend/src/HealthCore.API/appsettings.json`:

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=HealthCoreDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

### 3. Aplicar las Migraciones (Entity Framework Core)

Para crear la base de datos y sus tablas, necesitas ejecutar las migraciones.

Abre la terminal en la raíz de la solución (`backend/src`) y ejecuta:

```bash
cd backend/src/HealthCore.API
dotnet ef database update
```

*(Si no tienes las herramientas de EF Core instaladas, puedes instalarlas globalmente con: `dotnet tool install --global dotnet-ef`)*

### 4. Ejecutar el Proyecto (Backend)

Una vez que la base de datos esté configurada, puedes compilar y ejecutar el proyecto:

```bash
cd backend/src/HealthCore.API
dotnet run
```

Al iniciar correctamente, la API estará disponible. Puedes acceder a la interfaz de **Swagger** (en modo desarrollo) para explorar los endpoints disponibles desde tu navegador:

- **Swagger UI**: `https://localhost:<puerto>/swagger` o `http://localhost:<puerto>/swagger`

### 5. Ejecutar el Proyecto (Frontend)

En una nueva terminal, navega a la carpeta del frontend, instala las dependencias y ejecuta el servidor de desarrollo de Next.js:

```bash
cd frontend
npm install
npm run dev
```

La aplicación de React/Next.js estará disponible en tu navegador, típicamente en:

- **Frontend**: `http://localhost:3000`

*(Asegúrate de configurar las variables de entorno `.env.local` en el frontend si es necesario para apuntar a la URL de la API del backend).*

---

## 📋 Siguientes Pasos

1. **Gestión de Pacientes**: Implementar el módulo de registro e historial clínico de los pacientes.
2. **Sistema de Citas Médicas**: Desarrollo del calendario y la asignación de turnos.
3. **Módulos Clínicos Auxiliares**: Áreas de Laboratorio, Farmacia y Caja.
4. **Despliegue (Deployment)**: Configuración de CI/CD, dockerización y pase a entorno de producción.

---

## 👥 Contribución

*(Sección reservada para las normas de contribución de la empresa: flujos de ramas, pull requests, revisiones de código, etc.)*

1. Crea una nueva rama para tu feature: `git checkout -b feature/nombre-de-la-feature`
2. Realiza tus cambios y haz commits descriptivos.
3. Envía tu código al repositorio: `git push origin feature/nombre-de-la-feature`
4. Crea un Pull Request para su revisión.
