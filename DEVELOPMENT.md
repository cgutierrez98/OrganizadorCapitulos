# Manual de Desarrollo - Organizador de Capítulos

Este documento sirve como guía técnica para desarrolladores que deseen compilar, ejecutar y contribuir al proyecto **Organizador de Capítulos**.

## 1. Configuración del Entorno de Desarrollo

Antes de comenzar, asegúrese de tener instalado el siguiente software:

### Requisitos Principales
*   **Visual Studio 2022** (Versión 17.8 o superior)
    *   **Cargas de trabajo requeridas:**
        *   *Desarrollo de escritorio de .NET*
        *   *Desarrollo con la interfaz de usuario de aplicaciones multiplataforma de .NET (.NET MAUI)*
*   **.NET 9.0 SDK**: [Descargar aquí](https://dotnet.microsoft.com/download/dotnet/9.0)

### Requisitos Opcionales (Funcionalidad IA)
*   **Python 3.x**: Necesario si se planea depurar o modificar los scripts de análisis de IA en `Infrastructure/python`.
    *   Asegúrese de agregar Python al `PATH` del sistema.

## 2. Estructura de la Solución

La solución `organizadorCapitulos.sln` se compone de los siguientes proyectos:

| Proyecto | Tipo | Descripción |
| :--- | :--- | :--- |
| **OrganizadorCapitulos.Maui** | .NET MAUI | La aplicación moderna multiplataforma (Blazor Hybrid). Es el punto de entrada principal recomendado. |
| **organizadorCapitulos** | WinForms | La versión clásica de escritorio/Core. Contiene lógica compartida y formularios legacy. |
| **organizadorCapitulos.Tests** | xUnit | Pruebas unitarias para validar la lógica del núcleo. |
| *OrganizadorCapitulos.Web* | - | *Nota: Este proyecto puede aparecer como "no cargado" o faltante en el entorno actual.* |

### Arquitectura Lógica
El código sigue una estructura de capas para separar responsabilidades:
*   **Core**: Entidades del dominio e Interfaces (e.g., `IAIService`, `IMetadataService`). No tiene dependencias externas.
*   **Application**: Servicios de aplicación y orquestación.
*   **Infrastructure**: Implementación de servicios externos (API de TMDB, Scripts de Python, Sistema de Archivos).
*   **UI**: Capa de presentación (Componentes Razor en MAUI, Formularios en WinForms).

## 3. Guía de Compilación

### Opción A: Visual Studio 2022 (Recomendado)
1.  Abra el archivo `organizadorCapitulos.sln`.
2.  Si se le solicita, permítale a Visual Studio restaurar los paquetes NuGet.
3.  Establezca el proyecto de inicio (clic derecho en el proyecto -> **Establecer como proyecto de inicio**):
    *   Para la app moderna: `OrganizadorCapitulos.Maui`
    *   Para la clásica: `organizadorCapitulos`
4.  Presione `Ctrl + Shift + B` o vaya a **Compilar > Compilar solución**.

> **Nota:** Es posible que vea un error sobre `OrganizadorCapitulos.Web`. Puede ignorarlo si solo trabaja en las aplicaciones de escritorio.

### Opción B: Línea de Comandos (CLI)
Para compilar toda la solución:
```powershell
dotnet build
```

Para compilar solo el proyecto MAUI:
```powershell
dotnet build OrganizadorCapitulos.Maui/OrganizadorCapitulos.Maui.csproj
```

## 4. Ejecución y Depuración

1.  Asegúrese de estar en configuración **Debug**.
2.  Seleccione el framework destino (si aplica). Para MAUI en Windows, seleccione `Windows Machine`.
3.  Presione `F5` para iniciar con depuración.

### Configuración de API Keys
Para que la búsqueda de metadatos (TMDB) funcione:
1.  Inicie la aplicación.
2.  Vaya a **Configuración** (ícono de engranaje).
3.  Ingrese su API Key de TMDB (se puede obtener gratuitamente en themoviedb.org).

## 5. Pruebas Automatizadas

El proyecto incluye pruebas unitarias para validar reglas de negocio críticas (como el parseo de nombres de archivos).

Para ejecutarlas desde la terminal:
```powershell
dotnet test
```

## 6. Solución de Problemas Comunes

*   **Error: "Recurso no encontrado" o fallos en XAML/Razor**: Intente limpiar la solución (`Compilar > Limpiar solución`) y recompilar.
*   **Advertencias de "Found markup element with unexpected name"**: Asegúrese de que el archivo `_Imports.razor` en la raíz del proyecto MAUI incluya los espacios de nombres de sus componentes.
*   **El script de Python falla**: Verifique que la ruta al ejecutable de Python esté correctamente configurada o que el script `analyzer.py` tenga permisos de ejecución.
