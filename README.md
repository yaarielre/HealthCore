# HealthCore

HealthCore es un proyecto integral en desarrollo, diseñado bajo los principios de **Clean Architecture**. Actualmente, este repositorio contiene la implementación inicial del **Backend**, mientras que el Frontend se integrará en futuras etapas del desarrollo.

## 🏗️ Estructura del Proyecto

El proyecto está dividido principalmente en dos áreas (Backend y Frontend):

### Backend (`/backend`)
Desarrollado en **.NET 9** utilizando **C#** y **Entity Framework Core**. Sigue un enfoque de Arquitectura Limpia (Clean Architecture), separando las responsabilidades en distintas capas:
- **`HealthCore.API`**: Capa de presentación (Controladores, configuración de Swagger).
- **`HealthCore.Application`**: Casos de uso, interfaces y lógica de la aplicación.
- **`HealthCore.Domain`**: Entidades centrales, enumeraciones y lógica del negocio pura.
- **`HealthCore.Infrastructure`**: Implementación de acceso a datos (Entity Framework Core), repositorios y servicios externos.

### Frontend (`/frontend`)
Desarrollado con **Next.js** (React). Esta capa se encarga de la interfaz de usuario, conectándose con la API del backend para consumir y enviar datos.

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

1. **Conexión Frontend-Backend**: Asegurar que los endpoints del backend se consuman correctamente desde la interfaz en Next.js.
2. **Autenticación/Autorización**: Implementación de JWT (JSON Web Tokens) u otro método de seguridad.
3. **Despliegue (Deployment)**: Configuración de CI/CD para producción.

---

## 👥 Contribución

*(Sección reservada para las normas de contribución de la empresa: flujos de ramas, pull requests, revisiones de código, etc.)*

1. Crea una nueva rama para tu feature: `git checkout -b feature/nombre-de-la-feature`
2. Realiza tus cambios y haz commits descriptivos.
3. Envía tu código al repositorio: `git push origin feature/nombre-de-la-feature`
4. Crea un Pull Request para su revisión.
