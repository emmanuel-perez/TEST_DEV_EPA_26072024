
# Aplicación de Gestión de Personas Físicas

Aplicación de ejemplo construida con ASP.NET Core y Dapper para manejar datos de personas físicas. Permite agregar, consultar, actualizar y eliminar registros de personas físicas desde una base de datos SQL Server.

## Contenidos

- [Características](#características)
- [Tecnologías Utilizadas](#tecnologías-utilizadas)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Configuración](#configuración)
- [Uso](#uso)
- [Endpoints](#endpoints)

## Características

- **Agregar Persona Física:** Permite agregar un nuevo registro de persona física.
- **Obtener Todas las Personas Físicas:** Recupera todos los registros de personas físicas.
- **Obtener Persona Física por ID:** Recupera un registro específico de persona física basado en su ID.
- **Actualizar Persona Física:** Permite actualizar la información de un registro de persona física existente.
- **Eliminar Persona Física:** Permite eliminar un registro de persona física por su ID.

## Tecnologías Utilizadas

- **ASP.NET Core:** Framework para el desarrollo de aplicaciones web.
- **Dapper:** Micro ORM para acceso a datos.
- **Swashbuckle:** Generación automática de documentación Swagger para APIs.
- **Microsoft SQL Server:** Base de datos relacional.

## Estructura del Proyecto

### `controllers/PersonaFisicaController.cs`

Controlador que maneja las solicitudes HTTP para gestionar las personas físicas. Proporciona los endpoints para agregar, obtener, actualizar y eliminar registros de personas físicas.

### `contracts/IPersonaFisicaRepository.cs`

Interfaz que define los métodos para interactuar con la base de datos relacionados con las personas físicas. Incluye métodos para agregar, obtener, actualizar y eliminar registros.

### `repository/PersonaFisicaRepository.cs`

Implementación de `IPersonaFisicaRepository` usando Dapper para interactuar con la base de datos. Los métodos incluyen:

- **`AddPersonaFisica`**: Ejecuta un procedimiento almacenado para agregar una nueva persona física.
- **`GetAllPersonasFisicas`**: Consulta todos los registros activos de personas físicas.
- **`GetPersonaFisicaById`**: Consulta un registro de persona física por su ID.
- **`DeletePersonaFisica`**: Marca un registro de persona física como inactivo.
- **`UpdatePersonaFisica`**: Actualiza los campos de un registro existente basado en su ID.

**Nota Importante:** Los procedimientos almacenados utilizados en los métodos `DeletePersonaFisica` y `UpdatePersonaFisica` tienen una condición invertida en el archivo de scripts de prueba. Por lo que
 implementé mis propios sql scripts en estos 2 Endpoints.

### `models/PersonaFisica.cs`

Modelo que representa una persona física en la base de datos. Incluye propiedades como `IdPersonaFisica`, `Nombre`, `ApellidoPaterno`, `ApellidoMaterno`, `RFC`, `FechaNacimiento`, entre otras.

## Configuración

### Instalación de las dependencias del proyecto

    dotnet restore

### Configuración de la conexión a la base de datos en el archivo `appsettings.json`

    {
        "ConnectionStrings": {
            "DefaultConnection": "Server=localhost;Database=TokaTestDatabase;User Id=sa;Password=SU_CONTRASEÑA;"
        }
    }

## Uso

1. Ejecuta la aplicación:

    dotnet watch run

## Endpoints

* POST /api/personas-fisicas: Agrega una nueva persona física.
* GET /api/personas-fisicas: Obtiene todas las personas físicas.
* GET /api/personas-fisicas/{personaFisicaId}: Obtiene una persona física por ID.
* PUT /api/personas-fisicas/{personaFisicaId}: Actualiza una persona física existente.
* DELETE /api/personas-fisicas/{personaFisicaId}: Elimina una persona física por ID.

