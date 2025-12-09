# Organizador de Capítulos

Aplicación de escritorio desarrollada en C# (Windows Forms) para gestionar, renombrar y organizar archivos de video de series y animes de forma eficiente.

## 🚀 Características Principales

### 🎬 Integración con TMDB (The Movie Database)
- **Metadatos Automáticos**: Conexión con la API de TMDB para buscar series y obtener títulos de episodios automáticamente.
- **Búsqueda Inteligente**: Buscador integrado para localizar la serie correcta y asociar sus datos al proceso de renombrado.

### 📂 Gestión de Archivos
- **Carga Recursiva**: Permite seleccionar múltiples carpetas y subcarpetas para cargar todos los archivos de video disponibles.
- **Explorador Personalizado**: Interfaz de selección de directorios integrada.

### 🏷️ Estrategias de Renombrado
El sistema cuenta con diferentes modos de trabajo adaptables:

1. **Modo Mantener Estructura (`MaintainRenameStrategy`)**:
   - Ideal para renombrar series completas secuencialmente.
   - Utiliza el **Título** definido por el usuario.
   - **Formato Inteligente**:
     - Si hay datos de TMDB: `Título {TítuloEpisodio} SxxExx` (Ej: `Breaking Bad Ozymandias S05E14`).
     - Sin datos: `Título - SxxExx` (Ej: `Breaking Bad - S05E14`).
   - **Auto-incremento**: Al renombrar un archivo, el sistema prepara automáticamente el siguiente número de capítulo.

2. **Modo Cambiar Estructura**:
   - Permite una gestión más manual o variaciones en el flujo de trabajo (según implementación).

### ⚙️ Configuración
- **API Key Personalizable**: Ventana de ajustes para configurar tu propia API Key de TMDB, asegurando acceso privado y controlado a los metadatos.

### 🛡️ Seguridad y Control
- **Sistema Undo/Redo**: Implementación del patrón Command para deshacer y rehacer operaciones de renombrado, evitando pérdidas de datos accidentales.
- **Validaciones**: Verificación de caracteres inválidos en nombres de archivo y existencia de rutas.

### 📦 Organización Final
- **Mover a Destino**: Función "Guardar Todo" que permite mover todos los archivos procesados de la lista a una carpeta de destino final seleccionable por el usuario.

## 📖 Guía de Uso Rápida

1. **Configuración Inicial**:
   - Ve a `Ajustes` e introduce tu API Key de TMDB.
2. **Inicio**: Haz clic en **"Cargar Carpetas"** y selecciona los directorios con tus videos.
3. **Búsqueda (Opcional)**:
   - Usa el buscador para encontrar la serie en TMDB.
   - Al seleccionar la serie, el título se autocompletará.
4. **Configuración del Lote**:
   - Define el número de **Temporada** y el **Capítulo** inicial.
5. **Procesamiento**:
   - Selecciona el primer archivo de la lista.
   - Presiona **Enter** o el botón **Guardar**.
   - El archivo se renombrará usando el título del episodio (si está disponible), y el foco pasará al siguiente.
6. **Finalizar**: Haz clic en **"Guardar Todo"** y elige la carpeta donde quieres mover los archivos organizados.

## 🛠️ Tecnologías

- **Lenguaje**: C#
- **Framework**: .NET 9.0 (Windows Forms)
- **Patrones de Diseño**:
  - **Strategy**: Para los algoritmos de renombrado.
  - **Command**: Para las operaciones de deshacer/rehacer.
  - **Observer**: Para la notificación de progreso.
  - **Repository**: Para la abstracción del sistema de archivos.

## ⚠️ Notas
- Asegúrate de tener permisos de escritura en las carpetas de origen y destino.
- Para usar la funcionalidad de títulos de episodios, es necesaria una API Key válida de TMDB.
