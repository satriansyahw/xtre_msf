# Issue: Phase 1 - Project Setup & Core Infrastructure

## Objective
Establish the foundational Clean Architecture structure, database persistence (SQLite), and core domain entities for the Regulatory and Licensing Platform.

## Requirements

### 1. Project Dependencies
Add the following NuGet packages:
- **Infrastructure**: `Microsoft.EntityFrameworkCore.Sqlite`, `Microsoft.EntityFrameworkCore.Design`.
- **Application**: `FluentValidation`.
- **Domain**: None (keep it pure).

### 2. Domain Entities (`Licensing.Domain`)
Implement the following entities:
- `BaseEntity`: `Guid Id`, `DateTime CreatedAt`, `DateTime UpdatedAt`.
- `Application`: Tracks the current state of a license request.
- `ApplicationSnapshot`: Stores a JSON representation of the application at a point in time for version comparison.
- `Document`: Metadata for uploaded files, including `AIStatus` (Pending/Verified/Flagged).
- `Feedback`: Contextual comments from Officers linked to specific fields or documents.
- `Enums`: `ApplicationStatus` (11 states as per JD).

### 3. Persistence (`Licensing.Infrastructure`)
- Implement `ApplicationDbContext` using EF Core.
- Configure SQLite connection string in `appsettings.json`.
- Create the initial migration.

### 4. File Storage Service
- Define `IFileStorageService` in `Application`.
- Implement in `Infrastructure` to save files to a local folder (path from `appsettings.json`).

### 5. API Configuration (`Licensing.Api`)
- Register DI services for DbContext and FileStorage.
- Configure Swagger to display API documentation.
- Implement a basic `HealthCheck` endpoint.

## Definition of Done
- [ ] Solution builds without errors.
- [ ] Initial migration is created and can be applied to `licensing.db`.
- [ ] Domain entities are correctly defined with relationships.
- [ ] `appsettings.json` contains valid SQLite and FileStorage configurations.
- [ ] Swagger UI is accessible at `/swagger`.
