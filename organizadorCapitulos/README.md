# Organizador de Capítulos

Aplicación de escritorio desarrollada en C# (Windows Forms) para gestionar, renombrar y organizar archivos de video de series y animes de forma eficiente.

## 🚀 Características Principales

### 📂 Gestión de Archivos
- **Carga Recursiva**: Permite seleccionar múltiples carpetas y subcarpetas para cargar todos los archivos de video disponibles.
- **Explorador Personalizado**: Interfaz de selección de directorios integrada.

### 🏷️ Estrategias de Renombrado
El sistema cuenta con diferentes modos de trabajo adaptables:

1. **Modo Mantener Estructura (`MaintainRenameStrategy`)**:
   - Ideal para renombrar series completas secuencialmente.
   - Utiliza el **Título** definido por el usuario.
   - Aplica automáticamente el formato estándar `Título - SxxExx` (Ej: `Breaking Bad - S01E05`).
   - **Auto-incremento**: Al renombrar un archivo, el sistema prepara automáticamente el siguiente número de capítulo.

2. **Modo Cambiar Estructura**:
   - Permite una gestión más manual o variaciones en el flujo de trabajo (según implementación).

### 🛡️ Seguridad y Control
- **Sistema Undo/Redo**: Implementación del patrón Command para deshacer y rehacer operaciones de renombrado, evitando pérdidas de datos accidentales.
- **Validaciones**: Verificación de caracteres inválidos en nombres de archivo y existencia de rutas.

### 📦 Organización Final
- **Mover a Destino**: Función "Guardar Todo" que permite mover todos los archivos procesados de la lista a una carpeta de destino final, centralizando la organización.

## 📖 Guía de Uso Rápida

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

## 🛠️ Tecnologías

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
