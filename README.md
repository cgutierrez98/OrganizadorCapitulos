# Organizador de Capítulos - MAUI Blazor Hybrid

Aplicación de escritorio para organizar y renombrar archivos de video de series de TV.

## Requisitos

### Para desarrollo:
- **.NET 9 SDK** - [Descargar](https://dotnet.microsoft.com/download/dotnet/9.0)
- **Visual Studio 2022** (17.8+) con carga de trabajo ".NET MAUI"
- **Python 3.x** (opcional, para análisis con IA)

### Para ejecución:
- **Windows 10/11** (versión 1809 o superior)
- **.NET 9 Runtime**
- **Python 3.x** en el PATH del sistema (si usas análisis con IA)

## Estructura del Proyecto

```
organizadorCapitulos/
├── organizadorCapitulos/           # Librería core (lógica de negocio)
│   ├── Core/                       # Entidades e interfaces
│   ├── Application/                # Servicios y estrategias
│   ├── Infrastructure/             # Implementaciones (IA, TMDB)
│   └── Python/                     # Script de análisis IA
│
├── OrganizadorCapitulos.Maui/      # Aplicación MAUI Blazor Hybrid
│   ├── Components/                 # Componentes Blazor
│   │   ├── Pages/                  # Páginas principales
│   │   └── Layout/                 # Layouts
│   ├── Models/                     # ViewModels
│   ├── Services/                   # Servicios MAUI
│   ├── Python/                     # Script IA (copia)
│   └── wwwroot/                    # Assets web (CSS, JS)
│
└── organizadorCapitulos.Tests/     # Tests unitarios
```

## Compilación

### Desde línea de comandos:

```powershell
# Navegar al directorio del proyecto
cd c:\Users\carlo\source\repos\organizadorCapitulos

# Restaurar paquetes
dotnet restore

# Compilar (Debug)
dotnet build OrganizadorCapitulos.Maui/OrganizadorCapitulos.Maui.csproj

# Compilar (Release)
dotnet build OrganizadorCapitulos.Maui/OrganizadorCapitulos.Maui.csproj -c Release
```

### Desde Visual Studio:
1. Abrir `organizadorCapitulos.sln`
2. Seleccionar `OrganizadorCapitulos.Maui` como proyecto de inicio
3. Seleccionar configuración `Debug` o `Release`
4. Presionar `F5` o clic en "Iniciar"

## Ejecución

### Desde línea de comandos:

```powershell
# Ejecutar en modo desarrollo
dotnet run --project OrganizadorCapitulos.Maui/OrganizadorCapitulos.Maui.csproj -f net9.0-windows10.0.19041.0
```

### Ejecutar el binario compilado:

```powershell
# Debug
.\OrganizadorCapitulos.Maui\bin\Debug\net9.0-windows10.0.19041.0\win10-x64\OrganizadorCapitulos.Maui.exe

# Release
.\OrganizadorCapitulos.Maui\bin\Release\net9.0-windows10.0.19041.0\win10-x64\OrganizadorCapitulos.Maui.exe
```

## Publicación (Distribución)

### Crear ejecutable independiente:

```powershell
# Publicar como aplicación auto-contenida
dotnet publish OrganizadorCapitulos.Maui/OrganizadorCapitulos.Maui.csproj -c Release -f net9.0-windows10.0.19041.0 --self-contained true -r win-x64

# El resultado estará en:
# OrganizadorCapitulos.Maui\bin\Release\net9.0-windows10.0.19041.0\win-x64\publish\
```

### Crear instalador MSIX (opcional):

```powershell
# Cambiar en .csproj: <WindowsPackageType>None</WindowsPackageType> a:
# <WindowsPackageType>MSIX</WindowsPackageType>

# Luego publicar
dotnet publish OrganizadorCapitulos.Maui/OrganizadorCapitulos.Maui.csproj -c Release -f net9.0-windows10.0.19041.0
```

## Uso de la Aplicación

### Flujo básico:

1. **Cargar archivos**: Clic en `📁 Cargar` y selecciona carpetas con videos
2. **Analizar con IA**: Clic en `🤖 IA` para extraer metadatos automáticamente
3. **Editar manualmente**: Selecciona un archivo y edita Serie/Temporada/Episodio
4. **Renombrar**: 
   - `✏️ Renombrar` - Solo el archivo seleccionado
   - `✅ Todo` - Todos los archivos analizados
5. **Mover**: `💾 Mover` para mover todos a otra carpeta

### Modos de renombrado:

- **Mantener**: Al cambiar de archivo, mantiene Serie/Temporada e incrementa Episodio (+1)
- **Cambiar**: Cada archivo mantiene su propia información

### 🎬 Integración con TMDB (The Movie Database)
- **Metadatos Automáticos**: Conexión con la API de TMDB para buscar series y obtener títulos de episodios automáticamente.
- **Búsqueda Inteligente**: Buscador integrado para localizar la serie correcta y asociar sus datos al proceso de renombrado.

### 📂 Gestión de Archivos
- **Carga Recursiva**: Permite seleccionar múltiples carpetas y subcarpetas para cargar todos los archivos de video disponibles.
- **Explorador Personalizado**: Interfaz de selección de directorios integrada.

### TMDB (The Movie Database):
1. Clic en `⚙️` Configuración
2. Ingresa tu API Key de TMDB (gratis en themoviedb.org)
3. Clic en `🔍 TMDB` para buscar títulos de episodios

1. **Modo Mantener Estructura (`MaintainRenameStrategy`)**:
   - Ideal para renombrar series completas secuencialmente.
   - Utiliza el **Título** definido por el usuario.
   - Aplica automáticamente el formato estándar `Título - SxxExx` (Ej: `Breaking Bad - S01E05`).
   - **Auto-incremento**: Al renombrar un archivo, el sistema prepara automáticamente el siguiente número de capítulo.

```powershell
# Ejecutar todos los tests
dotnet test

### 🛡️ Seguridad y Control
- **Sistema Undo/Redo**: Implementación del patrón Command para deshacer y rehacer operaciones de renombrado, evitando pérdidas de datos accidentales.
- **Validaciones**: Verificación de caracteres inválidos en nombres de archivo y existencia de rutas.

### 📦 Organización Final
- **Mover a Destino**: Función "Guardar Todo" que permite mover todos los archivos procesados de la lista a una carpeta de destino final, centralizando la organización.

### La IA no funciona:
- Verifica que Python esté instalado: `python --version`
- Asegúrate de que Python está en el PATH del sistema

1. **Inicio**: Ejecuta la aplicación y haz clic en **"Cargar Carpetas"**.
2. **Selección**: Marca las carpetas que contienen tus videos y acepta.
3. **Configuración**:
   - Escribe el **Título** de la serie en el campo de texto.
   - Define el número de **Temporada** y el **Capítulo** inicial (ej. 1).
4. **Procesamiento**:
   - Selecciona el primer archivo de la lista.
   - Presiona **Enter** o el botón **Guardar**.
   - El archivo se renombrará, y el foco pasará al siguiente archivo con el número de capítulo incrementado (+1).
5. **Finalizar**: Cuando todos los archivos tengan el nombre correcto, haz clic en **"Guardar Todo"** para moverlos a su carpeta definitiva.

### La app no inicia:
- Verifica Windows 10 versión 1809+ o Windows 11
- Instala .NET 9 Runtime si es necesario

- **Lenguaje**: C#
- **Framework**: .NET 8.0 (Windows Forms)
- **Patrones de Diseño**:
  - **Strategy**: Para los algoritmos de renombrado.
  - **Command**: Para las operaciones de deshacer/rehacer.
  - **Observer**: Para la notificación de progreso.
  - **Repository**: Para la abstracción del sistema de archivos.

## ⚠️ Notas
- Asegúrate de tener permisos de escritura en las carpetas de origen y destino.
- El formato de renombrado por defecto es `Título - S[Temporada]E[Capítulo]`.
