# Proyecto PAW - Tareas 2 y 3

Este proyecto contiene la solución a las Tareas 2 y 3 del curso, implementando una arquitectura N-Capas en .NET.

## 🚀 Tecnologías Utilizadas
*   **.NET 10.0** (ASP.NET Core MVC y Minimal API).
*   **JavaScript (Vanilla)** para el consumo asíncrono de APIs.
*   **Arquitectura N-Capas**: `PAW.Models`, `PAW.DataAccess`, `PAW.Architecture`, `PAW.API` y `PAW.Web`.
*   **Inyección de Dependencias (DI)**: Utilizada para el repositorio en memoria y los servicios.

---

## 📋 Tarea 2: Widget de Tipo de Cambio
*   **Objetivo**: Cargar el tipo de cambio flotando en la parte superior derecha sin usar el Controller.
*   **Implementación**:
    *   Se utiliza la API pública del Ministerio de Hacienda (`https://api.hacienda.go.cr/indicadores/tc`).
    *   La lógica está en `wwwroot/js/site.js`.
    *   Se implementó una **caché en memoria** (`_exchangeRateCache`) para evitar múltiples llamadas a la API por cada navegación.
    *   El widget flota gracias a los estilos `position: fixed` en `wwwroot/css/site.css`.

## 📋 Tarea 3: Minimal API de Tareas (CRUD)
*   **Objetivo**: Crear un CRUD de Tareas usando Minimal API e Inyección de Dependencias, **sin usar Entity Framework**.
*   **Implementación**:
    *   La entidad `Task` se encuentra en `PAW.Models`.
    *   El repositorio en memoria (`TaskRepository`) está en `PAW.DataAccess`.
    *   Los endpoints (`GET`, `POST`, `PUT`, `DELETE`) están en `PAW.API/Program.cs`.
    *   La pantalla de usuario se creó en `PAW.Web/Views/Task/Index.cshtml` y consume la API mediante `fetch`.

---

## 🛠️ ¿Cómo ejecutar el proyecto?

Debido a que la solución separa el Frontend (MVC) del Backend (API), es necesario correr **dos proyectos al mismo tiempo**.

### Opción A: Desde Visual Studio
1. Haz clic derecho sobre la solución `PAW` en el Explorador de Soluciones.
2. Selecciona **"Configurar proyectos de inicio..."** (Set Startup Projects).
3. Elige **"Varios proyectos de inicio"** (Multiple startup projects).
4. Pon `PAW.Web` y `PAW.API` en **"Iniciar"** (Start) y dale Aceptar.
5. Presiona **F5**.

### Opción B: Desde la Terminal (PowerShell)
Abre **dos terminales** en la carpeta raíz del proyecto:

**Terminal 1 (Backend / API):**
```powershell
dotnet run --project .\PAW.API\PAW.API.csproj

(El puerto por defecto configurado es http://localhost:5146).

**Terminal 2 (Frontend / Web):**

```powershell
dotnet run --project .\PAW.Web\PAW.Web.csproj
(El puerto de la web puede variar, revisa la consola. Ej: http://localhost:5252).

Una vez corriendo ambos, abre tu navegador en la URL de la Web y haz clic en la pestaña "Tareas".
