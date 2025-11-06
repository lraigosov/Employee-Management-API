# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY EmployeeManagement.sln ./
COPY src/EmployeeManagement.Api/EmployeeManagement.Api.csproj src/EmployeeManagement.Api/
COPY src/EmployeeManagement.Application/EmployeeManagement.Application.csproj src/EmployeeManagement.Application/
COPY src/EmployeeManagement.Domain/EmployeeManagement.Domain.csproj src/EmployeeManagement.Domain/
COPY src/EmployeeManagement.Infrastructure/EmployeeManagement.Infrastructure.csproj src/EmployeeManagement.Infrastructure/

# Restore dependencies
RUN dotnet restore

# Copy all source files
COPY . .

# Build and publish
WORKDIR /src/src/EmployeeManagement.Api
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080

# Copy published app
COPY --from=build /app/publish .

# Health check
HEALTHCHECK --interval=30s --timeout=5s --retries=5 \
    CMD curl -f http://localhost:8080/health/live || exit 1

ENTRYPOINT ["dotnet", "EmployeeManagement.Api.dll"]
