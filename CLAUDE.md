# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Start the database
docker-compose -f docker/compose.yml up -d

# Run the application
dotnet run

# Build
dotnet build

# EF Core migrations
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

Swagger UI is available at `http://localhost:5076/swagger` when running in development.

## Architecture

ASP.NET Core 8 Web API using a layered architecture with generic base classes throughout:

```
Controllers → Mappers → Services → Repositories → EF Core (PostgreSQL)
```

**Generic base chain:** Every domain entity follows the same pattern:
- `EntityBase` → domain model (e.g., `User`)
- `DtoBase` → DTO (e.g., `UserDto`)
- `IRepositoryBase<T>` / `RepositoryBase<T>` → specific repository interface/implementation
- `IServiceBase<T>` / `ServiceBase<T, R>` → specific service interface/implementation
- `BaseController<E, D, S>` → specific controller

Each layer has an `Interfaces/` and `Implementations/` subfolder under `src/Services/` and `src/Repositories/`. Specific classes typically only override behavior that differs from the generic base.

**Multi-tenancy:** All entities have a `Tenant` FK via `EntityBase`. Repositories filter by tenant in all queries. Controllers extract `TenantId` from JWT claims (`User.FindFirst("tenantId")`).

**Mapping:** Uses [Riok.Mapperly](https://mapperly.riok.app/) source generator — mapping classes are partial classes with `[Mapper]` attribute. No reflection at runtime.

**DI registration:** All repositories and services registered as `Scoped` in `Program.cs`.

## Domain

Multi-tenant sports coaching platform. Key relationships:
- `User` (SUPER_ADMIN / OWNER / COACH) belongs to a `Tenant`
- `CoachSport` links a `User` (coach) to a `Sport`
- `Enrollment` is a program offering (coach + sport + fee)
- `AthleteEnrollment` links an `Athlete` to an `Enrollment` (with duration in months)
- `Payment` tracks payment for an `AthleteEnrollment`
