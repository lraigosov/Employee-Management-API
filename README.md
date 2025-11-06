# Employee-Management-API

API RESTful moderna y segura para la gestión de empleados y departamentos, desarrollada con .NET 9, siguiendo principios de Clean Architecture.

## Tecnologías Utilizadas

- **.NET 9.0** - Framework principal
- **ASP.NET Core** - Web API
- **Entity Framework Core 9** - ORM con SQL Server
- **JWT Bearer Authentication** - Seguridad
- **FluentValidation** - Validaciones
- **Swagger/OpenAPI** - Documentación de API
- **xUnit, Moq, FluentAssertions** - Testing
- **Docker & Docker Compose** - Contenedorización

## Arquitectura

El proyecto sigue **Clean Architecture** con separación de responsabilidades:

```
src/
├── EmployeeManagement.Api/          # Capa de presentación (Controllers, Auth)
├── EmployeeManagement.Application/  # Lógica de aplicación (Services, DTOs, Validators)
├── EmployeeManagement.Domain/       # Entidades de dominio e interfaces
└── EmployeeManagement.Infrastructure/ # Implementación (EF Core, Repositories)

tests/
└── EmployeeManagement.Tests.Unit/   # Tests unitarios con xUnit
```

## Endpoints Principales

### Autenticación
- `POST /api/auth/login` - Autenticación con JWT

### Employees
- `GET /api/employees` - Listar todos los empleados
- `GET /api/employees/{id}` - Obtener empleado por ID
- `POST /api/employees` - Crear empleado
- `PUT /api/employees/{id}` - Actualizar empleado
- `DELETE /api/employees/{id}` - Eliminar empleado

### Departments
- `GET /api/departments` - Listar todos los departamentos
- `GET /api/departments/{id}` - Obtener departamento por ID
- `GET /api/departments/{id}/employees` - Empleados de un departamento
- `GET /api/departments/{id}/total-salary` - Salario total del departamento
- `POST /api/departments` - Crear departamento
- `PUT /api/departments/{id}` - Actualizar departamento
- `DELETE /api/departments/{id}` - Eliminar departamento

## Inicio Rápido

### Prerrequisitos
- Docker y Docker Compose
- .NET SDK 9.0 (opcional, para desarrollo local)

### Ejecución con Docker

```bash
# Iniciar todos los servicios
docker compose up -d

# La API estará disponible en http://localhost:8080
# SQL Server en localhost:1433
```

### Ejecución Local

```bash
# Restaurar dependencias
dotnet restore

# Ejecutar migraciones
dotnet ef database update --project src/EmployeeManagement.Infrastructure --startup-project src/EmployeeManagement.Api

# Ejecutar la API
dotnet run --project src/EmployeeManagement.Api
```

## Autenticación

Para usar la API, primero obtenga un token JWT:

```bash
curl -X POST http://localhost:8080/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "username": "admin",
    "password": "admin123"
  }'
```

Luego use el token en los headers:
```
Authorization: Bearer {su-token-aqui}
```

## Swagger UI

Acceda a la documentación interactiva de la API:
- **Desarrollo**: http://localhost:8080/swagger

## Testing

```bash
# Ejecutar todos los tests
dotnet test

# Ejecutar con cobertura
dotnet test --collect:"XPlat Code Coverage"
```

## Variables de Entorno

Copie `.env.example` a `.env` y ajuste según necesidad:

```bash
ASPNETCORE_ENVIRONMENT=Development
ConnectionStrings__Default=Server=localhost;Database=EmployeeManagement;...
Jwt__Key=SuperSecretKeyForJWTAuthentication2024!
Jwt__Issuer=http://localhost:8080
Jwt__Audience=http://localhost:8080
```

## Health Checks

- `/health/live` - Liveness probe
- `/health/ready` - Readiness probe (incluye verificación de BD)

## Modelo de Dominio

### Employee
- Cálculo automático de salario según posición
- Developer: +10% bonus
- Manager: +20% bonus
- HR/Sales: Salario base

### Department
- Relación 1:N con Employees
- Cálculo de salario total del departamento

## CI/CD

GitHub Actions configurado para:
- Build automático
- Ejecución de tests
- Validación de código

## Autor

**Luis Raigoso** (@LuisRai / lraigosov)

## Licencia

Este proyecto es de código abierto y está disponible para la comunidad bajo las siguientes condiciones:

- ✅ Libre uso, modificación y distribución
- ✅ Uso comercial y no comercial permitido
- ⚠️ **Se debe mantener el crédito al autor original**: Luis Raigoso (@LuisRai / lraigosov)
- ⚠️ Cualquier fork o derivado debe incluir esta atribución

### Atribución Requerida

Al usar, modificar o distribuir este código, debe incluirse la siguiente atribución:

```
Proyecto original desarrollado por Luis Raigoso (@LuisRai / lraigosov)
GitHub: https://github.com/lraigosov
```

---

**Desarrollado por Luis Raigoso para la comunidad de desarrolladores**
