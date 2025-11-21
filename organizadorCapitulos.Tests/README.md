# Tests - Organizador de Capítulos

Este proyecto contiene tests unitarios completos para la aplicación Organizador de Capítulos.

## Estructura de Tests

```
organizadorCapitulos.Tests/
├── Application/
│   ├── Commands/
│   │   ├── MoveFilesCommandTests.cs
│   │   └── RenameFileCommandTests.cs
│   ├── Services/
│   │   └── FileOrganizerServiceTests.cs
│   └── Strategies/
│       ├── ChangeStrategyTests.cs
│       ├── MaintainStrategyTests.cs
│       └── RenameStrategyFactoryTests.cs
└── Core/
    └── Entities/
        └── ChapterInfoTests.cs
```

## Tecnologías Utilizadas

- **xUnit**: Framework de testing
- **Moq**: Librería para crear mocks y simular dependencias
- **.NET 8.0**: Framework de desarrollo

## Ejecutar los Tests

### Ejecutar todos los tests
```powershell
dotnet test organizadorCapitulos.Tests/organizadorCapitulos.Tests.csproj
```

### Ejecutar tests con salida detallada
```powershell
dotnet test organizadorCapitulos.Tests/organizadorCapitulos.Tests.csproj --verbosity normal
```

### Ejecutar tests con cobertura de código
```powershell
dotnet test organizadorCapitulos.Tests/organizadorCapitulos.Tests.csproj --collect:"XPlat Code Coverage"
```

### Ejecutar tests específicos por nombre
```powershell
dotnet test organizadorCapitulos.Tests/organizadorCapitulos.Tests.csproj --filter "FullyQualifiedName~ChapterInfoTests"
```

## Cobertura de Tests

### Core.Entities.ChapterInfo
- ✅ Incremento de capítulos
- ✅ Generación de nombres de archivo
- ✅ Validación de información de capítulos
- ✅ Manejo de caracteres inválidos

### Application.Services.FileOrganizerService
- ✅ Carga de archivos de video
- ✅ Renombrado de archivos con validación
- ✅ Movimiento de archivos con progreso
- ✅ Manejo de archivos existentes

### Application.Commands
- ✅ Ejecución y deshacer de comandos de renombrado
- ✅ Ejecución y deshacer de comandos de movimiento
- ✅ Actualización de progreso
- ✅ Manejo de errores

### Application.Strategies
- ✅ Estrategia de cambio (ChangeStrategy)
- ✅ Estrategia de mantenimiento (MaintainRenameStrategy)
- ✅ Factory de estrategias

## Resultados de Tests

**Total de tests**: 37  
**Correctos**: 37  
**Fallidos**: 0  
**Tiempo de ejecución**: ~1.6 segundos

## Agregar Nuevos Tests

Para agregar nuevos tests, sigue este patrón:

```csharp
using Xunit;
using Moq;

namespace organizadorCapitulos.Tests.TuNamespace
{
    public class TuClaseTests
    {
        [Fact]
        public void MetodoAProbar_DeberiaHacerAlgo_CuandoCondicion()
        {
            // Arrange (Preparar)
            var mockDependencia = new Mock<IDependencia>();
            var sut = new TuClase(mockDependencia.Object);

            // Act (Actuar)
            var resultado = sut.MetodoAProbar();

            // Assert (Verificar)
            Assert.Equal(valorEsperado, resultado);
        }
    }
}
```

## Convenciones de Nombres

Los tests siguen la convención:
```
MetodoAProbar_DeberiaHacerAlgo_CuandoCondicion
```

Ejemplos:
- `GenerateFileName_ShouldFormatCorrectly`
- `RenameFileAsync_ShouldThrowException_WhenChapterInfoIsInvalid`
- `ExecuteAsync_ShouldMoveAllFiles`

## Integración Continua

Estos tests pueden integrarse fácilmente en pipelines de CI/CD:

```yaml
# Ejemplo para GitHub Actions
- name: Run Tests
  run: dotnet test --no-build --verbosity normal
```

## Notas Importantes

1. Los tests usan **Moq** para simular dependencias externas (IFileRepository, IProgressObserver)
2. No se realizan operaciones reales en el sistema de archivos
3. Todos los tests son independientes y pueden ejecutarse en cualquier orden
4. Los tests documentan el comportamiento esperado de cada componente
